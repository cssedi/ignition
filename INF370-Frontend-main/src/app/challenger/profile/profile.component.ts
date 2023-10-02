import { AfterViewInit, Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Tabs } from 'flowbite';
import { AdminService } from 'src/app/services/admin.service';
import { AuthService } from 'src/app/services/auth.service';
import { DepartementChallengeService } from '../../services/departement-challenge.service';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements AfterViewInit {
  tabs!: Tabs
  options: any;
  tabElements: any;
  medals! : any[] 
  completedChallenges: any[] = []

  departments : any[] = []
  registrationForm!: FormGroup;
  base64Image: string | null = null;
  formSubmitted : boolean = false 
  formattedDate!: string
  todaysDate : Date = new Date();
  user : any
  constructor(private fb: FormBuilder,private authService : AuthService, private router : Router, private adminService : AdminService, private departChalService: DepartementChallengeService) {}

  getAllDepartments(){
    this.adminService.getAllDepartments().subscribe(data => {
      this.departments = data
      console.log(data)
    })
  }

  getUserDetails(){
    this.authService.getUserDetails().subscribe(data => {
      this.user = data.challenger 
      this.medals = data.medals
      console.log(data)
    })
  }

  ngOnInit(): void {
    
    this.getUserDetails()
    this.getAllDepartments()
  
    this.registrationForm = this.fb.group({
      name: ['', [Validators.required, Validators.pattern('^[A-Za-z]+$')]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(7), Validators.pattern('^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$%^&*]).{8,}$')]],
      confirmPassword: ['', [Validators.required, Validators.minLength(7)]],
      username: ['', Validators.required],
      bio: [''],
      profilePicture: [''],
      dateOfBirth: ['', Validators.required, ],
      surname: ['', Validators.required],
      departmentId: ['', Validators.required]
      
    });

    this.formattedDate = this.todaysDate.toISOString().slice(0, 16);
    console.log(this.formattedDate)
    this.base64Image = this.user.profilePicture
  }
  ngAfterViewInit(): void {
    this.base64Image = this.user.profilePicture

    // create an array of objects with the id, trigger element (eg. button), and the content element
     this.tabElements = [
      {
        id: 'profile',
        triggerEl: document.querySelector('#profile-tab-example'),
        targetEl: document.querySelector('#profile-example')
      },
      // {
      //   id: 'dashboard',
      //   triggerEl: document.querySelector('#dashboard-tab-example'),
      //   targetEl: document.querySelector('#dashboard-example')
      // },
      {
        id: 'settings',
        triggerEl: document.querySelector('#settings-tab-example'),
        targetEl: document.querySelector('#settings-example')
      },
      {
        id: 'contacts',
        triggerEl: document.querySelector('#contacts-tab-example'),
        targetEl: document.querySelector('#contacts-example')
      }
    ];

    // options with default values
     this.options = { 
      defaultTabId: 'settings',
      activeClasses: 'text-blue-600 hover:text-blue-600 dark:text-blue-500 dark:hover:text-blue-400 border-blue-600 dark:border-blue-500',
      inactiveClasses: 'text-gray-500 hover:text-gray-600 dark:text-gray-400 border-gray-100 hover:border-gray-300 dark:border-gray-700 dark:hover:text-gray-300',      onShow: () => {
      }
    };

    this.tabs = new Tabs(this.tabElements, this.options);
    this.tabs.show('profile');
     
  
   
  }

  // viewChallengesTab(){
  //   this.tabs.show('settings');
  // }

  // getAllSubmitedChallengesAuth() {
  //   this.departChalService.getAllSubmitedChallenges().subscribe({
  //     next: (reponse) => {
  //       this.completedChallenges = reponse
  //       console.log('getAllSubmitedChallengesAuth', reponse)
  //     }, complete: () => {

  //     },
  //     error: (err) => {
  //       console.log(err)
  //     },
  //   })
  // }

  signOut(){
    localStorage.clear()
    this.router.navigate(['/profile'])
  }
 
 
 
 
 
 
 
 
 
 
 
 
 
  onSubmit(): void {
    this.formSubmitted = true
    this.registrationForm.value.profilePicture = this.base64Image
    const registerModel = this.registrationForm.value;
   

    if (this.registrationForm.valid) {
      
      console.log(registerModel); // Display the registerModel object in the console or perform further actions
      this.authService.register(registerModel).subscribe({
        next: (reponse) =>{
          console.log(reponse)
        },
        complete : () =>{
          this.router.navigate(['login'])
        },
        error : (error) => {
          console.log('on register error', error.error)
        }
      })

    }


  }
 
  // Image processing 
  onImageDrop(event: DragEvent) {
    event.preventDefault();
    this.processImage(event.dataTransfer?.files!);
  }

  onDragOver(event: DragEvent) {
    event.preventDefault();
  }
  onFileSelected(event: Event) {
    const files = (event.target as HTMLInputElement).files;
    this.processImage(files);
  }
  processImage(files: FileList | null) {
    if (files && files.length > 0) {
      const reader = new FileReader();
      reader.onload = () => {
        
        this.base64Image = reader.result as string;
      };
      reader.readAsDataURL(files[0]);
    }
  }

  handleFileInput(event: any): void {
    const file = event.target.files[0];
    const reader = new FileReader();

    reader.onload = () => {
      this.base64Image = reader.result as string;
      console.log(this.base64Image)
    };

    reader.readAsDataURL(file);
  }

  clearImageUpload(){
    this.base64Image = null
 }


}
