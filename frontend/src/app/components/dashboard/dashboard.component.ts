import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.sass']
})
export class DashboardComponent {
  routeLinks: any[];
  activeLinkIndex = 0;

  constructor(private router: Router) {
    this.routeLinks = [
      {
        label: 'Projects',
        link: 'projects',
        
      }, {
        label: 'Teams',
        link: 'teams',
        
      }, {
        label: 'User Guide',
        link: 'glossaries',
        
      },
      {
        label: 'Strings',
        link: 'strings',
        
      }
    ];
  }
}

