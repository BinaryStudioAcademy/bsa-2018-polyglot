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
  private notificationConfig = {
    timeout: 10000,
    showProgressBar: true,
    closeOnClick: false,
    pauseOnHover: false        
  }

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

  onSignUpFormSubmit(user: IUserSignUp, form) {
    if (form.valid) {
      this.authService.signUpRegular(user.email, user.password).subscribe(
        (userCred) => {
          this.authService.sendEmailVerification();
          this.snotify.clear();
          this.snotify.info(`Email confirmation was send to ${userCred.user.email}`, this.notificationConfig);  
          this.IsNotificationSend = true;   
          setTimeout(
            () => this.dialogRef.close(), 
            10000
          );
        }, 
        (err) => {
          if (err.code === 'auth/email-already-in-use') {
            this.snotify.clear();
            this.snotify.warning(`Email confirmation was already send to ${this.user.email}. Check your email.`, this.notificationConfig);
          }
          this.firebaseError = err.message;
        }
      );
      // not sure that it is logging out 
      this.authService.logout();
    }
  }

  onGoogleClick() {
    this.authService.signInWithGoogle().subscribe(
      async (userCred) => {
        this.appState.updateState(userCred.user, await userCred.user.getIdToken(), true);

        if(this.appState.LoginStatus){
            this.dialogRef.close();
        }

        //if exist in db - show error
        this.router.navigate(['/profile/settings']);
      }, 
      (err) => {
        this.firebaseError = err.message;
      }
    );
  }

  onFacebookClick() {
    this.authService.signInWithFacebook().subscribe(
      async (userCred) => {
        this.appState.updateState(userCred.user, await userCred.user.getIdToken(), true);

        if(this.appState.LoginStatus){
          this.dialogRef.close();
        }

        //if exist in db - show error
        this.router.navigate(['/profile/settings']);
      }, 
      (err) => {
        this.firebaseError = err.message;
      }
    );  
  }
}
