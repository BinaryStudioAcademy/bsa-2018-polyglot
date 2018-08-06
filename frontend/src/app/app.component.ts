import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  providers: [AuthService],
  selector: 'app',
  template: '<router-outlet></router-outlet>',
})
export class AppComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
