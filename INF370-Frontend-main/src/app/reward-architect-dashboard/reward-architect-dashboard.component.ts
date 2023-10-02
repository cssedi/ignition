import { Component } from '@angular/core';
import { DepartementChallengeService } from '../services/departement-challenge.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-reward-architect-dashboard',
  templateUrl: './reward-architect-dashboard.component.html',
  styleUrls: ['./reward-architect-dashboard.component.scss']
})
export class RewardArchitectDashboardComponent {
   messages : any[] = []
  constructor(private challengeService : DepartementChallengeService, private router : Router){
    this.getInbox()
  }
  viewSubmision : boolean =false

  submision =  {
    image : ''
  }


  approve(){

  }

  dissprove(){

  }

  getInbox() {
    this.router.navigate(['/reward-architech-dashboard'])
    this.viewSubmision = false
    this.challengeService.rewadArchitectInbox().subscribe({
      next : (response) => {
        this.messages = response
        console.log(this.messages)
      }
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
