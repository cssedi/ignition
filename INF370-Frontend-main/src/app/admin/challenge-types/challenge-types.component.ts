import { Component } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-challenge-types',
  templateUrl: './challenge-types.component.html',
  styleUrls: ['./challenge-types.component.scss']
})
export class ChallengeTypesComponent {
  // Variables 
  challengeTypes!: any[]
  showModal: boolean = false
  showUpdateChallengeTypeModal: boolean = false;
  showDeleteChallengeTypeModal: boolean = false;
  challengeType: any = { name: '', id: 0 }
  modalVisible: boolean = false
  searchTerm!: string
  createChallengeTypeForm!: FormGroup;
  updateChallengeTypeForm!: FormGroup;
  formSubmitted:boolean=false;

  constructor(private adminService: AdminService, private fb: FormBuilder, private toast: NgToastService) {

  }

  toggleModal() {
    this.showModal = !this.showModal;
    this.modalVisible = !this.modalVisible;
    this.createChallengeTypeForm.reset()
  }

  toggleUpdateChallengeTypeModal() {
    this.showUpdateChallengeTypeModal = !this.showUpdateChallengeTypeModal;
    this.updateChallengeTypeForm.reset()
  }

  toggleDeleteChallengeTypeModal() {
    this.showDeleteChallengeTypeModal = !this.showDeleteChallengeTypeModal;

  }

  ngOnInit(): void {
    this.getAllChangeTypes()

    this.createChallengeTypeForm = this.fb.group({
      name: new FormControl('', Validators.required),
    })

    this.updateChallengeTypeForm = this.fb.group({
      name: new FormControl('', Validators.required),
      id: new FormControl('')
    })
  }

  searchChallengeType() {
    this.challengeTypes = this.challengeTypes.filter(challengeType =>
      challengeType.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );

    if (this.searchTerm == '') {
      this.searchTerm = ''
      this.getAllChangeTypes()
    }
  }

  getChallengeTypeById(id: number) {
    this.adminService.getChallengeTypeById(id).subscribe({
      next: (reponse) => {
        console.log('getChalleneTypeBuId', reponse)
        this.updateChallengeTypeForm.controls['name'].setValue(reponse.name)
        this.updateChallengeTypeForm.controls['id'].setValue(reponse.challengeTypeID)
        this.showUpdateChallengeTypeModal = true
      }
    })
  }

  viewDelete(id: number) {
    this.adminService.getChallengeTypeById(id).subscribe({
      next: (reponse) => {
        this.challengeType.name = reponse.name
        this.challengeType.id = reponse.challengeTypeID
        this.showDeleteChallengeTypeModal = true
      }
    })
  }

  deleteChallengeType() {
    this.adminService.deleteChallengeType(this.challengeType.id).subscribe({
      next: (response) => {
        this.toast.success({detail:"SUCCESS", summary:"Challenge Type deleted successfully", duration:5000})

      },
      complete: () => {
        this.getAllChangeTypes()
        this.showDeleteChallengeTypeModal = false
      },
      error:(error)=>{
        this.toast.error({detail:"ERROR", summary:error.error.message, duration:5000})

      }
    })
  }

  createChallengeType() {
    this.formSubmitted = true
  
    if (!this.createChallengeTypeForm.invalid) {
      let challengeType = {
        name: this.createChallengeTypeForm.value.name
      }
      this.adminService.addChallengeType(challengeType).subscribe({
        next: (response) => {
          console.log('addChallengeType - response ', response)
          this.toast.success({detail:"SUCCESS", summary:"Challenge Type created successfully", duration:5000})
        },
        complete: () => {
          this.getAllChangeTypes()
          this.showModal = false
        }
      })
    }
  }

  updateChallengeType() {
    if (!this.updateChallengeTypeForm.invalid) {
      let challengeType = {
        id: this.updateChallengeTypeForm.value.id,
        name: this.updateChallengeTypeForm.value.name
      }
      console.log('updated challenge type ', challengeType)
      this.adminService.updateChallengeType(challengeType).subscribe({
        next: (response) => {
          console.log('updaed ChallengeType - response ', response)
          this.toast.success({detail:"SUCCESS", summary:"Challenge Type updated successfully", duration:5000})


        },
        complete: () => {
          this.getAllChangeTypes()
          this.showUpdateChallengeTypeModal = false
        }
      })
    }
  }

  clearSearch() {
    this.searchTerm = ''
  }

  getAllChangeTypes() {
    this.adminService.getAllChallengeTypes().subscribe({
      next: (response) => {
        console.log('getAllChallengeTypes', response)
        this.challengeTypes = response
      },
      complete: () => {
      },
      error: () => {
        this.challengeTypes = []
      }
    })
  }

}