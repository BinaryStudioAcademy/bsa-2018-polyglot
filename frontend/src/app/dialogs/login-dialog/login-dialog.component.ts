import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { IUserLogin } from '../../models';

@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.sass']
})
export class LoginDialogComponent implements OnInit {

  public user: IUserLogin;

  constructor(
    public dialogRef: MatDialogRef<LoginDialogComponent>
  ) { }

  ngOnInit() {
    this.user = {
      email: '',
      password: ''
    };
  }

  onLoginFormSubmit(user: IUserLogin) {
    // when form submited you will get all data from it
    // here can you make all auth logic and then navigate
    // to other page ... or when error -> show it

    console.log(user);
  }

  onGoogleClick() {
    // google sign in
  }

}
