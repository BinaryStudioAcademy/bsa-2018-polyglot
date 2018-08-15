import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MatDialog } from '@angular/material';
import { IUserLogin } from '../../models';
import { AuthService } from '../../services/auth.service';
import { SnotifyService } from 'ng-snotify';
import { ForgotPasswordDialogComponent } from '../forgot-password-dialog/forgot-password-dialog.component';
import { AppStateService } from '../../services/app-state.service';

@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.sass']
})
export class LoginDialogComponent implements OnInit {

  public user: IUserLogin;
  public firebaseError: string;
  hide = true;
  notificationConfig = {
    timeout: 15000,
    showProgressBar: true,
    closeOnClick: false,
    pauseOnHover: false
  }

  constructor(
    public dialogRef: MatDialogRef<LoginDialogComponent>,
    private authService : AuthService,
    private snotify: SnotifyService,
    public dialog: MatDialog,
    private appState: AppStateService
  ) { }

  ngOnInit() {
    this.user = {
      email: '',
      password: ''
    };
  }

  onLoginFormSubmit(user: IUserLogin, form) {
    if (form.valid) {
      this.authService.signInRegular(user.email, user.password).subscribe(
        async (userCred) => {
          if (userCred.user.emailVerified) {
            this.appState.updateState(userCred.user, await userCred.user.getIdToken(), true);   

            if(this.appState.LoginStatus){
              this.dialogRef.close();
            }          
          } else {
            this.snotify.clear();
            this.snotify.warning(`Email confirmation was already send to ${userCred.user.email}. Check your email.`, {
              timeout: 15000,
              showProgressBar: true,
              closeOnClick: false,
              pauseOnHover: false,
              buttons: [
                {text: 'Resend', action: () => {
                  userCred.user.sendEmailVerification();
                  this.authService.logout();
                  this.snotify.clear();
                  this.snotify.info(`Email confirmation was send to ${userCred.user.email}`, this.notificationConfig);
                }}
              ]
            }
            );
            this.firebaseError = 'You need to confirm your email address in order to use our service';
          }
        }, 
        (err) => {
          this.firebaseError = this.handleFirebaseErrors(err);
        }
      );
    }
  }

  onGoogleClick() {
    this.authService.signInWithGoogle().subscribe(
      async (userCred) => {
        this.appState.updateState(userCred.user, await userCred.user.getIdToken(), true);

        if(this.appState.LoginStatus) {
          this.dialogRef.close();
        }

        // if not exist in db - show error
      }, 
      (err) => {
        this.firebaseError = this.handleFirebaseErrors(err);
      }
    );
  }

  onFacebookClick() {
    this.authService.signInWithFacebook().subscribe(
      async (userCred) => {
        this.appState.updateState(userCred.user, await userCred.user.getIdToken(), true);

        if(this.appState.LoginStatus) {
          this.dialogRef.close();
        }

        // if not exist in db - show error
      }, 
      (err) => {
        this.firebaseError = this.handleFirebaseErrors(err);
      }
    );
  }

  onForgotPasswordClick() {
    this.dialogRef.close();
    this.dialog.open(ForgotPasswordDialogComponent);
  }

  private handleFirebaseErrors(error) {
    var result;
    switch(error.code) {
      case 'auth/wrong-password': 
      case 'auth/user-not-found':
        result = 'Wrong email or password';
        break;
      default: 
        result = error.message;
        break;
    }
    return result;
  }
}
