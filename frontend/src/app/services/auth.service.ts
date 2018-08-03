import { Injectable } from '@angular/core';

import { AngularFireAuth } from 'angularfire2/auth';
import * as firebase from 'firebase/app';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private user: Observable<firebase.User>;
  private userDetails: firebase.User = null;

  constructor(private _firebaseAuth: AngularFireAuth) {
    this.user = _firebaseAuth.authState;
    this.user.subscribe(
      (user) => {
        if (user) {
          this.userDetails = user;
          console.log(this.userDetails);
        }
        else {
          this.userDetails = null;
        }
      }
    );
   }

   //TODO: Implement try-catch block with exceptions handle
  signInWithGoogle() {
    if (!this.isLoggedIn()) {
      return this._firebaseAuth.auth.signInWithPopup(
        new firebase.auth.GoogleAuthProvider()
      );
    }
  }

  //TODO: Implement try-catch block with exceptions handle
  signUpRegular(email: string, password: string) {
    return this._firebaseAuth.auth.createUserWithEmailAndPassword(email, password).then(
      // TODO: do email verification in better way
      () => this._firebaseAuth.auth.currentUser.sendEmailVerification()
    );
  }

  //TODO: Implement try-catch block with exceptions handle
  signInRegular(email: string, password: string) {
    if (!this.isLoggedIn()) {
      return this._firebaseAuth.auth.signInWithEmailAndPassword(email, password);
    }
  }

  isLoggedIn() {
    if (this.userDetails == null ) {
      return false;
    } else {
      return true;
    }
  }

  logout() {
    if (this.isLoggedIn()) {
      this._firebaseAuth.auth.signOut();
    }
  }
}

