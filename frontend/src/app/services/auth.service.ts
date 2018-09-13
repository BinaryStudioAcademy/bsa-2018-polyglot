import { Injectable } from '@angular/core';
import { AngularFireAuth } from 'angularfire2/auth';
import * as firebase from 'firebase';
import { AppStateService } from './app-state.service';
import { from, Observable, of } from 'rxjs';
import { first, tap } from 'rxjs/operators';
import { UserService } from './user.service';
import { Router } from '@angular/router';
import { async } from '@angular/core/testing';

@Injectable({
    providedIn: 'root'
})
export class AuthService {


    constructor(private _firebaseAuth: AngularFireAuth,
        private appState: AppStateService,
        private router: Router) {
            this._firebaseAuth.auth.onAuthStateChanged(async (user) => {
                if(user){
                    await this.refreshToken();
                    this.appState.currentFirebaseUser = user;
                }
            });
    }

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
        return from(this._firebaseAuth.auth.signOut()).pipe(
            tap(() => {
                this.appState.updateState(null, '', false, null);
                this.router.navigate(['/']);
            }));
    }

    sendEmailVerification() {
        this._firebaseAuth.auth.currentUser.sendEmailVerification();
    }

    sendResetPasswordConfirmation(email: string) {
        this._firebaseAuth.auth.sendPasswordResetEmail(email);
    }

    async refreshToken() {
        if (this._firebaseAuth.auth.currentUser) {
            const firebaseToken = await this._firebaseAuth.auth.currentUser.getIdToken(true);
            this.appState.currentFirebaseToken = firebaseToken;
        }
    }
}
