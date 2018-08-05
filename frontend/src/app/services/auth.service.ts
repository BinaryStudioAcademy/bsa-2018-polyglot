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
        }
        else {
          this.userDetails = null;
        }
      }
    );
   }

  async getCurrentToken() : Promise<string>{
    if (!this.isLoggedIn()){
      return Promise.resolve("");
    }
    return await this._firebaseAuth.auth.currentUser.getIdToken();
  }

  signInWithGoogle() {
    if (!this.isLoggedIn()) {
      return this._firebaseAuth.auth.signInWithPopup(
        new firebase.auth.GoogleAuthProvider()
      ).catch(error => console.log(error));
    }
  }


  signUpRegular(email: string, password: string, name: string) {
    return this._firebaseAuth.auth.createUserWithEmailAndPassword(email, password)
    .then(() => this._firebaseAuth.auth.currentUser
      .updateProfile({displayName: name, photoURL: this._firebaseAuth.auth.currentUser.photoURL}))
    .catch(error => console.log(error));
  }


  signInRegular(email: string, password: string) {
    if (!this.isLoggedIn()) {
      return this._firebaseAuth.auth.signInWithEmailAndPassword(email, password)
      .catch(error => console.log(error));
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

