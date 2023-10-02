import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Modal } from 'flowbite'
import type { ModalOptions, ModalInterface } from 'flowbite'
import { JwtHelperService } from '@auth0/angular-jwt';
import jwt_decode from "jwt-decode";
import { Tabs } from 'flowbite';
import { Router } from '@angular/router';
import { RewardArchitectService } from '../guards/reward-architect.service';
import { ChallengerGaurdService } from '../guards/challenger-gaurd.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements AfterViewInit, OnInit {
  user = { name: '', surname: '', profilePicture: '', userName: '' };
  admin : boolean = false
  reward : boolean = false
  chall : boolean = false

  ngOnInit(): void {
    this.tabs.show('dashboard');

    if(this.rewardArci.canActivate()){
      this.reward = true
      
    }

    if(this.challServ.canActivate()){
      this.chall = true
      
    }

    if(localStorage.getItem('admin') == 'true'){
      this.admin = true
      
    }

  }

  constructor(private router : Router, private rewardArci : RewardArchitectService, private challServ: ChallengerGaurdService ) {
   
  }

  signOut() {
    localStorage.clear()
    this.router.navigate(['/login']).then(() => {
      location.reload()
    })

  }
  // tabs list 
  tabElements: any[] = []
  options = {
    defaultTabId: 'settings',
    activeClasses: 'text-blue-600 hover:text-blue-600 dark:text-blue-500 dark:hover:text-blue-400 border-blue-600 dark:border-blue-500',
    inactiveClasses: 'text-gray-500 hover:text-gray-600 dark:text-gray-400 border-gray-100 hover:border-gray-300 dark:border-gray-700 dark:hover:text-gray-300',
    onShow: () => {
      console.log('tab is shown');
    }
  };

  tabs = new Tabs(this.tabElements, this.options);

  ngAfterViewInit(): void {

    // Tabs 
    this.tabElements = [
      {
        id: 'profile',
        triggerEl: document.querySelector('#profile-tab'),
        targetEl: document.querySelector('#profile')
      },
      {
        id: 'dashboard',
        triggerEl: document.querySelector('#dashboard-tab'),
        targetEl: document.querySelector('#dashboard')
      },
      {
        id: 'settings',
        triggerEl: document.querySelector('#settings-tab'),
        targetEl: document.querySelector('#settings')
      },
      {
        id: 'contacts',
        triggerEl: document.querySelector('#contacts-tab'),
        targetEl: document.querySelector('#contacts')
      }
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

    this.tabs.show('dashboard');


    var token = localStorage.getItem('token')!;
    interface TokenData {
      'http://schemas.microsoft.com/ws/2008/06/identity/claims/role': string;
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress': string;
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier': string;
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name': string;
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname': string;
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth': string;
      exp: number;
      iss: string;
      aud: string;
    }
    var decoded: TokenData = jwt_decode(token);

    console.log(decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);

  }


  onProfileTab() {
    this.tabs.show('dashboard');

  }
}