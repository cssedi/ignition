import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Chart } from 'chart.js/auto';
import jsPDF from 'jspdf';
import { Observable, of, toArray } from 'rxjs';
import { AdminService } from 'src/app/services/admin.service';
import { DepartementChallengeService } from 'src/app/services/departement-challenge.service';
import { ShopService } from 'src/app/services/shop.service';

@Component({
  selector: 'app-leader-board',
  templateUrl: './leader-board.component.html',
  styleUrls: ['./leader-board.component.scss']
})

export class LeaderBoardComponent implements OnInit {
  @ViewChild('chartCanvas', { static: false })
  chartCanvas!: ElementRef<HTMLCanvasElement>;

  challengers: any[] = []
  users!: any[]
  departments!: any[]

  selectedDep!: string // Bound to the select list value

  topDepChallengers: any[][] = [];
  topOverallChallengers!: any[][]
  selectedDepartment!: any[]

  // challenges: any[] = []
  // challengeInstances!: any[]

  // These are for rewards
  // rewards!: Observable<any[]>
  // TopTenRewards!: any[][]
  // rewardOrders!: Observable<any[]>

  // Everything to do with the Chart.JS graph/s
  public chart: Chart | undefined

  constructor(private adminService: AdminService, private challengeService: DepartementChallengeService, private shopService: ShopService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.pullLeaderboard() // I'm not sure why I did this, but it works...
  }

  ngAfterViewInit(): void {
    // this.createChart()
  }

  pullLeaderboard() {
    this.adminService.leaderBoard().subscribe({
      next: (reponse) => {
        this.challengers = reponse
        console.log("This from the leaderboard endpoint: ", reponse)
      },
      error: (error) => {
      },
      complete: () => {
        this.getTopTenByDepartment()
        this.getTopTenOverall()
        this.getAllDepartments()
        this.createChart()
      }
    })
  }

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

  // Filter the challengers array to get the 10 challengers with the most completed challenges
  // For each challenger, check their department
  // For that department, check how many challenges were available
  // For each challenger, then check what % of challenges available to them they could have completed

  // Departments and associated challenges
  // Users belonging to that department
  // No. of challenges completed by each user
  // % of challenges completed by each user
  getTopTenOverall() {
    console.log("Generating leaderboard")
    this.topOverallChallengers = []
    let completionRate: any | undefined

    this.challengers.forEach((challenger, index) => {
      console.log('Challenge instances: ', challenger.challengeInstances)
      // console.log('Challenges: ', challenger.department.challenges)

      if (challenger.department == null) {
        // Should say this, making it 0 for now.
        // completionRate = 'non-calculable'
        completionRate = 0
      }
      else {
        // ERR: Danie has 14 challenge instances, but his department has only 3 challenges
        console.log('Challenger challenge instances:', challenger.challengeInstances)
        console.log('Challenger challenge instances:', challenger.challengeInstances.length)
        console.log('challenger department challenges: ', challenger.department.challenges)
        console.log('challenger department challenges: ', challenger.department.challenges.length)

        completionRate = challenger.challengeInstances.length / challenger.department.challenges.length
      }

      this.topOverallChallengers.push([challenger, completionRate])
      console.log(this.topOverallChallengers)
    })

    this.topOverallChallengers.sort((a, b) => b[1] - a[1])

    // Ensure that only the top 10 are kept.
    this.topOverallChallengers = this.topOverallChallengers.slice(0, 10)
    console.log("Top 10 in OVERALL: ", this.topOverallChallengers)
  }

  getTopTenByDepartment() {
    console.log("Generating departmental leaderboard: ", this.selectedDep)
    this.topDepChallengers = []

    // Filter the challengers array to get the challengers in the selected department
    var challengersInDepartment = this.challengers.filter(challenger => challenger.department == this.selectedDep)

    challengersInDepartment.forEach((challenger, index) => {
      if (!this.topDepChallengers[index]) {
        this.topDepChallengers[index] = [];
      }

      this.topDepChallengers[index].push(challenger);

      this.topDepChallengers[index][1] = challenger.challengeInstances.length
    })

    this.topDepChallengers.sort((a, b) => b[0][0] - a[0][0])

    this.topDepChallengers = this.topDepChallengers.slice(0, 10)
    console.log("Top 10 in DEPARTMENT: ", this.topDepChallengers)
  }

  dep10PDF() {
    const doc = new jsPDF();
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

    const description = 'This is the departmental leaderboard for Ignition, as of ' + formattedDate;
    doc.setFontSize(10);
    doc.setTextColor(100);
    doc.text(description, 105, 70, { align: 'center' });
    const maxPageHeight = doc.internal.pageSize.height - 20;

    yPos += 60;

    // Table styles
    const tableHeaders = ['Position', 'Challenger', 'Challenges Completed'];
    const colWidths = [40, 40, 40, 40]; // Adjust colWidths as needed
    doc.setFontSize(12);

    // Headers
    doc.setFillColor(51, 122, 183); // Header background color
    doc.setTextColor(255); // Header text color
    doc.setFont('bold');
    doc.rect(10, yPos, colWidths.reduce((a, b) => a + b, 0), 10, 'F');
    let xPos = 10;

    for (let i = 0; i < tableHeaders.length; i++) {
      doc.text(tableHeaders[i], xPos + 2, yPos + 8);
      xPos += colWidths[i];
    }
    yPos += 10;

    // Draw table data
    doc.setFont('normal');
    this.topDepChallengers.forEach((chall, pos) => {
      let xDataPos = 10;
      for (var i = 0; i < tableHeaders.length; i++) {
        doc.setTextColor(0);

        if (tableHeaders[i] === 'Position') {
          doc.text((pos + 1).toString(), xDataPos + 2, yPos + 8);
          xDataPos += colWidths[i];
        }

        if (tableHeaders[i] === 'Challenger') {
          doc.text(chall[0].name, xDataPos + 2, yPos + 8);
          xDataPos += colWidths[i];
        }

        if (tableHeaders[i] === 'Challenges Completed') {
          doc.text(chall[1].toString(), xDataPos + 2, yPos + 8);
          xDataPos += colWidths[i];
        }

      }

      yPos += 10;
    });

    doc.save('DepartmentalLeaderboard.pdf');
  }

  // This is the workign one for the PDF, still need to fix the completion rates
  top10PDF() {
    const doc = new jsPDF();
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

    // Headers
    doc.setFillColor(51, 122, 183); // Header background color
    doc.setTextColor(255); // Header text color
    doc.setFont('bold');
    doc.rect(10, yPos, colWidths.reduce((a, b) => a + b, 0), 10, 'F');
    let xPos = 10;

    for (let i = 0; i < tableHeaders.length; i++) {
      doc.text(tableHeaders[i], xPos + 2, yPos + 8);
      xPos += colWidths[i];
    }
    yPos += 10;

    // Draw table data
    doc.setFont('normal');
    this.topOverallChallengers.forEach((chall, pos) => {
      let xDataPos = 10;
      for (var i = 0; i < tableHeaders.length; i++) {
        doc.setTextColor(0);

        if (tableHeaders[i] === 'Position') {
          doc.text((pos + 1).toString(), xDataPos + 2, yPos + 8);
          xDataPos += colWidths[i];
        }

        if (tableHeaders[i] === 'Challenger') {
          doc.text(chall[0].name, xDataPos + 2, yPos + 8);
          xDataPos += colWidths[i];
        }

        if (tableHeaders[i] === 'Department') {
          doc.text(chall[0].surname, xDataPos + 2, yPos + 8);
          xDataPos += colWidths[i];
        }

        // Problem again is the infitite completion rates
        if (tableHeaders[i] === 'Participation') {
          doc.text(chall[1].toString(), xDataPos + 2, yPos + 8);
          xDataPos += colWidths[i];
        }

      }

      yPos += 10;
    });

    doc.save('OverallLeaderboard.pdf');
  }

  top10ChartPDF() {
    const chartCanvas = document.getElementById('chartCanvas') as HTMLCanvasElement;

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

      doc.addImage(imgData, 'PNG', 10, 100, 190, 100); // You can adjust the positioning and dimensions

      doc.save('OverallLeaderboardGraph.pdf'); // Save the PDF with a specific name
    } else {
      console.error('Chart canvas not found.');
    }
  }

  createChart() {
    // Is already in descending order.
    // HOWEVER, infinite completion rates prevent graph from displaying properly.
    // Oscar endpoint**
    const topDepChallengers = this.topOverallChallengers.map(challenger => challenger[0].name + ' ' + challenger[0].surname)

    // Using mock data to avoid infinite rates.
    const completionRates = this.topOverallChallengers.map(cr => cr[1]);
    //const completionRates = [1, 1, 0.9, 0.87, 0.86, 0.8, 0.7, 0.6, 0.5, 0.4];

    // Colours don't need to be random ig?
    // Simplified teh random colour generation and integrated it
    const barColors = Array.from({ length: topDepChallengers.length }, () => `#${Math.random().toString(16).substr(-6)}`);

    this.chart = new Chart("chartCanvas", {
      type: 'bar',
      data: {
        labels: topDepChallengers, // X
        datasets: [{
          label: 'Completion Rate',
          backgroundColor: barColors,
          data: completionRates
        }],
      },
      options: {
        aspectRatio: 2.5
      }

    });
  }

}