import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import jwt_decode from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class ArchitectGuard {

  constructor(private router : Router) { }
  canActivate(): boolean {
    var isAdmin : boolean = false
    var token = localStorage.getItem('token')!;
    interface TokenData {
      'http://schemas.microsoft.com/ws/2008/06/identity/claims/role': string;
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress': string;
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier': string;
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name': string;
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname': string;
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth': string;
      exp: number;
      iss: string;
      aud: string;
    }
    var decoded : TokenData = jwt_decode(token);
    
    console.log(decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);
    if (decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] == 'SUPERARCHITECT'
    || decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] == 'REWARDARCHITECT') {
      return true;
    } else {
      this.router.navigate(['login']); // Correctly navigate to the 'home' route
      return false; // Return false to indicate that navigation has been triggered
    }
  }
}
