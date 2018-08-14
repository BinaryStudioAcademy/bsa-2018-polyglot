import { Injectable } from '@angular/core';
import { AngularFireAuth } from 'angularfire2/auth';
import * as firebase from 'firebase/app';
import { AppStateService } from './app-state.service';
import { from, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    private isLogged: boolean;

    constructor(private _firebaseAuth: AngularFireAuth) { 
        var currentUser;
        this.getCurrentUser().subscribe(
            (user) => currentUser = user
        );       
        this.isLogged = currentUser != undefined;
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

    signUpRegular(email: string, password: string) {
        return this._firebaseAuth.auth.createUserWithEmailAndPassword(email, password);
    }


    signInRegular(email: string, password: string) {
        if (!this.isLoggedIn()) {
            return this._firebaseAuth.auth.signInWithEmailAndPassword(email, password);
        }
    }

    isLoggedIn(): boolean {
        return this.isLogged;
    }

    logout() {
        if (this.isLoggedIn()) {
            this._firebaseAuth.auth.signOut();
        }
    }

    sendEmailVerification() {
        if (this.isLoggedIn()) {
            this._firebaseAuth.auth.currentUser.sendEmailVerification();
        }
    }

    getCurrentUser() : Observable<firebase.User> {
        return this._firebaseAuth.authState;
    }

    getCurrentToken() : Observable<string> {
        if (this.isLoggedIn()) {
            return from(this._firebaseAuth.auth.currentUser.getIdToken());
        }
        return from(Promise.resolve(''));
    }

    sendResetPasswordConfirmation(email: string) {
        if (this.isLoggedIn()) {
            this._firebaseAuth.auth.sendPasswordResetEmail(email);
        }
    }
}