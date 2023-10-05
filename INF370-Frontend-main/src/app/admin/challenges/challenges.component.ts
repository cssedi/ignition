import {  AfterViewInit,Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Popover } from "flowbite";
import { initFlowbite } from 'flowbite';import { Tabs } from "flowbite";
import type { TabsOptions, TabsInterface, TabItem } from "flowbite";
import type { PopoverOptions, PopoverInterface } from "flowbite";
import { DepartementChallengeService } from 'src/app/services/departement-challenge.service';
import jwt_decode from 'jwt-decode';
import { TokenData } from 'src/app/Models/TokenData';
import { Router } from '@angular/router';
import { Challenge } from 'src/app/Models/Challenge';
import { NgToastService } from 'ng-angular-popup';
import { ChallengeService } from 'src/app/services/challenge.service';
import { Modal } from 'flowbite'
import type { ModalOptions, ModalInterface } from 'flowbite'
import jsPDF from 'jspdf';
@Component({
  selector: 'app-challenges',
  templateUrl: './challenges.component.html',
  styleUrls: ['./challenges.component.scss']
})
export class ChallengesComponent implements OnInit,AfterViewInit {
  departChallenge = { 
    name : ''
  }
  maximumTokens: number = 0;
    //modals
    viewMaxTokenModal: boolean = true
    modalVisible: boolean = false
    dropDownIsVisisble:boolean = false;
  $modalElement!: HTMLElement ;
  modalOptions!: ModalOptions
   modal!: ModalInterface
  archiveChallengeModal:boolean =false
  challengeObject:Challenge=
  {
    challengeID: 0,
    name: '',
    description: '',
    endDate: new Date(),
    startDate: new Date(),
    challengeTypeId: 0,
    medalId: 0,
    image: '',
    challengeType: { challengeTypeID: 0, name: '', challenges: [] },
    medal: { medalName: '', imageString: '', challengeTypeId: 0, challengeType: { challengeTypeID: 0, name: '', challenges: [] } },
    user: { name: '', email: '', password: '', userName: '', bio: '', profilePicture: '', dateOfBirth: new Date(), surname: '', departmentId: 0 },
    prize: { prizeID: 0, name: '', description: '', frontImgURL: '', backImgURL: '', price: 0, prizeTypeID: 0 },
    countdown: '',
    tokens: 0,
    prizeId: null
  }
  dropDown:boolean = false
  isLoading : boolean = true 
  departChallenges : any[] = []
  archivedChallenges : any[] = []
  constructor(private departChalService  : DepartementChallengeService, private router : Router, private toast:NgToastService, private challengeService : ChallengeService) {
    this.fetchTableData()
  }
     //reports begin 
  
 @ViewChild('cards', { static: false }) cardsContainer!: ElementRef;
 cardData: any[] = [];
 async fetchTableData() {
   try {
    
    var token = localStorage.getItem('token')!;

    var decoded : TokenData = jwt_decode(token);

    this.departChalService.getDapartmentChallenges(decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']).subscribe({
      next: (reponse) => {
        this.isLoading = true
        var data: any[] = reponse
        data.forEach(element => {
          element.startDate = new Date(element.startDate).toString();
          element.endDate = new Date(element.endDate).toString();
          element.startDate = element.startDate.replace(/ GMT\+\d{4} \(.*\)$/, "");
          element.endDate = element.endDate.replace(/ GMT\+\d{4} \(.*\)$/, "");




          const startDate = new Date(element.startDate);
          const day = startDate.getDate();
          const month = startDate.toLocaleString('default', { month: 'short' });
          const year = startDate.getFullYear();
          const hours = startDate.getHours();
          const minutes = startDate.getMinutes()
          element.startDate =`${day} ${month} ${year} ${hours}:${minutes}`

          const endDate = new Date(element.endDate);
          const endDateday = endDate.getDate();
          const endDatemonth = endDate.toLocaleString('default', { month: 'short' });
          const endDateyear = endDate.getFullYear();
          const endDatehours = endDate.getHours();
          const endDateminutes = endDate.getMinutes()
          element.endDate =`${endDateday} ${endDatemonth} ${endDateyear} ${endDatehours}:${endDateminutes}`
          this.cardData.push(element)

          
        });
      },
      complete: () =>  {
        this.isLoading = false
      },
     })
      this.downloadPDF(); 

   } catch (error) {
     console.error("Error fetching data:", error);
   }
 }

 
 downloadPDF() {
   if (this.cardData && this.cardData.length > 0) {
     const doc = new jsPDF('landscape'); // Set landscape orientation
     let yPos = 20;

  // Load the logo image
  const logoUrl = 'https://media.licdn.com/dms/image/C4E0BAQG8lNSut2H2_g/company-logo_200_200/0/1648744583668?e=2147483647&v=beta&t=CT7YiTeKyZNhn6D7T26KNqIWwWV8X48u-aF37Qbb-xk'; // Replace with your actual logo URL

  // Load the logo image asynchronously
  const img = new Image();
  img.src = logoUrl;// Set up an event listener to ensure the image is loaded before rendering the PDF
  
    // Draw the logo image on the PDF
    doc.addImage(img, 'PNG', 10, 5, 30, 20)
 
 
 
   //  heading with date
   const today = new Date();
   const formattedDate = today.toDateString();
   doc.setFontSize(18);
   doc.text('Challenges Report- ' + formattedDate, 50, 15);
   yPos += 10;
 
     //  table headers
     const tableHeaders = ['Name', 'Challenge Type', 'Start Date', 'End Date', 'Tokens','Prize', 'Description', 'Medal'];
     const colWidths = [30, 30, 30, 30, 30, 30, 40,30];
     doc.setFontSize(12);
     
     doc.setFillColor(0, 51, 102); // Header background color
     doc.setTextColor(255); // Header text color
     doc.setFont('bold');
     doc.rect(10, yPos, colWidths.reduce((a, b) => a + b), 10, 'F');
     let xPos = 10;
     for (let i = 0; i < tableHeaders.length; i++) {
       doc.text(tableHeaders[i], xPos + 2, yPos + 8);
       xPos += colWidths[i];
     }
     yPos += 10;
 
     //  table data
     doc.setFont('normal');
     this.cardData.forEach(challenge => {
       xPos = 10;
      
       for (let i = 0; i < tableHeaders.length; i++) {
         const headerKey = tableHeaders[i].toLowerCase();
         let cellContent = this.getCellContent(challenge, headerKey);
         if (cellContent.length > colWidths[i] / 3) {
                      cellContent = doc.splitTextToSize(cellContent, colWidths[i] - 10);
                    }
 
 
         console.log(`cellContent: ${cellContent}, xPos: ${xPos}, yPos: ${yPos}`);
         doc.setTextColor(0);
         doc.setFontSize(10);
         doc.text(cellContent, xPos, yPos + 8);
         xPos += colWidths[i];
       }
     
       // Move to the next row
       yPos += 10;
     
       // Insert image into PDF
       if (challenge.image) {
         this.fetchImageAsDataURL(challenge.image).then(imageDataURL => {
           doc.addImage(imageDataURL, 'JPEG', xPos + 2, yPos - 8, 20, 20); // Adjust width and height as needed
           
         });
       }
     });
     
     
     doc.save('Challenges_Report.pdf');
   } else {
     console.error("No data to generate PDF.");
   }
 }
 
 
 getCellContent(challenge: any, headerKey: any): any {
   switch (headerKey) {
     case 'tokens':
       return challenge.tokens > 0 ? `${challenge.tokens} tokens` : '0';
     case 'image':
       return ''; // You can't directly insert images in this way with jspdf(image processing thing fn)
     case 'medal':
       return challenge.medal.medalName;
     case 'challenge type':
       return challenge.challengeType.name;
     case 'start date':
       return challenge.startDate; 
     case 'end date':
       return challenge.endDate;
     case 'prize':
       return challenge.prize.name;
     default:
       return challenge[headerKey] !== undefined && challenge[headerKey] !== null
           ? challenge[headerKey].toString()
           : '';
   }
 }
 fetchImageAsDataURL(imageUrl: string): Promise<string> {
   return new Promise<string>((resolve, reject) => {
     const img = new Image();
     img.crossOrigin = 'Anonymous'; // To handle CORS issues
     img.src = imageUrl;
 
     img.onload = () => {
       const canvas = document.createElement('canvas');
       canvas.width = img.width;
       canvas.height = img.height;
 
       const ctx = canvas.getContext('2d');
       if (ctx) {
         ctx.drawImage(img, 0, 0, img.width, img.height);
 
         const dataURL = canvas.toDataURL('image/jpeg'); 
         resolve(dataURL);
       } else {
         reject('Canvas context is null.');
       }
     };
 
     img.onerror = (error) => {
       reject(error);
     };
   });
 }
 //reports end
  ngOnInit(): void {

    this.getActiveChallenges()
    this.getArchivedChallenges()
      // Get the maximun tokens 
      console.log(this.viewMaxTokenModal)
  this.challengeService.getMaximunTokens().subscribe({
    next:(value)=>{
      this.maximumTokens=value
      console.log(value)
    },
    error: (response)=>{
      console.log(response)
    }
  })
  
  }
  
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
  viewMaxTokensModal() {
    this.viewMaxTokenModal = !this.viewMaxTokenModal;
    console.log(this.viewMaxTokenModal)
   
   
  }
  
  //crud challenge functions
  getArchivedChallenges()
  {
    this.departChalService.getArchivedChallenges().subscribe({
      next: (response)=>{
        this.archivedChallenges = response
      },
      complete: () =>  {},
      error: (error) =>  {}
     })
  }
  getActiveChallenges()
  {
    var token = localStorage.getItem('token')!;

    var decoded : TokenData = jwt_decode(token);

    this.departChalService.getDapartmentChallenges(decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']).subscribe({
      next: (reponse) => {
        this.isLoading = true
        var data: any[] = reponse
        data.forEach(element => {
          element.startDate = new Date(element.startDate).toString();
          element.endDate = new Date(element.endDate).toString();
          element.startDate = element.startDate.replace(/ GMT\+\d{4} \(.*\)$/, "");
          element.endDate = element.endDate.replace(/ GMT\+\d{4} \(.*\)$/, "");




          const startDate = new Date(element.startDate);
          const day = startDate.getDate();
          const month = startDate.toLocaleString('default', { month: 'short' });
          const year = startDate.getFullYear();
          const hours = startDate.getHours();
          const minutes = startDate.getMinutes()
          element.startDate =`${day} ${month} ${year} ${hours}:${minutes}`

          const endDate = new Date(element.endDate);
          const endDateday = endDate.getDate();
          const endDatemonth = endDate.toLocaleString('default', { month: 'short' });
          const endDateyear = endDate.getFullYear();
          const endDatehours = endDate.getHours();
          const endDateminutes = endDate.getMinutes()
          element.endDate =`${endDateday} ${endDatemonth} ${endDateyear} ${endDatehours}:${endDateminutes}`
          this.departChallenges.push(element)

          
        });
      },
      complete: () =>  {
        this.isLoading = false
      },
     })
  }
  viewArchive(id : number){
    this.departChalService.getChallengeById(id).subscribe({
      next:(response)=>{
        this.challengeObject = response
        console.log(this.challengeObject)
        this.archiveChallengeModal = true

      }
    })
  }

  EditChallenge(challengeId : number){
    this.router.navigate(['edit-challenge', challengeId])
  }

  archiveChallenge(){
    this.departChalService.archiveChallenge(this.challengeObject.challengeID).subscribe({
      next: (response)=>{
        this.toast.success({detail:"SUCCESS", summary:"Challenge archived successfully", duration:5000})
        this.archiveChallengeModal = false
       
      },
      complete: () =>  {
      
      },
      error: (error) =>  {}
     })
  }
  showDropDownId:number =0
  toggleDropDown(id:number) {
    this.showDropDownId = id
    this.dropDownIsVisisble = !this.dropDownIsVisisble
  }
  toggleArchiveChallengeModal() {
    this.archiveChallengeModal = !this.archiveChallengeModal;
  }
  closeModal(){
    this.modal.hide();
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
  
}

