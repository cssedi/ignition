import { AfterViewInit, Component, OnInit } from '@angular/core';
import { DepartementChallengeService } from '../../services/departement-challenge.service';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import jwt_decode from "jwt-decode";
import { Tabs } from 'flowbite';
import { Challenge } from 'src/app/Models/Challenge';

@Component({
  selector: 'app-user-challenges',
  templateUrl: './user-challenges.component.html',
  styleUrls: ['./user-challenges.component.scss']
})
export class UserChallengesComponent implements OnInit, AfterViewInit {
  admin: boolean = false
  reward: boolean = false
  formattedDates: Date[] = [];
  challengeObject: Challenge =
    {
      challengeID: 0,
      name: '',
      description: '',
      endDate: new Date(),
      startDate: new Date(),
      challengeTypeId: 0,
      medalId: 0,
      image: '',
      challengeType: { challengeTypeID: 0, name: '', challenges: [] },
      medal: { medalName: '', imageString: '', challengeTypeId: 0, challengeType: { challengeTypeID: 0, name: '', challenges: [] } },
      user: { name: '', email: '', password: '', username: '', bio: '', profilePicture: '', dateOfBirth: new Date(), surname: '', departmentId: 0 },
      prize: { prizeID: 0, name: '', description: '', frontImgURL: '', backImgURL: '', price: 0, prizeTypeID: 0 },
      countdown: '',
      tokens: 0,
      prizeId: null
    }
  countdownInterval: any;
  countdown!: string;
  user = { name: '', surname: '', profilePicture: '', userName: '' };
  departChallenges: any[] = []
  challenges: Challenge[] = []
  challengeInstances: any[] = []
  completedChallenges: any[] = []
  tabElements: any[] = []
  options = {
    defaultTabId: '', activeClasses: '', inactiveClasses: '', onShow: () => {
      console.log('tab is shown');
    }
  };
  tabs = new Tabs(this.tabElements, this.options);

  constructor(private departChalService: DepartementChallengeService, private router: Router, private route: ActivatedRoute) {
  }

  ngAfterViewInit(): void {
    // Tabs 
    this.tabElements = [
      {
        id: 'challenges',
        triggerEl: document.querySelector('#challenges-tab'),
        targetEl: document.querySelector('#challenges')
      },
      {
        id: 'enrolled',
        triggerEl: document.querySelector('#enrolled-tab'),
        targetEl: document.querySelector('#enrolled')
      },
      {
        id: 'settings',
        triggerEl: document.querySelector('#submited-tab'),
        targetEl: document.querySelector('#submited')
      },
      // {
      //   id: 'reviewed',
      //   triggerEl: document.querySelector('#reviewed-tab'),
      //   targetEl: document.querySelector('#reviewed')
      // }
    ];



    this.options = {
      defaultTabId: 'settings',
      activeClasses: 'text-blue-600 hover:text-blue-600 dark:text-blue-500 dark:hover:text-blue-400 border-blue-600 dark:border-blue-500',
      inactiveClasses: 'text-gray-500 hover:text-gray-600 dark:text-gray-400 border-gray-100 hover:border-gray-300 dark:border-gray-700 dark:hover:text-gray-300',
      onShow: () => {
        console.log('tab is shown');
      }
    };
    this.tabs = new Tabs(this.tabElements, this.options);

    this.tabs.show('enrolled');
  }

  viewChallenges() {
    this.tabs.show('challenges');
  }

  viewSubmitedChallenges() {
    this.options = {
      defaultTabId: 'submited',
      activeClasses: 'text-blue-600 hover:text-blue-600 dark:text-blue-500 dark:hover:text-blue-400 border-blue-600 dark:border-blue-500',
      inactiveClasses: 'text-gray-500 hover:text-gray-600 dark:text-gray-400 border-gray-100 hover:border-gray-300 dark:border-gray-700 dark:hover:text-gray-300',
      onShow: () => {
        console.log('tab is shown');
      }
    };
    this.tabs = new Tabs(this.tabElements, this.options);
    this.tabs.show('submited');
  }

  viewEnrolledChallenges() {

    this.options = {
      defaultTabId: 'enrolled',
      activeClasses: 'text-blue-600 hover:text-blue-600 dark:text-blue-500 dark:hover:text-blue-400 border-blue-600 dark:border-blue-500',
      inactiveClasses: 'text-gray-500 hover:text-gray-600 dark:text-gray-400 border-gray-100 hover:border-gray-300 dark:border-gray-700 dark:hover:text-gray-300',
      onShow: () => {
        console.log('tab is shown');
      }
    };
    this.tabs = new Tabs(this.tabElements, this.options);
    this.tabs.show('enrolled');
  }

  viewReviewedChallenges() {
    this.options = {
      defaultTabId: 'reviewed',
      activeClasses: 'text-blue-600 hover:text-blue-600 dark:text-blue-500 dark:hover:text-blue-400 border-blue-600 dark:border-blue-500',
      inactiveClasses: 'text-gray-500 hover:text-gray-600 dark:text-gray-400 border-gray-100 hover:border-gray-300 dark:border-gray-700 dark:hover:text-gray-300',
      onShow: () => {
        console.log('tab is shown');
      }
    };
    this.tabs = new Tabs(this.tabElements, this.options);
    this.tabs.show('reviewed');
  }

  ngOnInit(): void {
    this.user = JSON.parse(localStorage.getItem('user')!)
    this.getAllChalInstances()
    this.getAllChallenges()
    this.getAllSubmitedChallengesAuth()
  }

  userPfpButton() {
    console.log("Dropdown button clicked")
  }

  getAllSubmitedChallengesAuth() {
    this.departChalService.getAllSubmitedChallenges().subscribe({
      next: (reponse) => {
        this.completedChallenges = reponse
        console.log('getAllSubmitedChallengesAuth', reponse)
      }, complete: () => {

      },
      error: (err) => {
        console.log(err)
      },
    })
  }

  completeChallenge(challengeId: number) {
    this.router.navigate(['complete-challenge', challengeId])
    console.log(challengeId)
  }

  getAllChalInstances() {
    this.departChalService.getChallengeInstances().subscribe({
      next: (reponse) => {
        this.challengeInstances = reponse
        console.log('getChallengeInstances', reponse)
      }, complete: () => {

      },
      error: (err) => {
        console.log(err)
      },
    })
  }

  cancelChallengeInstance(id: number) {
    console.log(id)
    this.departChalService.cancelChallengeInstance(id).subscribe({
      next: (response) => {
        console.log(response)
      }, complete: () => {
        this.getAllSubmitedChallengesAuth()
        this.getAllChalInstances(),
          this.getAllChallenges()
      },
      error: (error) => {
        console.log('delete challenge instance', error.error)
      }
    })
  }

  signOut() {
    localStorage.clear()
    this.router.navigate(['/login']).then(() => {
      location.reload()
    })

  }

  getAllChallenges() {
    this.departChalService.getChallengerChallenges().subscribe({
      next: (response) => {
        var data: any[] = response;
        console.log('depart challenges', data);
        data.forEach((element) => {
          const challenge: Challenge = element.challenge;
          challenge.endDate = new Date(element.challenge.endDate);
          
          // Calculate and update the countdown for this challenge
          this.updateCountdown(challenge);
          
          this.challenges.push(challenge);
        });
  
        // Periodically update the countdown for each challenge
        setInterval(() => {
          this.challenges.forEach((challenge) => {
            this.updateCountdown(challenge);
          });
        }, 1000);
      },
      error(err) {
        console.log(err.error);
      },
    });
  }
  


  enroll(challengeId: number) 
  {
    console.log(challengeId)
    this.departChalService.enroll(challengeId).subscribe({
      next: (response) => {
        console.log(response)
      }, complete: () => {
        this.getAllChalInstances()
        this.viewEnrolledChallenges()

      },
      error: (error) => {
        console.log('error on enroll', error.error)
      }
    })
  }

  updateCountdown(challenge: Challenge) {
    const currentDate = new Date();
    const timeDifference = challenge.endDate.getTime() - currentDate.getTime();
  
    if (timeDifference <= 0) {
      challenge.countdown = 'Countdown expired';
    } else {
      const days = Math.floor(timeDifference / (1000 * 60 * 60 * 24));
      const hours = Math.floor((timeDifference % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
      const minutes = Math.floor((timeDifference % (1000 * 60 * 60)) / (1000 * 60));
      const seconds = Math.floor((timeDifference % (1000 * 60)) / 1000);
  
      challenge.countdown = `${days} days ${hours} hours ${minutes} minutes ${seconds} seconds`;
    }
  }
  
}
