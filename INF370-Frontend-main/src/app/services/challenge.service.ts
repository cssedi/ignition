import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Challenge } from '../Models/Challenge';
import { UpdateChallengeDto } from '../Models/update-challenge-dto';

@Injectable({
  providedIn: 'root'
})
export class ChallengeService {

  apiUrl = 'https://localhost:7269/api/Challenge/CreateChallenge'

   token = localStorage.getItem('token')
   httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${this.token}`
    })
  };
  constructor(private http  : HttpClient) { }

  createChallenge(departChal : Challenge) : Observable<Challenge>{
    return this.http.post<Challenge>(this.apiUrl, departChal, this.httpOptions)
  }

  superArchitectCreateChallenge(challengeObj : Challenge) : Observable<Challenge>{
    return this.http.post<Challenge>('https://localhost:7269/api/Challenge/SuperArchitectCreateChallenge', challengeObj, this.httpOptions)
  }
  
  getCreatedChallenges(id:string) : Observable<Challenge[]>{
    return this.http.get<Challenge[]>('https://localhost:7269/api/Challenge/GetArchitectChallenges/'+id, this.httpOptions)
  }

  updateChallenge(challengeId : number, challegeDto : UpdateChallengeDto) : Observable<any>{
    return this.http.put<any>('https://localhost:7269/api/Challenge/UpdateChallenge/' + challengeId , challegeDto, this.httpOptions)
  }


  getAllChallenges() : Observable<Challenge[]>{
      return this.http.get<Challenge[]>('https://localhost:7269/api/Challenge/GetAllChallengesActual/')
    }
  getMaximunTokens() : Observable<number>{
    return this.http.get<number>('https://localhost:7269/api/Challenge/GetMaximunToken', this.httpOptions)
  }

  updateMaximunTokens(maximumTokens : number) : Observable<any>{
    return this.http.post<any>('https://localhost:7269/api/Challenge/UpdateMaximumTokens/' + maximumTokens, this.httpOptions)
  }

}
