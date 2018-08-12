import { Component, OnInit } from '@angular/core';
import { IUserSignUp } from '../../models/user-signup';
import { AuthService } from '../../services/auth.service';
import { MatDialogRef } from '@angular/material';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup-dialog',
  templateUrl: './signup-dialog.component.html',
  styleUrls: ['./signup-dialog.component.sass']
})
export class SignupDialogComponent implements OnInit {

  public user: IUserSignUp;
  public firebaseError: string;
  public selectedOption : string

  constructor(
    public dialogRef: MatDialogRef<SignupDialogComponent>,
    private authService : AuthService,
    private router: Router
  ) { }

  ngOnInit() {
    this.user = {
      email: '',
      password: '',
      repeatPass: '',
      fullname: ''
    };

    this.selectedOption = "translator";
  }

  async onSignUpFormSubmit(user: IUserSignUp, form) {
    if (form.valid) {
      await this.authService.signUpRegular(user.email, user.password, user.fullname).then(
        () => this.router.navigate(['/profile/settings'])
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
      () => this.router.navigate(['/profile/settings'])
    ).catch(
      (error) => this.firebaseError = error.message
    );
    if(this.authService.isLoggedIn()){
      this.dialogRef.close();
    }
  }

}
