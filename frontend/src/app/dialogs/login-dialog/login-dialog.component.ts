import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { IUserLogin } from '../../models';
import { AuthService } from '../../services/auth.service';

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
    private authService : AuthService
  ) { }

  ngOnInit() {
    this.user = {
      email: '',
      password: ''
    };
  }

  async onLoginFormSubmit(user: IUserLogin, form) {
    if (form.valid) {
      await this.authService.signInRegular(user.email, user.password).catch(
        (error) => this.firebaseError = this.handleFirebaseErrors(error)
      );
      if(this.authService.isLoggedIn()){
        this.dialogRef.close();
      }
    }
  }

  async onGoogleClick() {
    await this.authService.signInWithGoogle().catch(
      (error) => this.firebaseError = error.message
    );
    if(this.authService.isLoggedIn()){
      this.dialogRef.close();
    }
  }

  async onFacebookClick() {
    await this.authService.signInWithFacebook().catch(
      (error) => this.firebaseError = error.message
    );
    if(this.authService.isLoggedIn()){
      this.dialogRef.close();
    }
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
