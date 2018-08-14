import { Component, OnInit } from '@angular/core';
import { IUserSignUp } from '../../models/user-signup';
import { AuthService } from '../../services/auth.service';
import { MatDialogRef } from '@angular/material';
import { Router } from '@angular/router';
import { SnotifyService, SnotifyPosition, SnotifyToastConfig } from 'ng-snotify';
import { AppStateService } from '../../services/app-state.service';

@Component({
  selector: 'app-signup-dialog',
  templateUrl: './signup-dialog.component.html',
  styleUrls: ['./signup-dialog.component.sass']
})
export class SignupDialogComponent implements OnInit {

  public user: IUserSignUp;
  public firebaseError: string;
  public selectedOption : string;
  private IsNotificationSend: boolean;

  constructor(
    public dialogRef: MatDialogRef<SignupDialogComponent>,
    private authService : AuthService,
    private router: Router,
    private snotify: SnotifyService,
    private appState: AppStateService
  ) { }

  ngOnInit() {
    this.user = {
      email: '',
      password: '',
      repeatPass: '',
      fullname: ''
    };

    this.selectedOption = "translator";

    this.IsNotificationSend = false;
  }

  async onSignUpFormSubmit(user: IUserSignUp, form) {
    if (form.valid) {
      await this.authService.signUpRegular(user.email, user.password).then(
        // () => this.router.navigate(['/profile/settings'])
        () => {
          // email confirmation
          if (!this.IsNotificationSend) {
            this.authService.sendEmailVerification();
            this.snotify.clear();
            this.snotify.info(`Email confirmation was send to ${this.appState.currentFirebaseUser.email}`, {
              timeout: 10000,
              showProgressBar: true,
              closeOnClick: false,
              pauseOnHover: false        
            });
            this.authService.logout();
            setTimeout(
              () => this.dialogRef.close(), 
              10000
            );
            this.IsNotificationSend = true;
          } else {
            this.snotify.clear();
            this.snotify.warning(`Email confirmation was already send to ${this.appState.currentFirebaseUser.email}. Check your email.`, {
              timeout: 10000,
              showProgressBar: true,
              closeOnClick: false,
              pauseOnHover: false        
            });
          }
        }
      ).catch(
        (error) => this.firebaseError = error.message
      );
      if(this.authService.isLoggedIn()){
        this.dialogRef.close();
      }
    }
  }

  async onGoogleClick() {
    await this.authService.signInWithGoogle().then(
      // if exist in db - show error
      () => this.router.navigate(['/profile/settings'])
    ).catch(
      (error) => this.firebaseError = error.message
    );
    if(this.authService.isLoggedIn()){
      this.dialogRef.close();
    }
  }

  async onFacebookClick() {
    await this.authService.signInWithFacebook().then(
      // if exist in db - show error
      () => this.router.navigate(['/profile/settings'])
    ).catch(
      (error) => this.firebaseError = error.message
    );
    if(this.authService.isLoggedIn()){
      this.dialogRef.close();
    }
  }

}
