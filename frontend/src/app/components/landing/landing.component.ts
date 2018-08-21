import { Component, OnInit } from '@angular/core';
import { LoginDialogComponent } from '../../dialogs/login-dialog/login-dialog.component';
import { SignupDialogComponent } from '../../dialogs/signup-dialog/signup-dialog.component';
import { MatDialog } from '@angular/material';
import { AuthService } from '../../services/auth.service';
import { Router } from '../../../../node_modules/@angular/router';
import { AppStateService } from '../../services/app-state.service';
import { UserService } from '../../services/user.service';

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
    private appState: AppStateService,
    private userService: UserService
  ) {
  }

  ngOnInit() {
    document.body.classList.add('bg-image');
    this.updateCurrentUser();
  }

  onSignUpClick() {
    let dialogRef = this.dialog.open(SignupDialogComponent);
    dialogRef.componentInstance.reloadEvent.subscribe(
      () => {
        this.updateCurrentUser();
      }
    );
  }

  onLoginClick() {
    let dialogRef = this.dialog.open(LoginDialogComponent);  
    dialogRef.componentInstance.reloadEvent.subscribe(
      () => {
        this.updateCurrentUser();
      }
    );
  }

  private updateCurrentUser() {
    if (this.appState.LoginStatus){
      if (!this.userService.getCurrrentUser()) {
        this.userService.getUser().subscribe(
          (user)=> {
            this.userService.updateCurrrentUser(user);   
          },
          err => {
            console.log('err', err);
          }
        );
      }
    }
  }
}
