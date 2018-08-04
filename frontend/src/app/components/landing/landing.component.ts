import { Component } from '@angular/core';
//import { DataService } from './services/data.service';

@Component({
  selector: 'app-root',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.sass']
})
export class LandingComponent {
  title: string;

  constructor() { 

  }

  ngOnInit() {
    this.title = "Poliglot";
  }
}
