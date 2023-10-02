import { Component, OnInit } from '@angular/core';
import { NgToastService } from 'ng-angular-popup';
import { Audit } from 'src/app/Models/Audit';
import { DatabaseService } from 'src/app/services/database/database.service';

@Component({
  selector: 'app-database',
  templateUrl: './database.component.html',
  styleUrls: ['./database.component.scss']
})
export class DatabaseComponent implements OnInit {

constructor(private databaseService:DatabaseService, private toast: NgToastService) {}
  audits:Audit[] = []
  searchTerm!:string;

  ngOnInit(): void {
    this.getDatabaseAudits()
  }

  getDatabaseAudits(){
    this.databaseService.getDatabaseAudits().subscribe(
      {
        next:(response)=>
        {
          this.audits = response
          console.log(this.audits)
        },
        complete:()=>
        {

        },
        error:(error)=>
        {

        }
      })
  }

  backUpDatabase(){
    this.databaseService.backupDatabase().subscribe({
      next:(response)=>
      {
        this.toast.success({detail:"SUCCESS", summary: response.message, duration:5000})
      }, 
      complete:()=>
      {
        this.getDatabaseAudits()
      },
      error:(error)=>
      {
        this.toast.error({detail:"ERROR", summary: error.error.message, duration:5000})
      }
    })
  }
  restoreDatabase(){
    this.databaseService.restoreDatabase().subscribe({
      next:(response)=>
      {
        this.toast.success({detail:"SUCCESS", summary: response.message, duration:5000})
      }, 
      complete:()=>
      {
        this.getDatabaseAudits()
      },
      error:(error)=>
      {
        this.toast.error({detail:"ERROR", summary: error.error.message, duration:5000})
      }
    })
  }

  

  //search functionality
  searchDbAudits() {
    this.audits = this.audits.filter(entry =>
      entry.action.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
      entry.timestamp.toDateString().includes(this.searchTerm.toLowerCase())||
      entry.userId.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
      entry.amount.toString().includes(this.searchTerm.toLowerCase()) ||
      entry.quantity.toString().includes(this.searchTerm.toLowerCase())

    );

    if (this.searchTerm == '') {
      this.searchTerm = ''
      this.getDatabaseAudits()
    }
  }

  clearSearch() {
    this.searchTerm = ''
  }


}
