import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { AdminService } from '../../services/admin.service';
import { ShopService } from 'src/app/services/shop.service';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-reward-category',
  templateUrl: './reward-category.component.html',
  styleUrls: ['./reward-category.component.scss']
})
export class RewardCategoryComponent {
  // Variables 
  // rewardCategories!: any[]
  prizeTypes!: any[]
  showModal: boolean = false
  showUpdateRewardCategoryModal: boolean = false;
  showDeleteRewardCategoryModal: boolean = false;
  formSubmitted:boolean = false;
  PrizeType: any = { name: '', id: 0 }
  modalVisible: boolean = false
  searchTerm!: string
  createRewardCategoryForm!: FormGroup;
  updateRewardCategoryForm!: FormGroup;
  
  constructor(private adminService: AdminService, private fb: FormBuilder, private shopService: ShopService, private toast:NgToastService) { }

  //NOTE: Temporarily using prize types from the DB instead of categories.    

  ngOnInit(): void {
    this.adminService.getAllPrizeTypes().subscribe({
      next: (value) => {
        this.prizeTypes = value
        console.log('prize types', this.prizeTypes)
      },
    })



    this.createRewardCategoryForm = this.fb.group({
      name: new FormControl('', Validators.required),
    })

    this.updateRewardCategoryForm = this.fb.group({
      name: new FormControl('', Validators.required),
      id: new FormControl('')
    })
  }

  toggleModal() {
    this.showModal = !this.showModal;
    this.modalVisible = !this.modalVisible;
  }

  toggleUpdateRewardCategoryModal() {
    this.showUpdateRewardCategoryModal = !this.showUpdateRewardCategoryModal;

  }

  toggleDeleteRewardCategoryModal() {
    this.showDeleteRewardCategoryModal = !this.showDeleteRewardCategoryModal;

  }

  searchRewardCategory() {
    if (this.searchTerm != '') {
      this.prizeTypes = this.prizeTypes.filter(RewardType =>
        RewardType.name.toLowerCase().includes(this.searchTerm.toLowerCase())
      );

      console.log("Matching search terms:", this.prizeTypes)

      if (this.searchTerm == '') {
        this.searchTerm = ''
        this.getAllRewardCategories()
      }
    }
    else{
      this.getAllRewardCategories()
    }

  }

  getRewardTypeById(id: number) {
    this.adminService.getRewardTypeById(id).subscribe({
      next: (reponse) => {
        console.log('getChalleneTypeBuId', reponse)
        this.updateRewardCategoryForm.controls['name'].setValue(reponse.name)
        this.updateRewardCategoryForm.controls['id'].setValue(reponse.prizeCategoryID)

        this.showUpdateRewardCategoryModal = true
      }
    })
  }

  viewDelete(id: number) {
    this.adminService.getRewardTypeById(id).subscribe({
      next: (reponse) => {
        this.PrizeType.name = reponse.name
        this.PrizeType.id = reponse.prizeCategoryID


        this.showDeleteRewardCategoryModal = true
      }
    })
  }

  deleteRewardCategory() {
    this.adminService.deleteRewardType(this.PrizeType.id).subscribe({
      next: (response) => {
      },
      complete: () => {
        this.getAllRewardCategories()
        this.showDeleteRewardCategoryModal = false
      },
      error: (error) =>{
        this.toast.error({detail:"ERROR", summary:error.erorr, duration:5000})
      }
    })
  }

  createRewardCategory() {
    this.formSubmitted = true
    if (!this.createRewardCategoryForm.invalid) {
      let PrizeType = {
        name: this.createRewardCategoryForm.value.name
      }
      this.adminService.addRewardType(PrizeType).subscribe({
        next: (response) => {
          console.log('addPrizeType - response ', response)
          this.toast.success({detail:"SUCCESS", summary:"Reward category created successfully", duration:5000})
        },
        complete: () => {
          this.adminService.getAllPrizeTypes().subscribe({
            next: (value) => {
              this.prizeTypes = value
              console.log('prize types', this.prizeTypes)
            },
          })
          this.showModal = false
        }
      })
    }
  }

  updateRewardCategory() {
    if (!this.updateRewardCategoryForm.invalid) {
      let RewardCategory = {
        id: this.updateRewardCategoryForm.value.id,
        name: this.updateRewardCategoryForm.value.name
      }
      console.log('updated challenge type ', RewardCategory)
      this.adminService.updateRewardType(RewardCategory).subscribe({
        next: (response) => {
          console.log('updated RewardCategory - response ', response)
        },
        complete: () => {
          this.getAllRewardCategories()
          this.showUpdateRewardCategoryModal = false
        }
      })
    }
  }

  clearSearch() {
    this.searchTerm = ''
  }

  //This needs to retrieve prize types instead
  getAllRewardCategories() {
    this.adminService.getAllPrizeTypes().subscribe({
      next: (response) => {
        console.log('getAllRewardCategories', response)
        this.prizeTypes = response
      },
      complete: () => {

      }, error: () => {
        this.prizeTypes = []
      }
    })
  }
}