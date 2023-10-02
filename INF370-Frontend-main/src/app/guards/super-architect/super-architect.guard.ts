import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import jwt_decode from "jwt-decode";
import { TokenData } from 'src/app/Models/TokenData';
@Injectable({
  providedIn: 'root'
})
export class SuperArchitectGuard {

  canActivate(): boolean {

    if(localStorage.getItem('token')){
      var token = localStorage.getItem('token')!;

      var decoded : TokenData = jwt_decode(token);
      
       if(decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] == 'SUPERARCHITECT'){
        
        return true
       }
       
    }
    

     return false;
   }
}
