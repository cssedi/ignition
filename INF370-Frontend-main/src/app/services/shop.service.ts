import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Prize } from '../Models/prize';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { PrizeDto } from '../Models/prize-dto';
import { PrizeOrderDto } from '../Models/prize-order-dto';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseApiURL = "https://localhost:7269/"
  constructor(private http: HttpClient) { }
  token = localStorage.getItem('token')
  httpOptions = {
    headers : new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${this.token}`
      })
    };
  GetAllPrizes(): Observable<Prize[]>
  {
    return this.http.get<Prize[]>(this.baseApiURL + "api/Reward/GetAllRewards", this.httpOptions)
  }
   getAllPrizeTypes():Observable<any>{
    return this.http.get<Prize[]>(this.baseApiURL + "api/Reward/GetAllRewardTypes")

   }
  GetPrize(id: string): Observable<Prize>
  {
    return this.http.get<Prize>(this.baseApiURL+ "api/Reward/GetReward/" +id)
  }

  createPrize(prizeDto : PrizeDto):Observable<any>{
    return this.http.post('https://localhost:7269/api/Reward', prizeDto, this.httpOptions);
  }

  createPrizeOrder(PrizeOrderDto : PrizeOrderDto[]):Observable<any> {
    return this.http.post('https://localhost:7269/api/PrizeOrder/CreatePrizeOrder', PrizeOrderDto, this.httpOptions);

  }
  updatePrize(id : number,  prizeDto : PrizeDto):Observable<any>{
    return this.http.put('https://localhost:7269/api/Reward/UpdatePrize/' + id, prizeDto, this.httpOptions);
  }
  deletePrize(id : number): Observable<any>{
    return this.http.delete<any>('https://localhost:7269/api/Reward/'+ id, this.httpOptions)
  }
}
