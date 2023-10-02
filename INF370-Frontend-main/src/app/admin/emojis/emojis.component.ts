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
  selector: 'app-emojis',
  templateUrl: './emojis.component.html',
  styleUrls: ['./emojis.component.scss']
})
export class EmojisComponent implements OnInit {
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
            this.toast.success({detail: "SUCCESS", summary: "Maximum tokens updated succesfully", duration:3000})
          },
          error: (response)=>{
            console.log(response)
          }
        })
      }
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