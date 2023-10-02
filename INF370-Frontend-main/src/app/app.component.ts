import { AfterViewInit, Component, ElementRef, HostListener, OnInit, Renderer2 } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { initFlowbite } from 'flowbite';
import { Location } from '@angular/common';
import { AuthService } from './services/auth.service';
import { RewardArchitectService } from './guards/reward-architect.service';
import { SuperArchitectGuard } from './guards/super-architect/super-architect.guard';
import { HelpDashService } from './services/help-dash.service';
import { Help } from './Models/Help';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  template: `
    <div id="help-popup">
      <!-- Content of your element -->
    </div>
  `
})

export class AppComponent implements AfterViewInit {
  title = 'INF370-Frontend';

  admin : boolean = false
  reward : boolean = false
  // There is no boolean for a normal challenger
  challenger : boolean = false

  isHelpPopupVisible: boolean = false;
  showDescription: boolean = false;
  helps: any[] = [];
  locations!: any[];
  locationId!: any; // Location ID of current place

  constructor(private router : Router, private rewardArci : RewardArchitectService, private superArchitectGuard: SuperArchitectGuard, private helpDashService: HelpDashService, private renderer: Renderer2, private el: ElementRef){
    router.events.subscribe(event => {
      if(event instanceof NavigationEnd)
      {
        this.getHelps()
      }
    }) 
  }
  
  ngOnInit(): void {
    if(this.rewardArci.canActivate() || this.superArchitectGuard.canActivate()){
      this.reward = true
    }

    if(localStorage.getItem('admin') == 'true'){
      this.admin = true
    }
  }

  ngAfterViewInit(){
    if(this.rewardArci.canActivate()){
      this.reward = true
    }
  }

  singOutRewardArchitect() {
   localStorage.clear()
   this.router.navigate(['/home']).then(() => {
    location.reload()
   })
  }
 
  getHelps() {
    try {
      var currentLocation = this.router.url;
      currentLocation = currentLocation.slice(1, currentLocation.length)
      console.log(currentLocation);

      this.helps = [];
      this.locations = [];
      this.locationId = null;

      this.helpDashService.getLocations().subscribe({
        next: (response) => {
          this.locations = response;
        },
        error: (error) => {
          console.error('Error retrieving locations:', error);
        },
        complete: () => {
          console.log('Locations retrieval completed:', this.locations);

          this.locationId = this.locations.find(location => location.name == currentLocation)?.locationId;
      
          console.log("current loc: ", this.locationId)
        }
      });

      this.helpDashService.getContextualHelp(currentLocation).subscribe({
        next: (response) => {
          console.log('Contextual help retrieved successfully:', response);
          this.helps = response;
        },
        error: (error) => {
          console.error('Error retrieving contextual help:', error);
        },
        complete: () => {
          console.log('Contextual help retrieval completed');
        }
      });
    }
    catch (e) {
      console.log('Error', e);
    }
  }

  goToSpecificHelp() {
    this.router.navigate(['help-page'], { queryParams: { elementId: this.locationId } });
    this.isHelpPopupVisible = false;
    this.locationId = null;
  }

  toggleHelpPopup() {
    console.log("this is the current list of helps:", this.helps)
    this.isHelpPopupVisible = !this.isHelpPopupVisible;
  }

  // This opens all descriptions instead of just one
  toggleDescription(help: Help) {
    this.showDescription = !this.showDescription;
  }

}