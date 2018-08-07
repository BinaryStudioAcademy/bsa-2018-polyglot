import { Component } from '@angular/core';
import { MatDialog } from '@angular/material';
import { LoginDialogComponent } from 'src/app/dialogs/login-dialog/login-dialog.component';
import { SignupDialogComponent } from 'src/app/dialogs/signup-dialog/signup-dialog.component';
import { StringDialogComponent } from 'src/app/dialogs/string-dialog/string-dialog.component';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.sass']
})
export class NavigationComponent {

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

  onNewStrClick() {
    this.dialog.open(StringDialogComponent);
  }

}
