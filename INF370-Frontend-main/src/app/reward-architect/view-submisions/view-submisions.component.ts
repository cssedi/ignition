import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DepartementChallengeService } from 'src/app/services/departement-challenge.service';

@Component({
  selector: 'app-view-submisions',
  templateUrl: './view-submisions.component.html',
  styleUrls: ['./view-submisions.component.scss']
})
export class ViewSubmisionsComponent {
  messages : any[] = []
  constructor(private challengeService : DepartementChallengeService, private router : Router){
    this.getInbox()
  }
  viewSubmision : boolean =false

  submision =  {
    image : '',
    challengerId : '',
    challengeId : 0
  }


  approve(){
    this.challengeService.approveChallenge(this.submision.challengerId, this.submision.challengeId).subscribe({
      next : (response) => {
        this.messages = response
        console.log(response)
      },
      error: (err) => {
       console.log(err)
     }
    })

    this.viewSubmision =false
  }

  dissprove(){

  }

  getInbox() {
  
    this.viewSubmision = false
    this.challengeService.rewadArchitectInbox().subscribe({
      next : (response : any[]) => {
        this.messages = response

        response.forEach(message => {
          
        });
        console.log('submisions ', this.messages)
      },
      error: (err) => {
        console.log('error getting submisons')
      },
    })
  }
  logOut(){
    localStorage.setItem('admin', 'false')
    localStorage.setItem('reward', 'fa')
  }
  viewSubmison(challengeId : number, challengerId : string)
  {

    this.challengeService.viewInbox(challengerId, challengeId).subscribe({
      next:(value) => {
        console.log(value)
        this.submision.image = value.submition
        this.submision.challengerId = value.challengerId
        this.submision.challengeId = value.challengeID

        console.log()
      }, complete: () => {
        this.viewSubmision = true
        
      },
       error: (err) => {
        console.log(err)
      },
    })
    console.log(challengeId)
  }
}
