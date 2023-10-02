import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmojiService {

  constructor(private http : HttpClient) { }

  createEmoji(emoji : any):Observable <any>{
      return this.http.post<any>( 'https://localhost:7269/api/Emoji', { imageBase64 : emoji.base64Image})
  }
 

  deleteEmoji(id : number):Observable<any>{
    return this.http.delete<any>('https://localhost:7269/api/Emoji/' + id)
  }
  getAllEmojis():Observable<any[]>{
    return this.http.get<any[]>( 'https://localhost:7269/api/Emoji')
   
  }
  
}
