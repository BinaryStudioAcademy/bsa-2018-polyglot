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
          this.appState.updateState(userCred.user, await userCred.user.getIdToken(), true);   

          if(this.authService.isLoggedIn()){
            this.dialogRef.close();
          }
        }, 
        (err) => {
          this.firebaseError = this.handleFirebaseErrors(err);
        }
      );
      // .then(
      //   () => {
      //     if(!this.appState.currentFirebaseUser.emailVerified) {
      //         // email confirmation
      //       this.snotify.clear();
      //       this.snotify.warning(`Email confirmation was already send to ${this.appState.currentFirebaseUser.email}. Check your email.`, {
      //         timeout: 15000,
      //         showProgressBar: true,
      //         closeOnClick: false,
      //         pauseOnHover: false,
      //         buttons: [
      //           {text: "Resend", action: async () => {
      //             // resend confirmation to user
      //             await this.authService.signInRegular(user.email, user.password);
      //             this.authService.sendEmailVerification();
      //             this.authService.logout();
      //             this.snotify.clear();
      //             this.snotify.info(`Email confirmation was send to ${this.appState.currentFirebaseUser.email}`, {
      //               timeout: 15000,
      //               showProgressBar: true,
      //               closeOnClick: false,
      //               pauseOnHover: false
      //             });
      //           }}
      //         ]        
      //       });
      //       this.authService.logout();
      //       throw {message: 'You need to confirm your email address in order to use our service'};
      //     } else {
      //       // if not exist in db - send post request and navigate to settings
      //     }
      //   }
      // )
    }
  }

  onGoogleClick() {
    this.authService.signInWithGoogle().subscribe(
      async (userCred) => {
        this.appState.updateState(userCred.user, await userCred.user.getIdToken(), true);

        if(this.authService.isLoggedIn()){
          this.dialogRef.close();
        }
      }, 
      (err) => {
        this.firebaseError = this.handleFirebaseErrors(err);
      }
    );
    // .then(
    //   () => {
    //     // if not exist in db - show error
    //   }
    // ) 
  }

  onFacebookClick() {
    this.authService.signInWithFacebook().subscribe(
      async (userCred) => {
        this.appState.updateState(userCred.user, await userCred.user.getIdToken(), true);

        if(this.authService.isLoggedIn()){
          this.dialogRef.close();
        }
      }, 
      (err) => {
        this.firebaseError = this.handleFirebaseErrors(err);
      }
    );
    // .then(
    //   () => {
    //     // if not exist in db - show error
    //   }
    // )
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
