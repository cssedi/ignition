
import { AfterContentInit, AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import Chart from 'chart.js/auto';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { NgToastService } from 'ng-angular-popup';
import { ChallengeType } from 'src/app/Models/ChallengeType';
import { Department } from 'src/app/Models/Department';
import { DepartmentChallengeReports } from 'src/app/Models/DepartmentChallengeReport';
import { AdminService } from 'src/app/services/admin.service';
import { DepartementChallengeService } from 'src/app/services/departement-challenge.service';
import { ReportService } from 'src/app/services/reports/report.service';
import { ShopService } from 'src/app/services/shop.service';
import { SocialFeedService } from 'src/app/services/social-feed.service';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.scss']
})

export class ReportsComponent implements OnInit {
  @ViewChild('chartContainer', { static: false }) chartContainer!: ElementRef;  //Variables
  data: number[] = []
  likesData: number[] = []
  catergories: string[] = []
  DepartmentChallenges: DepartmentChallengeReports[] = []
  DepartmentArray: Department[] = []
  challengeTypes: ChallengeType[] = []
  challengeType!: FormGroup;
  selectedChallengeType!: number;
  topTenPrizes: any[] = [];
  private chart: ApexCharts | undefined;
  departmentDetails: Department = {
    departmentId: 0,
    departmentCode: '',
    name: '',
    functionId: 0,
    awardsArchitectId: '',
    checked: null,
    awardsArchitect: undefined
  }
  departmentCodeArray: string[] = []
  constructor(private reportsservice: ReportService, private toast: NgToastService, private adminservice: AdminService, private PrizeService: ShopService, private fb: FormBuilder, private socialFeedService: SocialFeedService) {
    
    this.PrizeService.GetAllPrizes().subscribe({
      next: (prizes: any[]) => {
        prizes.forEach(element => {
          if (element.prizeOrders.length > 0) {
            this.topTenPrizes.push(element);
          }

          this.topTenPrizes.sort((a, b) => b.prizeOrders.length - a.prizeOrders.length)
          this.topTenPrizes = this.topTenPrizes.slice(0, 10);

        });

        console.log(this.topTenPrizes)
      },
      error: (response) => {
        console.log(response)
      }
    })
  }

  generateGraph() {
    let options = {
      // set grid lines to improve the readabilit of the chart, learn more here: https://apexcharts.com/docs/grid/
      grid: {
        show: true,
        strokeDashArray: 4,
        padding: {
          left: 2,
          right: 2,
          top: -26
        },
      },
      series: [
        {
          name: "Posts",
          data: this.data,
          color: "#1A56DB",
        },
        {
          name: "Likes",
          data: this.likesData,
          color: "#7E3BF2",
        },
      ],
      chart: {
        height: "100%",
        maxWidth: "100%",
        type: "area",
        fontFamily: "Inter, sans-serif",
        dropShadow: {
          enabled: false,
        },
        toolbar: {
          show: true,
        },
      },
      tooltip: {
        enabled: true,
        x: {
          show: false,
        },
      },
      legend: {
        show: true
      },
      fill: {
        type: "gradient",
        gradient: {
          opacityFrom: 0.55,
          opacityTo: 0,
          shade: "#1C64F2",
          gradientToColors: ["#1C64F2"],
        },
      },
      dataLabels: {
        enabled: false,
      },
      stroke: {
        width: 6,
      },
      xaxis: {
        categories: this.catergories,
        labels: {
          show: true,
        },
        axisBorder: {
          show: true,
        },
        axisTicks: {
          show: true,
        },
      },
      yaxis: {
        show: false,
        labels: {
          formatter: function (value: any) {
            return value;
          }
        }
      },
    }

    if (document.getElementById("grid-chart") && typeof ApexCharts !== 'undefined') {
      const chart = new ApexCharts(document.getElementById("grid-chart"), options);
      chart.render();
    }
  }

  ngOnInit(): void {
    this.getAllChallengeTypes();
    this.challengeType = this.fb.group({
      challengeTypeId: new FormControl(0),
    })
    this.getPostsReport()

  }

  //filter data function
  filterDataByChallengeType() {
    //assign variable for challengeTpe ID
    this.selectedChallengeType = parseInt(this.challengeType.value.challengeTypeId)
    console.log(this.selectedChallengeType)
    //get depart challenges report


    if (this.selectedChallengeType == 0) {

      //get data for reports without Id if option is default
      this.reportsservice.DepartmentChallengeReport()
        .subscribe({
          next: (response) => {
            this.DepartmentChallenges = response
            console.log(this.DepartmentChallenges)
            this.createChart();
            // get departments to add to array
            this.DepartmentChallenges.forEach(department => {
              //get department by Id
              this.adminservice.getDepartmentById(department.department.departmentId)
                .subscribe({
                  next: (response) => {
                    this.departmentDetails.departmentCode = response.departmentCode;
                  }
                })
            });
          },
          complete: () => { },
          error: (error) => { }
        })
    }
    else {
      //get data if challenge type provided
      this.reportsservice.DepartmentChallengeById(this.selectedChallengeType)
        .subscribe({
          next: (response) => {
            this.DepartmentChallenges = response
            console.log(this.DepartmentChallenges) 
            this.createChart()
          },
          complete: () => {
             
          },
          error: (error) => {
          }
        })
    }



  }

  viewFullReport() {
    this.PrizeService.GetAllPrizes().subscribe({
      next: (prizes) => {
        this.topTenPrizes = prizes;

      },
      error: (response) => {
        console.log(response)
      }
    })
  }

  createChart() {
    const departmentCodes = this.DepartmentChallenges.map(department => department.departmentCode.toString());
    const departmentChallengeCount = this.DepartmentChallenges.map(dc => dc.count);
    const backgroundColors = departmentChallengeCount.map(() => this.generateRandomColor());
    const getChartOptions = () => {
      return {
        series: departmentChallengeCount,
        colors: backgroundColors,
        chart: {
          height: 530,
          width: "100%",
          type: "pie",
        },
        stroke: {
          colors: ["white"],
          lineCap: "",
        },
        plotOptions: {
          pie: {
            labels: {
              show: true,
            },
            size: "100%",
            dataLabels: {
              offset: -25
            }
          },
        },
        labels: departmentCodes,
        dataLabels: {
          enabled: true,
          style: {
            fontFamily: "Inter, sans-serif",
          },
        },
        legend: {
          position: "bottom",
          fontFamily: "Inter, sans-serif",
        },
        yaxis: {
          labels: {
            formatter: function (value: any) {
              return value
            },
          },
        },
        xaxis: {

          labels: {

            formatter: function (value: any) {
              return value
            },
          },
          axisTicks: {
            show: false,
          },
          axisBorder: {
            show: false,
          },
        },
      }
    }

    const pieChartElement = document.getElementById("pie-chart");

    if (pieChartElement && typeof ApexCharts !== 'undefined') {
      // Destroy the existing chart instance, if it exists
      if (this.chart) {
        this.chart.destroy();
      }
  
      // Create a new chart instance
      this.chart = new ApexCharts(pieChartElement, getChartOptions());
      this.chart.render();
    }
  }

  getAllChallengeTypes() {
    this.adminservice.getAllChallengeTypes().subscribe(
      {
        next: (response) => {
          this.challengeTypes = response
          console.log(this.challengeTypes)
        }
      }
    )
  }

  //background color function
  generateRandomColor() {
    const letters = '0123456789ABCDEF';
    let color = '#';
    for (let i = 0; i < 6; i++) {
      color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
  }

  getPostsReport() {

    this.socialFeedService.getPostsReport().subscribe({
      next: (response) => {
        this.data = response.data
        this.catergories = response.categories
        this.likesData = response.likesData
        console.log(response)
      }, complete: () => {
        this.generateGraph()
      },
      error: (error) => {
        console.log(error)
      }
    })
  }

  depChallChartPDF() {
    const chartContainer = this.chartContainer.nativeElement;

    if (chartContainer) {
      html2canvas(chartContainer).then((canvas) => {
        const doc = new jsPDF('landscape');
        const imgData = canvas.toDataURL('image/png');

        // Set up the PDF document
        doc.addImage(imgData, 'PNG', 10, 100, 190, 100);
        
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

        const description = 'Challenges per department, as of ' + formattedDate; 
        const moreInfo= 'The selected challenge type is'
        doc.setFontSize(10);
        doc.setTextColor(100);
        doc.text(description, 105, 70, { align: 'center' });

        // ... (your existing code for document setup)

        doc.save('DepartmentChallengeReport.pdf');
      });
    } else {
      console.error('Chart container not found.');
    }
  }

  topRewardsPDF() {
    const doc = new jsPDF();
    let yPos = 20;

    const currentDate = new Date();
    const day = String(currentDate.getDate()).padStart(2, '0');
    const month = String(currentDate.getMonth() + 1).padStart(2, '0');
    const year = String(currentDate.getFullYear());
    const formattedDate = day + '/' + month + '/' + year;

    const logoSrc = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQa8wwT3d7Q-UCENieoWH3frKQ8-XkQMy6r1utPjjIIoQ&s';
    const title = 'Top Ten Rewards Report';
    doc.addImage(logoSrc, 'PNG', 80, 20, 55, 30);
    doc.setFontSize(18);
    doc.text(title, 105, 60, { align: 'center' });

    const description = 'This is the top ten most popular rewards report for ignition, as of ' + formattedDate;
    doc.setFontSize(10);
    doc.setTextColor(100);
    doc.text(description, 105, 70, { align: 'center' });
    const maxPageHeight = doc.internal.pageSize.height - 20;

    yPos += 60;

    // Table styles
    const tableHeaders = ['Pos', 'Prize', 'Orders'];
    const colWidths = [30, 120, 30]; // Adjust colWidths as needed
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
    this.topTenPrizes.forEach((prize, pos) => {
      let xDataPos = 10;
      for (var i = 0; i < tableHeaders.length; i++) {
        doc.setTextColor(0);

        if (tableHeaders[i] === 'Pos') {
          doc.text((pos + 1).toString(), xDataPos + 2, yPos + 8);
          xDataPos += colWidths[i];
        }

        if (tableHeaders[i] === 'Prize') {
          doc.text(prize.name, xDataPos + 2, yPos + 8);
          xDataPos += colWidths[i];
        }

        if (tableHeaders[i] === 'Orders') {
          doc.text(prize.prizeOrders.length.toString(), xDataPos + 2, yPos + 8);
          xDataPos += colWidths[i];
        }
      }

      yPos += 10;
    });

    doc.save('TopTenRewardReport.pdf');
  }

}


