import {  AfterViewInit,Component, OnInit } from '@angular/core';
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
    user: { name: '', email: '', password: '', username: '', bio: '', profilePicture: '', dateOfBirth: new Date(), surname: '', departmentId: 0 },
    prize: { prizeID: 0, name: '', description: '', frontImgURL: '', backImgURL: '', price: 0, prizeTypeID: 0 },
    countdown: ''
  }
  dropDown:boolean = false
  isLoading : boolean = true 
  departChallenges : any[] = []
  archivedChallenges : any[] = []
  constructor(private departChalService  : DepartementChallengeService, private router : Router, private toast:NgToastService, private challengeService : ChallengeService) {
    
  }
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

  toggleDropDown() {

      const dropdownButton = document.getElementById("dropdownButton");
      const dropdown = document.getElementById("dropdown");
        dropdownButton!.addEventListener("click", function () {
        dropdown!.classList.toggle("hidden");

      });  
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

