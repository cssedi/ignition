import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {
  resetForm!: FormGroup;
  formSubmitted : boolean = false 
  constructor(private route: ActivatedRoute, private formBuilder : FormBuilder, private authService: AuthService, private router : Router, private toast:NgToastService) { }

   ngOnInit() {
    const userId = this.route.snapshot.queryParams['userId'];
    const token = this.route.snapshot.queryParams['token'];
  
    console.log('userId:', userId);
    console.log('token:', token);

    this.resetForm = this.formBuilder.group({
      password: ['', [Validators.required, Validators.minLength(7), Validators.pattern('^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$%^&*]).{8,}$')]],
      confirmPassword: ['', Validators.required]
    });
    
  }

  onSubmit(){
    const userId = this.route.snapshot.queryParams['userId'];
    const token = this.route.snapshot.queryParams['token'];
    const password = this.resetForm.value.password;
    const confirmPassword  = this.resetForm.value.confirmPassword;
    this.formSubmitted =true
    this.authService.resetPassword(userId, token,password, confirmPassword).subscribe({
      next : (reponse) =>{
        console.log(reponse)
        this.toast.success({detail:"SUCCESS", summary:"Password reset successfully"})
      }, complete: () => {
        this.router.navigate(['login'])
      }
    })
    

  }
  
}
