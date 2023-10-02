import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/services/admin.service';
import { NgToastService } from 'ng-angular-popup';
import { Department } from 'src/app/Models/Department';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  departments : Department[] = []
  constructor(private fb: FormBuilder, private authService : AuthService, private router : Router, private adminService : AdminService, private toast: NgToastService) {}
  registrationForm!: FormGroup;
  base64Image: string | null = null;
  formSubmitted : boolean = false 
  formattedDate!: string
  todaysDate : Date = new Date();
  getAllDepartments(){
    this.adminService.getAllDepartmentsMain().subscribe(data => {
      this.departments = data
      console.log(data)
    })
  }

  ngOnInit(): void {
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
          this.toast.success({detail:"SUCCESS", summary: "Registration successful", duration:5000})
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

   getMaxDate(): string {
    const today = new Date();
    const year = today.getFullYear();
    const month = ('0' + (today.getMonth() + 1)).slice(-2); // Adding 1 because month is zero-indexed
    const day = ('0' + today.getDate()).slice(-2);
    return `${year}-${month}-${day}`;
  }
}
