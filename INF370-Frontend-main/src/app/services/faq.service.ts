import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FAQ } from '../Models/FAQ ';

@Injectable({
  providedIn: 'root'
})
export class FaqService {

  private apiUrl = 'https://localhost:7269/api/faq'; // Replace 'your-api-url' with your API URL

  constructor(private http: HttpClient) { }
  token = localStorage.getItem('token')
  httpOptions = {
    headers : new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${this.token}`
      })
    };
  getFAQ(): Observable<FAQ[]> {
    return this.http.get<FAQ[]>('https://localhost:7269/api/FAQs/Get', this.httpOptions);
  }

  createFAQ(faq: FAQ): Observable<FAQ> {
    return this.http.post<FAQ>('https://localhost:7269/CreateFAQ', faq);
  }

  updateFAQ(faqId: number, faq: FAQ): Observable<any> {
    const url = `${this.apiUrl}/${faqId}`;
    return this.http.put<any>(url, faq);
  }

  deleteFAQ
  (faqId: number): Observable<any> {
    const url = `https://localhost:7269/DeleteFAQ/${faqId}`;
    return this.http.delete<any>(url);
  }
}
