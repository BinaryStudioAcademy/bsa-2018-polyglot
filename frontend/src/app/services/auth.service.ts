import { Injectable } from '@angular/core';
import { AngularFireAuth } from 'angularfire2/auth';
import * as firebase from 'firebase/app';
import { Observable, from } from 'rxjs';
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

  getCurrentToken() : Observable<string>{
    if (!this.isLoggedIn()){
      return from(Promise.resolve(""));
    }
    return from(this._firebaseAuth.auth.currentUser.getIdToken());
  }

  signInWithGoogle() {
    if (!this.isLoggedIn()) {
      return this._firebaseAuth.auth.signInWithPopup(
        new firebase.auth.GoogleAuthProvider()
      );
    }
  }

  signInWithFacebook() {
    if (!this.isLoggedIn()) {
      return this._firebaseAuth.auth.signInWithPopup(
        new firebase.auth.FacebookAuthProvider()
      );
    }
  }

  signUpRegular(email: string, password: string, name: string) {
    return this._firebaseAuth.auth.createUserWithEmailAndPassword(email, password)
    .then(() => this._firebaseAuth.auth.currentUser
      .updateProfile({displayName: name, photoURL: this._firebaseAuth.auth.currentUser.photoURL}));
  }


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