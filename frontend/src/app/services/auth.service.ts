import { Injectable } from '@angular/core';
import { AngularFireAuth } from 'angularfire2/auth';
import * as firebase from 'firebase/app';
import { Observable, from } from 'rxjs';
import { Router } from '@angular/router';
import { SessionStorage } from "ngx-store";
import { async } from 'q';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private user: Observable<firebase.User>;
  private userDetails: firebase.User = null;
  @SessionStorage() token: string;
  @SessionStorage() isLogged: boolean;

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

  getCurrentToken() : string{
    return this.token;
  }

  signInWithGoogle() {
    if (!this.isLoggedIn()) {
      return this._firebaseAuth.auth.signInWithPopup(
        new firebase.auth.GoogleAuthProvider()
      ).then(async () => {
        this.token = await this._firebaseAuth.auth.currentUser.getIdToken();
        this.isLogged = true;
      });
    }
  }

  signInWithFacebook() {
    if (!this.isLoggedIn()) {
      return this._firebaseAuth.auth.signInWithPopup(
        new firebase.auth.FacebookAuthProvider()
      ).then(async () => {
        this.token = await this._firebaseAuth.auth.currentUser.getIdToken();
        this.isLogged = true;
      });
    }
  }

  signUpRegular(email: string, password: string, name: string) {
    return this._firebaseAuth.auth.createUserWithEmailAndPassword(email, password)
    .then(() => this._firebaseAuth.auth.currentUser
      .updateProfile({displayName: name, photoURL: this._firebaseAuth.auth.currentUser.photoURL}))
    .then(async () => {
      this.token = await this._firebaseAuth.auth.currentUser.getIdToken();
      this.isLogged = true;
    });
  }


  signInRegular(email: string, password: string) {
    if (!this.isLoggedIn()) {
      return this._firebaseAuth.auth.signInWithEmailAndPassword(email, password).then(async () => {
        this.token = await this._firebaseAuth.auth.currentUser.getIdToken();
        this.isLogged = true;
      });
    }
  }

  isLoggedIn() : boolean{
    return this.isLogged;
  }

  logout() {
    if (this.isLoggedIn()) {
      this._firebaseAuth.auth.signOut().then(async () => {
        this.token = "";
        this.isLogged = false;
        this.router.navigate(['/']);
      });
    }
  }

  sendEmailVerification() {
    if (this.isLoggedIn && !this.userDetails.emailVerified) {
      this._firebaseAuth.auth.currentUser.sendEmailVerification();
    }
  }
}