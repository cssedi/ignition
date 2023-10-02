import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DepartmentChallenge } from '../Models/DepartmentChallenge';
import { Challenge } from '../Models/Challenge';


@Injectable({
  providedIn: 'root'
})
export class DepartementChallengeService {
  apiUrl = 'https://localhost:7269/api/DepartmentChallenge'

   token = localStorage.getItem('token')
   httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${this.token}`
    })
  };
  
  constructor(private http : HttpClient) { }

  getDapartmentChallenges(id:string):Observable<any>{
    return this.http.get<any>('https://localhost:7269/api/Challenge/GetArchitectChallenges/'+id, this.httpOptions)
    
  }
  getArchivedChallenges():Observable<any>{
    return this.http.get<any>('https://localhost:7269/api/Challenge/GetArchivedChallenges', this.httpOptions)
    
  }
 
  getChallengerChallenges(): Observable<any>{
    return this.http.get<any>('https://localhost:7269/api/DepartmentChallenge/GetChallengerChallenges', this.httpOptions)
  }
  getChallengeInstances(): Observable<any>{
    return this.http.get<any>('https://localhost:7269/api/ChallengeInstance', this.httpOptions)

  }
  

  getAllSubmitedChallenges(): Observable<any>{
    return this.http.get<any>('https://localhost:7269/api/ChallengeInstance/getUserSumitedChallenges', this.httpOptions)

  }

  rewadArchitectInbox() : Observable<any>{
    return this.http.get<any>('https://localhost:7269/api/ChallengeInstance/SubmitedChallenges', this.httpOptions)

  }
  enroll(challengeId : number): Observable<any>{
    var dto = {
      challengeId : challengeId
    }
    return this.http.post<any>('https://localhost:7269/api/ChallengeInstance', dto , this.httpOptions)
  }

  subminChallenge(challenge : any ) : Observable<any>{
    var dto = {
      challengeId : challenge.challengeId,
      file : challenge.file 
    }

    return this.http.post<any>('https://localhost:7269/api/ChallengeInstance/completeChallenge', dto , this.httpOptions)

  }

  viewInbox(challengerId : string,challengeId : number ):Observable<any>{
    var dto = {
      challengerId : challengerId,
      challengeId : challengeId
    }
    
    return this.http.post<any>('https://localhost:7269/api/ChallengeInstance/ViewSubmision', dto , this.httpOptions)

  }
  cancelChallengeInstance( id : number) : Observable<any>{
    return this.http.delete<any>('https://localhost:7269/api/ChallengeInstance/'+id, this.httpOptions)

  }
  approveChallenge(challengerId : string,challengeId : number ):Observable<any>{
    var dto = {
      challengerId : challengerId,
      challengeId : challengeId
    }
    
    return this.http.post<any>('https://localhost:7269/api/ChallengeInstance/ApproveChallenge', dto , this.httpOptions)

  }
  archiveChallenge(challengeId : number ):Observable<any>{
    return this.http.get<any>('https://localhost:7269/api/Challenge/ArchiveChallenge/'+challengeId, this.httpOptions)
  }


  createDepartmentChallenge(departmentChallengeObj:DepartmentChallenge):Observable<DepartmentChallenge>{
    return this.http.post<DepartmentChallenge>('https://localhost:7269/api/DepartmentChallenge/CreateDepartmentChallenge', departmentChallengeObj, this.httpOptions)
  }
  getChallengeById(id:number):Observable<Challenge>{
    return this.http.get<Challenge>('https://localhost:7269/api/Challenge/GetChallenge/'+id, this.httpOptions)
  }

}
