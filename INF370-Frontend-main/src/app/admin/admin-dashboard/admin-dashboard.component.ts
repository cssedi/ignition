import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.scss']
})
export class AdminDashboardComponent implements OnInit {
  title = 'INF370-Frontend';
  admin : boolean = false

  constructor(private router : Router){
  }

  ngOnInit(): void {
    if(localStorage.getItem('admin') == 'true'){
      this.admin = true
    }
    console.log('admin loaded')

    
  }

  ngAfterViewInit(){
    if(localStorage.getItem('admin') == 'true'){
      this.admin = true
    }

    console.log('admin loaded')
  }
 
  AdminSignOut(){
    localStorage.clear()
    this.router.navigate(['/home']).then( () => {
      window.location.reload()
    })
  }
}