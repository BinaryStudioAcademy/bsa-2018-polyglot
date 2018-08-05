import { Injectable } from '@angular/core';

import { AngularFireAuth } from 'angularfire2/auth';
import * as firebase from 'firebase/app';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private user: Observable<firebase.User>;
  private userDetails: firebase.User = null;

  constructor(private _firebaseAuth: AngularFireAuth, private router: Router) {
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

  getCurrentToken() : Promise<string>{
    if (!this.isLoggedIn()){
      return Promise.resolve("");
    }
    return this._firebaseAuth.auth.currentUser.getIdToken();
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
    return this._firebaseAuth.auth.createUserWithEmailAndPassword(email, password);
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
      this._firebaseAuth.auth.signOut()
      .then(() => this.router.navigate(['/']));
      
    }
  }

  sendEmailVerification() {
    if (this.isLoggedIn && !this.userDetails.emailVerified) {
      this._firebaseAuth.auth.currentUser.sendEmailVerification();
    }
  }
}

