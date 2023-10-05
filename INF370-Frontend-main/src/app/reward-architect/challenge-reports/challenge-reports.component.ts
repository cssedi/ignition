import { AfterViewInit, Component } from '@angular/core';
import * as ApexCharts from 'apexcharts';
import { Chart } from 'chart.js/auto';
import jsPDF from 'jspdf';
import { NgToastService } from 'ng-angular-popup';
import { AdminService } from 'src/app/services/admin.service';
import { ChallengeService } from 'src/app/services/challenge.service';
import { DepartementChallengeService } from 'src/app/services/departement-challenge.service';
import { ReportService } from 'src/app/services/reports/report.service';
import { ShopService } from 'src/app/services/shop.service';

@Component({
  selector: 'app-challenge-reports',
  templateUrl: './challenge-reports.component.html',
  styleUrls: ['./challenge-reports.component.scss']
})
export class ChallengeReportsComponent implements AfterViewInit {
  challengers: any[] = []
  users!: any[]
  departments!: any[]
  challenges: any[] = []
  depChallengers: any[][] = [];

  combinedArray: [any, { user: any, completedChallenges: number }[], number, number][] = [];
  topOverallChallengers!: any[][]

  public chart: Chart | undefined

  constructor(private reportsService: ReportService, private toast: NgToastService, private adminService: AdminService, private prizeService: ShopService, private depChallService: DepartementChallengeService, private challService: ChallengeService) { }

  ngOnInit(): void {
    this.getAllDepartments();
    this.getLeaderboard();
  }

  ngAfterViewInit(): void {
    // this.createChart()
    // Can't have it here bc getleaderboard needs to execute first.
  }

  // Mik: I'm building the challenge completed by department report ehre bc idk where else to put it
  // I need the departments
  // The challenges related to each department
  // The challenges completed by each user in said department
  // So the dataflow would be => users => departments => filter to challenges per department => filter to challenge isntances per department = > completion rates or sum'
  getAllDepartments() {
    console.log("Fetching all departments...")
    this.adminService.getAllDepartments().subscribe(data => {
      this.departments = data;
      console.log('departments', this.departments);
      // console.log('department challenges', this.departments)
      console.log("Departments fetched successfully.");
    }, error => {
      console.error('Error fetching departments', error);
    });
  }

  // Problem here is leaderboard endpoint only pulls instances.
  // What if department wide challenge instance? Will skew results.
  // Sort this into a new array that groups every challenger object into departments
  getLeaderboard() {
    this.adminService.leaderBoard().subscribe({
      next: (reponse) => {
        this.users = reponse
        console.log("This from the leaderboard endpoint: ", reponse)
      },
      error: (error) => {
      },
      complete: () => {
        // Thinking a 3D array with departments || users in that department || total number of completed challenge instances for department
        // This is breaking my brain rn icl
        // let combinedArray: [string, { user: any, completedChallenges: number }[], number, number][] = [];

        // So, foreach dep
        this.departments.forEach((department) => {
          // Get all the users in that dep
          let usersInDepartment = this.users.filter(user => user.department && user.department.departmentCode === department.departmentCode);

          // Then get the total number of completed challenge instances for the entire dep
          let totalCompletedChallenges = usersInDepartment.reduce((total, user) => {
            let completedChallenges = user.challengeInstances.filter((ci: { challengeInstanceStatusId: number; }) => ci.challengeInstanceStatusId === 5).length;
            return total + completedChallenges;
          }, 0);

          // Then get the number of completed challenges for each user in that dep
          // I think this one is not neccessary, but extra data in tuple could prove useful
          let usersWithCompletedChallenges = usersInDepartment.map(user => {
            let completedChallenges = user.challengeInstances.filter((ci: { challengeInstanceStatusId: number; }) => ci.challengeInstanceStatusId === 5).length;
            return { user, completedChallenges };
          });

          // Then calculate the completion rate for the department
          let challengeCompletionRate = (totalCompletedChallenges) / (department.challenges.length * usersInDepartment.length);

          // Lastly push the data to the combined array/tuple
          this.combinedArray.push([department, usersWithCompletedChallenges, totalCompletedChallenges, challengeCompletionRate]);

          // NBNBNB: Adding mock completion rates here bc of infinite completion rates.
          // REMOVE THIS ONCE ENDPOINT IS FIXED
          // this.combinedArray.forEach((dep, pos) => {
          //   this.combinedArray[pos][3] = 0.75
          // })
          // *************************************************************************//

          console.log(this.combinedArray)


        });

        this.createChart();

        console.log(this.combinedArray);
      }
    })
  }

  createChart() {
    // Is already in descending order.
    // HOWEVER, infinite completion rates prevent graph from displaying properly.
    // Oscar endpoint**
    const chartData = this.combinedArray.map(dep => dep[0].departmentCode)
    const actualCR = this.combinedArray.map(dep => dep[3])

    // Using mock data to avoid infinite rates.
    // const completionRates = this.topOverallChallengers.map(cr => cr[1].value);
    const completionRates = [1, 1, 0.9, 0.87, 0.86, 0.8, 0.7, 0.6, 0.5, 0.4];

    // Colours don't need to be random ig?
    // Simplified teh random colour generation and integrated it
    const barColors = Array.from({ length: this.combinedArray.length }, () => `#${Math.random().toString(16).substr(-6)}`);

    this.chart = new Chart("depCompletionRates", {
      type: 'bar',
      data: {
        labels: chartData, // X
        datasets: [{
          label: 'Completion Rate',
          backgroundColor: barColors,
          data: actualCR
        }],
      },
      options: {
        aspectRatio: 2.5
      }

    });
  }

  depChallChartPDF() {
    const chartCanvas = document.getElementById('depCompletionRates') as HTMLCanvasElement;

    if (chartCanvas) {
      const doc = new jsPDF();
      const imgData = chartCanvas.toDataURL('image/png');

      let yPos = 20;

      const currentDate = new Date();
      const day = String(currentDate.getDate()).padStart(2, '0');
      const month = String(currentDate.getMonth() + 1).padStart(2, '0');
      const year = String(currentDate.getFullYear());
      const formattedDate = day + '/' + month + '/' + year;

      const logoSrc = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQa8wwT3d7Q-UCENieoWH3frKQ8-XkQMy6r1utPjjIIoQ&s';
      const title = 'Ignition Overall Leaderboard';
      doc.addImage(logoSrc, 'PNG', 80, 20, 55, 30);
      doc.setFontSize(18);
      doc.text(title, 105, 60, { align: 'center' });

      const description = 'This is the Overall Leaderboard for ignition, as of ' + formattedDate;
      doc.setFontSize(10);
      doc.setTextColor(100);
      doc.text(description, 105, 70, { align: 'center' });
      const maxPageHeight = doc.internal.pageSize.height - 20;

      yPos += 60;

      // Table styles
      const tableHeaders = ['Position', 'Challenger', 'Department', 'Participation'];
      const colWidths = [40, 40, 40, 40]; // Adjust colWidths as needed
      doc.setFontSize(12);

      doc.addImage(imgData, 'PNG', 10, 100, 190, 100);

      doc.save('ParticipationReport.pdf');
    } else {
      console.error('Chart canvas not found.');
    }
  }

}
