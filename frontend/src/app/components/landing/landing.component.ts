import { Component, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';
import { MatDialog } from '@angular/material';
import { AuthService } from '../../services/auth.service';
import { Router } from '../../../../node_modules/@angular/router';
import { EventService } from '../../services/event.service';

@Component({
  selector: 'app-root',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.sass'],
  providers: [AuthService]
})
export class LandingComponent implements OnInit {
  title: string;

  constructor(
    public dialog: MatDialog,
    public router: Router,
    private eventService: EventService
  ) {
  }

  ngOnInit() {
    document.body.classList.add('bg-image');
  }

  onSignUpClick() {
    this.eventService.filter('signUp');
  }

  onLoginClick() {
    this.eventService.filter('login');
  }
}
