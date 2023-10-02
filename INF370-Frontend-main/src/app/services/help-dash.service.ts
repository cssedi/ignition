import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Help } from '../Models/Help';

@Injectable({
  providedIn: 'root'
})

export class HelpDashService {
  private apiUrl = 'https://localhost:7269/api/Help'; // Replace 'your-api-url' with your API URL

  constructor(private http: HttpClient) { }

  getHelp(): Observable<Help[]> {
    return this.http.get<Help[]>('https://localhost:7269/api/Help/Get');
  }

  getLocations(): Observable<any[]> {
    return this.http.get<any[]>('https://localhost:7269/api/Help/GetLocations');
  }

  getContextualHelp(currentLocation: string): Observable<Help[]> {
    return this.http.get<Help[]>('https://localhost:7269/api/Help/GetContextualHelp/' + currentLocation);
  }

  createHelp(help: Help): Observable<Help> {
    return this.http.post<Help>('https://localhost:7269/api/Help/CreateHelp', help);
  }

  // For some reason update is creating an entirely new help.
  // updateHelp(HelpId: number, help: Help): Observable<any> {
  //   // Correct ID is retrieved
  //   console.log("Id of the help to be updated: ", HelpId)

  //   //This is where things go wrong
  //   // const url = `${this.apiUrl}/Update/${HelpId}`;
  //   const url = `${this.apiUrl}/Update/${HelpId}`;

  //   // const newURL = 'https://localhost:7269/api/Help/Update/' + HelpId;

  //   // This return works and doesn't throw any errors
  //   // I think problem is with the helpDto now
  //   // return this.http.put<any>('https://localhost:7269/api/Help/Update'+ HelpId, { Name: help.Name, Description: help.Description, Location: help.Location });  
  //   return this.http.put<any>(`https://localhost:7269/api/Help/Update/${HelpId}`, { Name: help.Name, Description: help.Description, Location: help.Location });

  //   // return this.http.put<any>(newURL, help);
  // }

  // Problem with Update was twofold.
  // Data wasn't being passed to the API properly.
  // Updated data wasn't being retrieved from the frontend properly.
  updateHelp(id: number, help: any): Observable<void> {
    // Testing if providing object in different format works.
    var tempHelp = {
      Name: help.Name,
      Description: help.Description,
      LocationId: help.LocationId
    }

    return this.http.put<void>(
      `${this.apiUrl}/Update/${id}`, tempHelp);
  }

  deleteHelp(HelpId: number): Observable<any> {
    const url = `https://localhost:7269/api/Help/DeleteHelp/${HelpId}`;
    return this.http.delete<any>(url);
  }
}