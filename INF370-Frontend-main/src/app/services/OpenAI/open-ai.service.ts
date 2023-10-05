import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OpenAIService {

  private apiKey = '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';
  private apiUrl = 'https://api.openai.com/v1/engines/davinci/completions';
  swearWords = ['fuck', 'shit', 'bitch', 'ass' ,'bastard', 'bullshit', 'bs','cunt', 'i hate' ]; 
  constructor(private http: HttpClient) {}

  analyzeText(text: string): Observable<{ sentiment: string, containsSwearWords: boolean }> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.apiKey}`
    });
  
    const promptText = `Please analyse the sentiment, the following text, please tell me if the text is positive, negative, or neutral in nature. If the text is talking negatively about a person, place or thing please return negative.  please only return 1 word "${text.toLowerCase()}"`;
    const requestBody = {
      prompt: promptText,
      max_tokens: 1
    };
  
  
    return this.http.post(this.apiUrl, requestBody, { headers }).pipe(
      map((response: any) => {
        const sentimentText = response.choices[0].text.trim().toLowerCase();
        let sentiment = '';
        if (sentimentText.includes('positive')) {
          sentiment = 'positive';
        } 
        else if (sentimentText.includes('negative') || sentimentText.includes('-') ) {
          sentiment = 'negative';
        } else {
          sentiment = 'neutral';
        }
  
        const containsSwearWords = this.swearWords.some(swearWord => text.toLowerCase().includes(swearWord));
  
        return { sentiment, containsSwearWords };
      })
    );
  }
}
