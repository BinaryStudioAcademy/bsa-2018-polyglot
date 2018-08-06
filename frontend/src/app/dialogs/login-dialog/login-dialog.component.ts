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

  constructor(
    public dialogRef: MatDialogRef<LoginDialogComponent>,
    private authService : AuthService
  ) { }

  ngOnInit() {
    this.user = {
      email: '',
      password: ''
    };
    console.log(this.authService.isLoggedIn());
  }

  async onLoginFormSubmit(user: IUserLogin) {
    await this.authService.signInRegular(user.email, user.password);
    if(this.authService.isLoggedIn()){
      this.dialogRef.close();
    }
  }

  async onGoogleClick() {
    await this.authService.signInWithGoogle();
    if(this.authService.isLoggedIn()){
      this.dialogRef.close();
    }
  }

}
