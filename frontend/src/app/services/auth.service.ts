import { Injectable } from '@angular/core';
import { AngularFireAuth } from 'angularfire2/auth';
import * as firebase from 'firebase/app';
import { AppStateService } from './app-state.service';
import { from, Observable, of } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    constructor(private _firebaseAuth: AngularFireAuth) { }

    signInWithGoogle() {
        return from(this._firebaseAuth.auth.signInWithPopup(
            new firebase.auth.GoogleAuthProvider()
        ));
    }

    signInWithFacebook() {
        return from(this._firebaseAuth.auth.signInWithPopup(
            new firebase.auth.FacebookAuthProvider()
        ));
    }

    signUpRegular(email: string, password: string) {
        return from(this._firebaseAuth.auth.createUserWithEmailAndPassword(email, password));
    }


    signInRegular(email: string, password: string) {
        return from(this._firebaseAuth.auth.signInWithEmailAndPassword(email, password));
    }

    logout() {
        this._firebaseAuth.auth.signOut();
    }

    sendEmailVerification() {
        this._firebaseAuth.auth.currentUser.sendEmailVerification();
    }

    sendResetPasswordConfirmation(email: string) {
        this._firebaseAuth.auth.sendPasswordResetEmail(email);
    }
}