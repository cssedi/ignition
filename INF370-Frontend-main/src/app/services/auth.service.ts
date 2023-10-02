import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegisterModel } from '../Models/register-model';
import { CanActivate, Router } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class AuthService   {
  private apiUrl = 'https://localhost:7269/api/Auth'; // Replace with your API URL
 
  constructor(private http: HttpClient, private router: Router) { }
  token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJSRVdBUkRBUkNISVRFQ1QiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJjaGFsbGVuZ2VyeC5TbWl0QGJtd2l0aHViLmNvLnphIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJiMWI3NTA2ZC02M2QyLTQ5NmYtYTQzZC02NDIyN2U5Zjg4ODMiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiRGFuaWUgIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc3VybmFtZSI6IlNtaXQiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9kYXRlb2ZiaXJ0aCI6IjAwMDEvMDEvMDEgMDA6MDA6MDAiLCJEZXBhcnRtZW50SUQiOiIxIiwiZXhwIjoxNjkyMjg3NjM4LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjQyMDAiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQyMDAifQ.Ec_MqubAXaMU0aL8qd3pVXh02NP-D2zduEshIaZ9Qsc'

  // Gaurd 
  private checkIfUserIsLoggedIn(): boolean {
    // Implement your logic to check if the user is logged in
    // Return true if the user is logged in, otherwise return false
    // Example: return localStorage.getItem('isLoggedIn') === 'true';
    
   
    return localStorage.getItem('isLoggedIn') === 'true'
  }

  canActivate(): boolean {
    // Implement your authentication logic here
    const isAuthenticated = this.checkIfUserIsLoggedIn();

    // if (!isAuthenticated) {
    //   this.router.navigate(['/login']);
    // }
    //location.reload()
    return true;
  }
  
  isAdmin() : boolean {
    return true
  }
  // end points
  login(email: string, password: string): Observable<any> {
    const loginData = {
      email: email,
      password: password
    };
    
    return this.http.post<any>(`${this.apiUrl}/login`, loginData);
  }

  register(registerModel : RegisterModel): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/register`, registerModel);

  }

  rewardArchitectLogin() : Observable<any> {
    var token = localStorage.getItem('token')
     var httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`
      })
    };
    
    return this.http.get<any>('https://localhost:7269/api/Auth/RewardArchitectLogin', httpOptions);

  }

  superArchitectLogin() : Observable<any> {
    var token = localStorage.getItem('token')
     var httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`
      })
    };
    
    return this.http.get<any>('https://localhost:7269/api/Auth/SuperArchitectLogin', httpOptions);

  }

  adminLogin() : Observable<any> {
    var token = localStorage.getItem('token')
     var httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`
      })
    };
    
    return this.http.get<any>('https://localhost:7269/api/Auth/AdminLogin', httpOptions);

  }
  

  resetPassword( id : string, token : string, password : string, confirmPassword : string) : Observable<any>{
    const reset = {
       id : id,
       token : token,
       password : password,
       confirmPassword : confirmPassword
    }
    console.log('on-reques', reset)
    return this.http.post<any>(`${this.apiUrl}/ResetPassword`, reset);

  }

  forgotPassword(email : string ) : Observable<any>{
    var dto = {
      email : email
    }
    return this.http.post<any>('https://localhost:7269/api/Auth/ForgotPassword', dto);
  }

  getUserDetails() : Observable<any>{ 
    var token = localStorage.getItem('token')
    var httpOptions = {
     headers: new HttpHeaders({
       'Content-Type': 'application/json',
       Authorization: `Bearer ${token}`
     })
   };
    
    return this.http.get<any>('https://localhost:7269/api/Auth/GetUserDetails', httpOptions);

  }
}
