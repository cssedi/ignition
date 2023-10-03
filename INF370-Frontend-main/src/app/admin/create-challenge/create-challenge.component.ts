import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Modal } from 'flowbite'
import type { ModalOptions, ModalInterface } from 'flowbite'
import { Prize } from 'src/app/Models/prize';
import { AdminService } from 'src/app/services/admin.service';
import { ChallengeService } from 'src/app/services/challenge.service';
import { DepartementChallengeService } from 'src/app/services/departement-challenge.service';
import { ShopService } from 'src/app/services/shop.service';
import jwt_decode from "jwt-decode";
import { Dropdown } from "flowbite";
import type { DropdownOptions, DropdownInterface } from "flowbite";
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { RewardArchitectService } from 'src/app/guards/reward-architect.service';
import { SuperArchitectGuard } from 'src/app/guards/super-architect/super-architect.guard';
import { Department } from 'src/app/Models/Department';
import { TokenData } from 'src/app/Models/TokenData';
import { Challenge } from 'src/app/Models/Challenge';
import { DepartmentChallenge } from 'src/app/Models/DepartmentChallenge';
import { ChallengeType } from 'src/app/Models/ChallengeType';
@Component({
  selector: 'app-create-challenge',
  templateUrl: './create-challenge.component.html',
  styleUrls: ['./create-challenge.component.scss']
})
export class CreateChallengeComponent implements OnInit, AfterViewInit  {
  // varaibles
  maximumTokens : number = 0; 
  tokensExceeded : boolean = false
  formSubmitted:boolean = false
  isSuperArchitect: boolean = false
  isRewardsArchitect:boolean = false;
  Departments: Department[]= [];
  newChallenge : Challenge = {
    name: '',
    description: '',
    tokens: 0,
    endDate: new Date(),
    startDate: new Date(),
    challengeTypeId: 0,
    medalId: 0,
    prizeId: 0,
    image: '',
    challengeID: 0,
    challengeType: { challengeTypeID: 0, name: '', challenges: [] },
    medal: { medalName: '', imageString: '', challengeTypeId: 0, challengeType: { challengeTypeID: 0, name: '', challenges: [] } },
    user: { name: '', email: '', password: '', username: '', bio: '', profilePicture: '', dateOfBirth: new Date(), surname: '', departmentId: 0 },
    prize: { prizeID: 0, name: '', description: '', frontImgURL: '', backImgURL: '', price: 0, prizeTypeID: 0 },
    countdown: null
  }
  departmentDetails:Department ={
    departmentId: 0,
    departmentCode: '',
    name: '',
    awardsArchitectId: '',
    functionId: 0,
    checked: null
  }
  departmentChallengeDetails : DepartmentChallenge ={
    ChallengeID: 0,
    DepartmentId: 0,
  }
  checkedDepartments:Department[] = []
  base64Image: string | null = null;
  todaysDate : Date = new Date();
  selectedChallengeType!:number;
  selectedMedal = {
    medalId : 0,
    imageString : '', 
    medalName : ''
  }
  challengeType: ChallengeType = {
    challengeTypeID: 0, name: '',challenges: []}
  prizes: Prize[]=[];
  $targetEl!: HTMLElement ;
  // set the element that trigger the dropdown menu on click
   $triggerEl!: HTMLElement 
   departChallenges : any[] = []
   challengesTypes : any[] = []
   medals : any[] = [] 
   superArchitectForm!: FormGroup;
   rewardArchitectForm!: FormGroup;
   options: DropdownOptions = {
    placement: 'bottom',
    triggerType: 'click',
    offsetSkidding: 0,
    offsetDistance: 10,
    delay: 300,
    onHide: () => {
        console.log('dropdown has been hidden');
    },
    onShow: () => {
        console.log('dropdown has been shown');
    },
    onToggle: () => {
        console.log('dropdown has been toggled');
    }
  };
  constructor(private router : Router, private PrizeService: ShopService,  private chalService : ChallengeService, private departChalService : DepartementChallengeService, private AdminService : AdminService, private toast:NgToastService, private superArchitectGuard:SuperArchitectGuard, private fb:FormBuilder, private rewardArchitectService:RewardArchitectService) {}
  ngAfterViewInit(): void {
    this.$targetEl = document.getElementById('dropdownMenu')!;
    // set the element that trigger the dropdown menu on click
     this.$triggerEl = document.getElementById('dropdownButton')!;     
     const dropdown: DropdownInterface = new Dropdown(this.$targetEl, this.$triggerEl, this.options);
     // show the dropdown
     this.chalService.getMaximunTokens().subscribe({
      next:(value)=>{
        this.maximumTokens = value
        console.log(this.maximumTokens)
      }
    })
    
  }
   formattedDate!: string
  ngOnInit(): void {
    // Get maximum tokens 
    this.chalService.getMaximunTokens().subscribe({
      next:(value)=>{
        this.maximumTokens = value
        console.log(this.maximumTokens)
      }
    })
   // get all department challenges 
   this.getAllChallengeTypes()
   this.getAllPrize()
   
   if(this.superArchitectGuard.canActivate()){
    this.isSuperArchitect = true
    this.getSuperArchitectDepartments()
   }
   if(this.rewardArchitectService.canActivate()){
    this.isRewardsArchitect=true
    console.log('is reward architect', this.isRewardsArchitect)
   }
   
   this.Departments.forEach(department => {
    department.checked = false
   });
    this.formattedDate = this.todaysDate.toISOString().slice(0, 16);

    this.superArchitectForm = this.fb.group({
      name: new FormControl('', Validators.required),
      startDate: new FormControl('', Validators.required),
      endDate: new FormControl('', Validators.required),
      tokens: new FormControl(0, [Validators.min(0), Validators.required]),
      challengeTypeId: new FormControl('', Validators.required),
      prizeId: new FormControl(''),
      medalId: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      selectedDepartments: this.fb.array(this.checkedDepartments)
    })
    console.log('maximumTokens', this.maximumTokens)
    this.rewardArchitectForm = this.fb.group({
      name: new FormControl('', Validators.required),
      startDate: new FormControl('', Validators.required),
      endDate: new FormControl('', Validators.required),
      tokens: new FormControl(0, [Validators.min(0), Validators.required] ),
      challengeTypeId: new FormControl('', Validators.required),
      prizeId: new FormControl(null),
      medalId: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
    })
  }
  
  //Awards architect data tree
  getMedalsByChallengeType() {
    //reset form value when challenge type is changed
    this.rewardArchitectForm.controls['medalId'].reset()
    //assign variable for challengeTpe ID
    this.selectedChallengeType= parseInt(this.rewardArchitectForm.value.challengeTypeId)
    this.AdminService.getMedalsByChallengeType(parseInt(this.rewardArchitectForm.value.challengeTypeId))
    .subscribe({
      next:(medals)=>{

        this.medals=medals;
        console.log('medals', medals)
      },
      error: (response)=>{
        console.log(response)
      }
    })
  }
    //Super architect data tree
  getMedalsByChallengeTypeSuperArchitect() {
    //reset form value when challenge type is changed
    this.superArchitectForm.controls['medalId'].reset()
    //assign variable for challengeTpe ID
    this.selectedChallengeType= parseInt(this.superArchitectForm.value.challengeTypeId)
    this.AdminService.getMedalsByChallengeType(parseInt(this.superArchitectForm.value.challengeTypeId))
    .subscribe({
      next:(medals)=>{

        this.medals=medals;
        console.log('medals', medals)
      },
      error: (response)=>{
        console.log(response)
      }
    })
  }

  getAllChallengeTypes() {
   this.AdminService.getAllChallengeTypes().subscribe({
    next: (value) => {
      this.challengesTypes  = value 
      console.log('challlenge types', this.challengesTypes)
    },
   })
  }
  selectMedal(medalId : number){
    this.selectedMedal = this.medals.find( m => m.medalId == medalId)
    if(this.isSuperArchitect)
    {
      this.superArchitectForm.controls['medalId'].setValue(medalId)
    }
    else if(this.isRewardsArchitect)
    {
      this.rewardArchitectForm.controls['medalId'].setValue(medalId)
    }
    const dropdown: DropdownInterface = new Dropdown(this.$targetEl, this.$triggerEl, this.options);
     // show the dropdown
     dropdown.hide();
  }

  getAllPrize(){
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
  //Awards architect on submit
  onSubmit(){
    this.formSubmitted = true
    this.newChallenge.name = this.rewardArchitectForm.controls['name'].value
    this.newChallenge.description = this.rewardArchitectForm.controls['description'].value
    this.newChallenge.tokens = this.rewardArchitectForm.controls['tokens'].value
    this.newChallenge.startDate = this.rewardArchitectForm.controls['startDate'].value
    this.newChallenge.endDate = this.rewardArchitectForm.controls['endDate'].value
    this.newChallenge.challengeTypeId = parseInt(this.rewardArchitectForm.controls['challengeTypeId'].value)
    this.newChallenge.prizeId = parseInt(this.rewardArchitectForm.controls['prizeId'].value)
    if(this.newChallenge.prizeId == 0){
      this.newChallenge.prizeId = null
    }
    this.newChallenge.medalId = this.rewardArchitectForm.controls['medalId'].value
    console.log(this.newChallenge)
    //check if image uploaded
    if (this.base64Image) {
      this.newChallenge.image = this.base64Image;
      if(this.rewardArchitectForm.valid){

            //check for medalId error
            if(this.newChallenge.medalId != null)
            {
              //create challenge
              this.chalService.createChallenge(this.newChallenge).subscribe({
                next : (reponse) => {
                  this.toast.success({detail:"SUCCESS", summary: "challenge created successfully", duration:5000})
                }, complete : () => { 
                this.router.navigate(['/reward-architect-challenges'])
                },
                error: (error) =>{
                  this.toast.error({detail:"ERROR", summary: error.error.message, duration:5000})
                }
              })
            }

           }


      }
    else{
      this.toast.error({detail:"ERROR", summary: "Please select an image", duration:5000})
    }
  }
  //Super architect on submit
  superArchitectOnSubmit(){
    this.newChallenge.name = this.superArchitectForm.controls['name'].value
    this.newChallenge.description = this.superArchitectForm.controls['description'].value
    this.newChallenge.tokens = this.superArchitectForm.controls['tokens'].value
    this.newChallenge.startDate = this.superArchitectForm.controls['startDate'].value
    this.newChallenge.endDate = this.superArchitectForm.controls['endDate'].value
    this.newChallenge.challengeTypeId = parseInt(this.superArchitectForm.controls['challengeTypeId'].value)
    this.newChallenge.prizeId = parseInt(this.superArchitectForm.controls['prizeId'].value)
    this.newChallenge.medalId = this.superArchitectForm.controls['medalId'].value
    console.log(this.newChallenge.medalId)

    if (this.base64Image) {
      this.newChallenge.image = this.base64Image;
      if(this.superArchitectForm.valid){
           //check if at leats one department is selected
           if(this.checkedDepartments.length == 0)
           {
              this.toast.error({detail:"ERROR", summary: "Please select a department", duration:5000})
           }
           else
           {
            //check for medalId error
            if(this.newChallenge.medalId!= null)
            {
              //create challenge
              this.chalService.superArchitectCreateChallenge(this.newChallenge).subscribe({
                next : (reponse) => {
                  this.toast.success({detail:"SUCCESS", summary: "challenge created successfully", duration:5000})
                  //create department challenge for each selected department
                  this.checkedDepartments.forEach(department => {
                    this.departmentChallengeDetails.DepartmentId = department.departmentId
                    this.departmentChallengeDetails.ChallengeID = reponse.challengeID
                    
                    this.departChalService.createDepartmentChallenge(this.departmentChallengeDetails).subscribe({
                      next:(response)=>{
                      }
                })
              })
          
                }, complete : () => { 
                this.router.navigate(['/reward-architect-challenges'])
                },
                error: (error) =>{
                  this.toast.error({detail:"ERROR", summary: error.error.message, duration:5000})
                }
              })
            }

           }


      }
    }
    else{
      this.toast.error({detail:"ERROR", summary: "Please select an image", duration:5000})
    }

   }
  // image processing
  clearImageUpload(){
    this.base64Image = null
 }


  processImage(files: FileList | null) {
    if (files && files.length > 0) {
      const file = files[0];
      const allowedExtensions = ['jpg', 'jpeg', 'png', 'gif'];

      const fileExtension = file.name.split('.').pop()?.toLowerCase()!;
      console.log('FILE', fileExtension)
      if (allowedExtensions.includes(fileExtension)) {
        const reader = new FileReader();
        reader.onload = () => {
          this.base64Image = reader.result as string;
        };
        reader.readAsDataURL(file);
      } else {
        alert('Invalid file type. Please upload an image (JPEG, PNG, GIF, or SVG).');      }
    }
  }

  onFileSelected(event: Event) {
    const files = (event.target as HTMLInputElement).files;
    this.processImage(files);
  }

  fileTypeError ! : string 
  onImageDrop(event: DragEvent) {
    event.preventDefault();
    const droppedFiles = event.dataTransfer?.files;

    if (droppedFiles && droppedFiles.length > 0) {
      const file = droppedFiles[0];
      if (file.type.startsWith('image/')) {
        // The dropped file is an image
        this.processImage(event.dataTransfer?.files!);
      } else {
        // The dropped file is not an image
        alert('Invalid file type. Please upload an image (JPEG, PNG, GIF, or SVG).'); 
            }
    }
  }


  onDragOver(event: DragEvent) {
    event.preventDefault();
  }
  handleFileInput(event: any): void {
    const file = event.target.files[0];
  
    if (!file) {
      // No file selected, you can show an error message or handle it as needed
      console.log('No file selected');
      return;
    }
  
    // File type validation
    const allowedFileTypes = ['image/jpeg', 'image/png', 'image/gif', 'image/svg+xml'];
    if (!allowedFileTypes.includes(file.type)) {
      // Invalid file type, show an error message to the user
      this.toast.error({detail:"ERROR", summary:'Invalid file type. Please upload an image (JPEG, PNG, GIF, or SVG).' , duration:5000})
      return;
    }
  
    // File size validation (example: 2 MB limit)
    const maxSizeInBytes = 2 * 1024 * 1024; // 2 MB in bytes
    if (file.size > maxSizeInBytes) {
      // File size exceeds the limit, show an error message to the user
      this.toast.error({detail:"ERROR", summary:'File size exceeds the allowed limit (2 MB).' , duration:5000})
      return;
    }
  
    const reader = new FileReader();
  
    reader.onload = () => {
      this.base64Image = reader.result as string;
      console.log(this.base64Image);
    };
  
    reader.readAsDataURL(file);
  }
  

  getSuperArchitectDepartments(){
    const id= localStorage.getItem('token')
    var decoded : TokenData = jwt_decode(id!);
    this.AdminService.getSuperArchitectDepartments(decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'].toString())
    .subscribe({
      next:(response)=>{
        this.Departments = response;
        console.log('departments', response)
        this.superArchitectForm.get('department') as FormArray;
      }
    })
  }

  get selectedDepartments() {
    return this.superArchitectForm.get('selectedDepartments') as FormArray;
  }
  toggleItem(department: Department) {
    if (this.checkedDepartments.includes(department)) {
        this.checkedDepartments.forEach(department => {
          if(department.departmentId == department.departmentId){
            this.checkedDepartments.pop();
          }
        }
          )
    } else {
      this.checkedDepartments.push({ ...department });
      console.log(this.checkedDepartments)
    }
  }
}


