import { Component } from '@angular/core';
import { MatDialog } from '@angular/material';
import { DataService } from './services/data.service';
import { UserService } from './services/user.service';
import { LoginDialogComponent } from './dialogs/login-dialog/login-dialog.component';
import { SignupDialogComponent } from './dialogs/signup-dialog/signup-dialog.component';

@Component({
  selector: 'app-root',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.sass']
})
export class LandingComponent {
  title: string;

  constructor(
    public dialog: MatDialog
  ) {

  }

  onLoginClick() {
    this.dialog.open(LoginDialogComponent);
  }

  onSignUpClick() {
    this.dialog.open(SignupDialogComponent);
  }

  ngOnInit() {
    this.title = "Polyglot";

  }
}
