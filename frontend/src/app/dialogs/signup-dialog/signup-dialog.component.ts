import { Component, OnInit } from '@angular/core';
import { IUserSignUp } from '../../models/user-signup';

@Component({
  selector: 'app-signup-dialog',
  templateUrl: './signup-dialog.component.html',
  styleUrls: ['./signup-dialog.component.sass']
})
export class SignupDialogComponent implements OnInit {

  private user: IUserSignUp;

  constructor() { }

  ngOnInit() {
    this.user = {
      email: '',
      password: '',
      fullname: ''
    }
  }

  onSignUpFormSubmit(user: IUserSignUp) {
    // when form submited you will get all data from it
    // here can you make all auth logic and then navigate
    // to other page ... or when error -> show it

    console.log(user);
  }

  onGoogleClick() {
    // google sign in
  }

}
