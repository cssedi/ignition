import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Audit } from 'src/app/Models/Audit';

@Injectable({
  providedIn: 'root'
})
export class DatabaseService {

  constructor(private http:HttpClient) { }
  baseApiURL = "https://localhost:7269/api/"
  token = localStorage.getItem('token')
  httpOptions = {
    headers : new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${this.token}`
      })
    };

  getDatabaseAudits():Observable<Audit[]>{
    return this.http.get<Audit[]>(this.baseApiURL+"Database/GetDatabaseAuditTrailEntries")
  }

  backupDatabase():Observable<any>{
    return this.http.get<any>(this.baseApiURL+"Database/BackupDatabase", this.httpOptions)
  }

  restoreDatabase():Observable<any>{
    return this.http.get<any>(this.baseApiURL+"Database/RestoreDatabase", this.httpOptions)
  }


}
