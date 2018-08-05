import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.sass']
})
export class DashboardComponent  {
  routeLinks: any[];

  constructor(private router: Router) {
    this.routeLinks = [
      {
        label: 'Projects',
        link: 'Projects',
        index: 0
      }, {
        label: 'Teams',
        link: 'Teams',
        index: 1
      }, {
        label: 'Glossaries',
        link: 'Glossaries',
        index: 2
      }
    ];
  }



  }

