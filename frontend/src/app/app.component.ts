import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  providers: [AuthService],
  selector: 'app',
  templateUrl: './app.component.html'
  
  
})
export class AppComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
