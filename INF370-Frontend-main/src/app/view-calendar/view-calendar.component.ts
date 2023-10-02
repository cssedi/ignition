import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { CalendarOptions } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid'
import { TokenData } from '../Models/TokenData';
import jwt_decode from 'jwt-decode';
import { DepartementChallengeService } from '../services/departement-challenge.service';
import { Challenge } from '../Models/Challenge';
import { CommonModule, DatePipe } from '@angular/common';
import { FullCalendarComponent } from '@fullcalendar/angular';
import { co } from '@fullcalendar/core/internal-common';

@Component({
  selector: 'app-view-calendar',
  templateUrl: './view-calendar.component.html',
  styleUrls: ['./view-calendar.component.scss']
})

export class ViewCalendarComponent implements OnInit, AfterViewInit {
  @ViewChild('calendar') calendarComponent!: FullCalendarComponent;
  presentDays: number=0;
  absentDays: number=0;
  formattedDate!:string;
  title:any;
  eventObject =
  {
    title:'',
    date:'',
    color:''
  }
  constructor(private challengService:DepartementChallengeService, private datePipe: DatePipe) { }
  events: any =[];


  calendarOptions: CalendarOptions = {
    plugins: [dayGridPlugin],
    initialView: 'dayGridMonth',
    weekends: true,
    events: this.events,
    eventClick: this.handleDateClick.bind(this),
    
};
    ngOnInit(): void {
        //get challenges
        var token = localStorage.getItem('token')!;

        var decoded : TokenData = jwt_decode(token);
  
        this.challengService.getDapartmentChallenges(decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']).subscribe({
          next: (response) => {
            response.forEach((challenge: Challenge) => {
              this.formattedDate = this.datePipe.transform(challenge.endDate, 'yyyy-MM-dd')!;
              const newEventObject = {
                title: challenge.name,
                date: this.formattedDate,
                color: '#60a5fa'
              };
              this.events.push(newEventObject);
            });
            // Update the calendar events here, after all data is processed.
            this.calendarOptions.events = this.events;
            //set created challenges in local storage
            localStorage.setItem('calendar', JSON.stringify(this.calendarOptions.events));

          },
          complete: () => {
            // Code to execute when the subscription is complete.
          },
          error: (err) => {
            // Handle errors here.
          }
        });
        
}


ngAfterViewInit(): void {
    this.events.forEach((e:{[x:string]: string}) => {
      if (e["title"] == 'Present'){
        this.presentDays++
      }
      else{
        this.absentDays++
      }
    });
    //inititalize events from local storage
    this.calendarOptions.events = JSON.parse(localStorage.getItem('calendar')!);
}

handleDateClick(arg:any){
  console.log(arg);
  console.log(arg.event._def.title);
  this.title =arg.event._def.title; 
}
}
