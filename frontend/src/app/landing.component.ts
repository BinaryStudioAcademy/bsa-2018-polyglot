import { Component } from '@angular/core';
import { DataService } from './services/data.service';
import { UserService } from './services/user.service';

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
    this.title = "Polyglot";

  }
}
