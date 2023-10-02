import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { throttleTime } from 'rxjs';
import { Location } from '@angular/common';
import { NgToastComponent, NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  error! : string;
  isAdmin : boolean = false;
  showErrorToast : boolean = false
  formSubmitted: boolean = false;

  user = { 
    name : '',
    surname : '',
    profile : ''
  }
  userRoles = [ 'role 1', 'role 2']
  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router : Router,
    private toast:NgToastService
  ) {
    
  }


  loginForm!: FormGroup;

ngOnInit() {
  this.loginForm = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  });

}
sysLogin(){
  localStorage.setItem('admin', 'true')
  this.router.navigate(['users'])
}


loginAs(role : string){

 if(role == "CHALLENGER"){
  this.router.navigate(['/user-challenges']).then( () => {
    window.location.reload()
  })

 

 }

 if(role == "ADMIN"){
  this.authService.adminLogin().subscribe({
    next :(value) => {
     localStorage.setItem('admin', 'true')
     localStorage.setItem('token', value.token)
     this.user.name = value.user.name
     
    }, 
    error(err) {
      console.log(err)

    },
    complete: () => {
      
      this.router.navigate(['/users']).then( () => {
        window.location.reload()
      })    
    },
   })
  
 

 }
 if(role == "REWARDARCHITECT"){
  this.authService.rewardArchitectLogin().subscribe({
   next :(value) => {
     localStorage.setItem('token', value.token)

    this.router.navigate(['/submitions'])

   }, 
   error(err) {
     console.log(err)

   },
   complete: () => {
     
     this.router.navigate(['/submitions']).then( () => {
       window.location.reload()
     })
  
   },
  })
}
 if(role == "SUPERARCHITECT"){
   this.authService.superArchitectLogin().subscribe({
    next :(value) => {
      localStorage.setItem('token', value.token)

     this.router.navigate(['/submitions'])

    }, 
    error(err) {
      console.log(err)

    },
    complete: () => {
      
      this.router.navigate(['/submitions']).then( () => {
        window.location.reload()
      })
   
    },
   })
 }
}

onSubmit() {
  this.formSubmitted = true;

  if (this.loginForm.valid) { 
    this.authService.login(this.loginForm.value.email, this.loginForm.value.password).subscribe({
      next : (response) =>{
         console.log(response)
         localStorage.setItem('token', response.token)
         localStorage.setItem('user', JSON.stringify(response.user))
        this.toast.success({detail:"SUCCESS",summary:'Login successful!',duration:5000});
         if(response.roles.length > 1){         
          this.isAdmin = true;
          this.userRoles = response.roles
          this.user.name = response.user.name
          this.user.surname = response.user.surname
          this.user.profile = response.user.profilePicture
  
         }
         else{
          this.isAdmin = false
          this.router.navigate(['/user-challenges'])
         }
         
      },
      complete : () => {
       // this.router.navigate(['/functions'])
      },
      error : (erorr) => {
        console.log(erorr.error)
        this.error = erorr.error.message
        this.showErrorToast = true
        this.toast.error({detail:"ERROR",summary: this.error ,duration:5000});
      }
    })
  }




  // if (this.loginForm.valid) {
  //   this.loginForm.value.email = 'challengerx.Smit@bmwithub.co.za'
  //   this.loginForm.value.email = 'Reward.123'
  //   const { email, password } = this.loginForm.value;

  //   this.authService.login(email, password).subscribe({
  //     next : (response) =>{
  //        console.log(response)
  //        localStorage.setItem('token', response.token)
  //        if(response.roles.length > 1){         
  //         this.isAdmin = true;
  //         this.userRoles = response.roles
  //        }
        
  //     },
  //     complete : () => {
  //      // this.router.navigate(['/functions'])
  //     },
  //     error : (erorr) => {
  //       console.log(erorr.error)
  //       this.error = erorr.error.message
  //     }
  //   })
  // }
}
}
