import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MatDialog } from '@angular/material';
import { IUserLogin } from '../../models';
import { AuthService } from '../../services/auth.service';
import { SnotifyService } from 'ng-snotify';
import { ForgotPasswordDialogComponent } from '../forgot-password-dialog/forgot-password-dialog.component';

@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.sass']
})
export class LoginDialogComponent implements OnInit {

  public user: IUserLogin;
  public firebaseError: string;

  constructor(
    public dialogRef: MatDialogRef<LoginDialogComponent>,
    private authService : AuthService,
    private snotify: SnotifyService,
    public dialog: MatDialog
  ) { }

  ngOnInit() {
    this.user = {
      email: '',
      password: ''
    };
  }

  async onLoginFormSubmit(user: IUserLogin, form) {
    if (form.valid) {
      await this.authService.signInRegular(user.email, user.password).then(
        () => {
          if(!this.authService.getCurrentUser().emailVerified) {
              // email confirmation
            this.authService.sendEmailVerification();
            this.snotify.info(`Email confirmation was send to ${this.authService.getCurrentUser().email}`, {
              timeout: 10000,
              showProgressBar: true,
              closeOnClick: false,
              pauseOnHover: false        
            });
            this.authService.logout();
            throw {message: 'You need to confirm your email address in order to use our service'};
          } else {
            // if not exist in db - send post request and navigate to settings
          }
        }
      ).catch(
        (error) => this.firebaseError = this.handleFirebaseErrors(error)
      );
      if(this.authService.isLoggedIn()){
        this.dialogRef.close();
      }
    }
  }

  async onGoogleClick() {
    await this.authService.signInWithGoogle().then(
      () => {
        // if not exist in db - show error
      }
    ).catch(
      (error) => this.firebaseError = error.message
    );
    if(this.authService.isLoggedIn()){
      this.dialogRef.close();
    }
  }

  async onFacebookClick() {
    await this.authService.signInWithFacebook().then(
      () => {
        // if not exist in db - show error
      }
    ).catch(
      (error) => this.firebaseError = error.message
    );
    if(this.authService.isLoggedIn()){
      this.dialogRef.close();
    }
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
