import { Component, OnInit } from '@angular/core';
import { IUserSignUp } from '../../models/user-signup';
import { AuthService } from '../../services/auth.service';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-signup-dialog',
  templateUrl: './signup-dialog.component.html',
  styleUrls: ['./signup-dialog.component.sass']
})
export class SignupDialogComponent implements OnInit {

  public user: IUserSignUp;
  public firebaseError: string;

  constructor(
    public dialogRef: MatDialogRef<SignupDialogComponent>,
    private authService : AuthService
  ) { }

  ngOnInit() {
    this.user = {
      email: '',
      password: '',
      fullname: ''
    };
  }

  async onSignUpFormSubmit(user: IUserSignUp, form) {
    if (form.valid) {
      await this.authService.signUpRegular(user.email, user.password, user.fullname).catch(
        (error) => this.firebaseError = error.message
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

}
