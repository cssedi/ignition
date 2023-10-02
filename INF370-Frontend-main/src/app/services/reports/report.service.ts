import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DepartmentChallengeReports } from 'src/app/Models/DepartmentChallengeReport';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor(private Http: HttpClient) { }


  baseAPIURL: string = "https://localhost:7269/api/"
  DepartmentChallengeById(id:number):Observable<DepartmentChallengeReports[]>{
    return this.Http.get<DepartmentChallengeReports[]>(this.baseAPIURL +"DepartmentChallenge/GetDepartmentChallengeReportsById/"+id)
  }

  DepartmentChallengeReport():Observable<DepartmentChallengeReports[]>{
    return this.Http.get<DepartmentChallengeReports[]>(this.baseAPIURL +"DepartmentChallenge/GetDepartmentChallengeReports")
  }
}
