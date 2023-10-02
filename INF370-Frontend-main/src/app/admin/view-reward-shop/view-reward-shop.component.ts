import { AfterViewInit, Component, OnInit,ViewChild, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { Prize } from 'src/app/Models/prize';
import { ShopService } from 'src/app/services/shop.service';
import { Modal } from 'flowbite'
import type { ModalOptions, ModalInterface } from 'flowbite'

import { NgToastService } from 'ng-angular-popup';

import * as jspdf from 'jspdf';
import jsPDF from "jspdf";
import { ChallengeService } from 'src/app/services/challenge.service';



@Component({
  selector: 'app-view-reward-shop',
  templateUrl: './view-reward-shop.component.html',
  styleUrls: ['./view-reward-shop.component.scss']
})
export class ViewRewardShopComponent implements OnInit, AfterViewInit {
  maximumTokens: number = 0;
  prizes: Prize[]=[];
  prize : Prize= {
    prizeID: 0,
    name:"",
    description:"",
    frontImgURL:"",
    backImgURL:"",
    price:0,
    prizeTypeID:0
  } 

   $modalElement!: HTMLElement ;
   modalOptions!: ModalOptions
    modal!: ModalInterface
  isViewPrize: boolean = false
  isViewPrizes : boolean = true

  constructor(private PrizeService: ShopService, private router : Router, private toast: NgToastService, private challengeService: ChallengeService) {}
//start of report stuff
  @ViewChild('cards', { static: false }) cardsContainer!: ElementRef;
  cardData: any[] = [];

    // updateMaximunTokens  
    updateMaximumTokens(){
      if(this.maximumTokens > 0){
        this.challengeService.updateMaximunTokens(this.maximumTokens).subscribe({
          next:(response)=>{
            console.log(response)
          },
          error: (response)=>{
            console.log(response)
          }
        })
      }
    }

  fetchTableData() {
    // Replace with your API endpoint
    this.PrizeService.GetAllPrizes()
    .subscribe({
      next:(prizes)=>{
        this.cardData=prizes;
        console.log(this.cardData)
      },
      error: (response)=>{
        console.log(response)
      }

    })
    this.downloadPDF();
  }
  downloadPDF() {
    const doc = new jsPDF();
    let yPos = 20;

    // Load the logo image
  const logoUrl = 'https://media.licdn.com/dms/image/C4E0BAQG8lNSut2H2_g/company-logo_200_200/0/1648744583668?e=2147483647&v=beta&t=CT7YiTeKyZNhn6D7T26KNqIWwWV8X48u-aF37Qbb-xk'; // Replace with your actual logo URL


    // Load the logo image asynchronously
  const img = new Image();
  img.src = logoUrl;// Set up an event listener to ensure the image is loaded before rendering the PDF
  
    // Draw the logo image on the PDF
    doc.addImage(img, 'PNG', 10, 5, 30, 20)
  
    // Add heading with date
    const today = new Date();
    const formattedDate = today.toDateString();
    doc.setFontSize(18);
    doc.text('Rewards Report- ' + formattedDate, 50, 15);
    yPos += 10;
  
    // Table styles
    const tableHeaders = ['name', 'description','price'];
    const tableHeader = ['Name', 'Description','Price(Hubcoins)'];
    const colWidths = [90, 80, 20]; // Adjust colWidths as needed
    doc.setFontSize(12);
  
    // Draw headers
    doc.setFillColor(51, 122, 183); // Header background color
    doc.setTextColor(255); // Header text color
    doc.setFont('bold');
    doc.rect(10, yPos, colWidths.reduce((a, b) => a + b), 10, 'F');
    let xPos = 10;
    for (let i = 0; i < tableHeader.length; i++) {
      doc.text(tableHeader[i], xPos + 2, yPos + 8);
      xPos += colWidths[i];
    }
    yPos += 10;
  
    // Draw table data
    doc.setFont('normal');
    this.cardData.forEach(course => {
      let xDataPos = 10;
      for (let i = 0; i < tableHeaders.length; i++) {
        doc.setTextColor(0); // Set text color to black
        doc.text(course[tableHeaders[i]].toString(), xDataPos + 2, yPos + 8);
        xDataPos += colWidths[i];
      }
      yPos += 10;

      yPos += 2;
    });
  
    doc.save('Rewards_Report.pdf');
  }
  
  //end of report 

  
  ngOnInit(): void {

    // Get the maximun tokens 
    this.challengeService.getMaximunTokens().subscribe({
      next:(value)=>{
        this.maximumTokens=value
        console.log(value)
      },
      error: (response)=>{
        console.log(response)
      }
    })
 
    this.PrizeService.GetAllPrizes()
    .subscribe({
      next:(prizes)=>{
        this.prizes=prizes;
        console.log(prizes)
      },
      error: (response)=>{
        console.log(response)
      }

    })
  }

  createPrize() {
    this.router.navigate(['/create-prize']).then(() => {
      
    })
  }
  viewPrize(Id : number){
    this.prize = this.prizes.find( x=> x.prizeID == Id )!
    console.log()
    this.router.navigate(['/update-reward',Id])
 
   }
 

   viewDeleteModal(Id : number ){

    this.prize = this.prizes.find( x=> x.prizeID == Id )!
    this.modal.show();
   }

   deletePrize() {
   console.log(this.prize.prizeID)
    this.PrizeService.deletePrize(this.prize.prizeID).subscribe({
      next:(value) => {
        console.log('delete prize ', value)
        this.closeModal()
        this.toast.success({detail: "SUCCESS", summary: "Prize deleted succesfully", duration:3000})

        setTimeout(function(){
          window.location.reload();
       }, 3000)
      },error:(err) => {
        console.log('delete prize error ', err.error)
      },
    })

   

   }
   
   ngAfterViewInit(): void {
    this.$modalElement = document.querySelector('#modalEl')!;
    this.modal = new Modal(this.$modalElement, this.modalOptions);
    this.modalOptions = {
      placement: 'bottom-right',
      backdrop: 'dynamic',
      backdropClasses: 'bg-gray-900 bg-opacity-50 dark:bg-opacity-80 fixed inset-0 z-40',
      closable: true,
      onHide: () => {
          console.log('modal is hidden');
      },
      onShow: () => {
          console.log('modal is shown');
      },
      onToggle: () => {
          console.log('modal has been toggled');
      }
    }


   }
   batoToshop(){ 
    this.isViewPrize = false; 
    this.isViewPrizes = true; 
  }

  closeModal(){
    this.modal.hide();
  }
  
}
