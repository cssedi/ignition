import { Component, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit{
  emailSent : boolean = false
  email! : string 
  formSubmitted : boolean = false 

  forgotPassword(){
    this.emailSent = true
  }
  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router : Router,
  ) {
    
  }
  resetPasswordForm!: FormGroup;

ngOnInit() {
  this.resetPasswordForm = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]],
  });

}


onSubmit(): void {
  this.formSubmitted = true

  console.log(this.resetPasswordForm.value.email)
  if (this.resetPasswordForm.valid) {
    this.authService.forgotPassword(this.resetPasswordForm.value.email).subscribe({
      next:(value) => {
        this.forgotPassword()
      },
      error: ( error) => {
        console.log(error.error)
      }
    })
    this.forgotPassword()
  }

}
}