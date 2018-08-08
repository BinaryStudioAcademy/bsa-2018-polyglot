import { Component } from '@angular/core';
import { MatDialog } from '@angular/material';
import { LoginDialogComponent } from 'src/app/dialogs/login-dialog/login-dialog.component';
import { SignupDialogComponent } from 'src/app/dialogs/signup-dialog/signup-dialog.component';
import { AuthService } from '../../services/auth.service';


@Component({
  providers: [AuthService],
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.sass']
})
export class NavigationComponent {

  constructor(
    public dialog: MatDialog,
    private authService: AuthService
  ) { }

  onLoginClick() {
    this.dialog.open(LoginDialogComponent);
  }

  onSignUpClick() {
    this.dialog.open(SignupDialogComponent);
  }

  onLogoutClick() {
    this.authService.logout();
  }

  isLoggedIn() {
    return this.authService.isLoggedIn();
  }
}
