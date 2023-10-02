import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { Tabs } from 'flowbite';
import { AdminService } from 'src/app/services/admin.service';
import { AuthService } from 'src/app/services/auth.service';
import { SocialFeedService } from 'src/app/services/social-feed.service';

@Component({
  selector: 'app-other-profile',
  templateUrl: './other-profile.component.html',
  styleUrls: ['./other-profile.component.scss']
})
export class OtherProfileComponent implements AfterViewInit, OnInit {
  
  tabs!: Tabs
  options: any;
  tabElements: any;
  medals! : any[] 
  challengerId : string = ''
  posts ! : any[]
  user : any = {
    name : '',
    surname:  '',
    userName : '',
    profilePicture : ''
  }
  constructor(private socialFeedService : SocialFeedService,private fb: FormBuilder, private router : Router, private route: ActivatedRoute ){
    this.challengerId = this.route.snapshot.paramMap.get('challengerId')!;
    console.log('Received ID:', this.challengerId);
  }
  ngOnInit(): void {
    this.viewProfile(this.challengerId)
  }
  ngAfterViewInit(): void {
   

    // create an array of objects with the id, trigger element (eg. button), and the content element
     this.tabElements = [
      {
        id: 'profile',
        triggerEl: document.querySelector('#profile-tab-example'),
        targetEl: document.querySelector('#profile-example')
      },
      // {
      //   id: 'dashboard',
      //   triggerEl: document.querySelector('#dashboard-tab-example'),
      //   targetEl: document.querySelector('#dashboard-example')
      // },
      {
        id: 'settings',
        triggerEl: document.querySelector('#settings-tab-example'),
        targetEl: document.querySelector('#settings-example')
      },
      {
        id: 'contacts',
        triggerEl: document.querySelector('#contacts-tab-example'),
        targetEl: document.querySelector('#contacts-example')
      }
    ];

    // options with default values
     this.options = { 
      defaultTabId: 'settings',
      activeClasses: 'text-blue-600 hover:text-blue-600 dark:text-blue-500 dark:hover:text-blue-400 border-blue-600 dark:border-blue-500',
      inactiveClasses: 'text-gray-500 hover:text-gray-600 dark:text-gray-400 border-gray-100 hover:border-gray-300 dark:border-gray-700 dark:hover:text-gray-300',      onShow: () => {
      }
    };

    this.tabs = new Tabs(this.tabElements, this.options);
    this.tabs.show('profile');
     
  
   
  }
  signOut(){
    localStorage.clear()
    this.router.navigate(['/home'])
  }
  viewProfile( challengerId : string ){
    this.socialFeedService.viewOtherUser(challengerId).subscribe({
      next: (value) => {
        this.user = value.challenger
        this.posts = value.posts
        this.medals = value.medals
        console.log('on view profile',this.medals)
      }, 
      error : (error) => {
      console.log('error on like', error.error)
      }
    })
   
  }
}
