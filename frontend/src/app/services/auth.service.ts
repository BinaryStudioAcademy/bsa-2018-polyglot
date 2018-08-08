import { Injectable } from '@angular/core';
import { AngularFireAuth } from 'angularfire2/auth';
import * as firebase from 'firebase/app';
import { Observable, from } from 'rxjs';
import { Router } from '@angular/router';
import { SessionStorage } from "ngx-store";

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
        this.updateStorage(await this._firebaseAuth.auth.currentUser.getIdToken(), true);
        this.router.navigate(['/dashboard']);
      });
    }
  }

  signInWithFacebook() {
    if (!this.isLoggedIn()) {
      return this._firebaseAuth.auth.signInWithPopup(
        new firebase.auth.FacebookAuthProvider()
      ).then(async () => {
        this.updateStorage(await this._firebaseAuth.auth.currentUser.getIdToken(), true);
        this.router.navigate(['/dashboard']);
      });
    }
  }

  signUpRegular(email: string, password: string, name: string) {
    return this._firebaseAuth.auth.createUserWithEmailAndPassword(email, password)
    .then(() => this._firebaseAuth.auth.currentUser
      .updateProfile({displayName: name, photoURL: this._firebaseAuth.auth.currentUser.photoURL}))
    .then(async () => {
      this.updateStorage(await this._firebaseAuth.auth.currentUser.getIdToken(), true);
      this.router.navigate(['/dashboard']);
    });
  }


  signInRegular(email: string, password: string) {
    if (!this.isLoggedIn()) {
      return this._firebaseAuth.auth.signInWithEmailAndPassword(email, password).then(async () => {
        this.updateStorage(await this._firebaseAuth.auth.currentUser.getIdToken(), true);
        this.router.navigate(['/dashboard']);
      });
    }
  }

  isLoggedIn() : boolean{
    return this.isLogged;
  }

  logout() {
    if (this.isLoggedIn()) {
      this._firebaseAuth.auth.signOut();
      this.updateStorage("", false);
      this.router.navigate(['/']);
    }
  }

  private updateStorage(token : string, isLogged : boolean){
    this.token = token;
    this.isLogged = isLogged; //Don`t fix, it shold be twice
    this.token = token;       //(yes, in docs written do like it, I laughed a lot)
    this.isLogged = isLogged;
  }

  
  sendEmailVerification() {
    if (this.isLoggedIn && !this.userDetails.emailVerified) {
      this._firebaseAuth.auth.currentUser.sendEmailVerification();
    }
  }
}