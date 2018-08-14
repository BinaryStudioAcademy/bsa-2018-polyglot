import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { UserProfile } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AppStateService {
  
  // Firebase user
  public currentFirebaseUser: firebase.User;

  // Database user
  public currentDatabaseUser: UserProfile;

  // Firebase token
  public currentFirebaseToken: string;

  // Login status
  public isLoggedIn: boolean;


  constructor(private authService: AuthService) { 

    this.authService.getCurrentUser().subscribe(
      (user) => {
        if (user) {
          this.currentFirebaseUser = user;
        } else {
          this.currentFirebaseUser = null;
        }
      }
    );

    this.authService.getCurrentToken().subscribe(
      (token) => {
        if (token) {
          this.currentFirebaseToken = token;
        } else {
          this.currentFirebaseToken = null;
        }
      }
    );

    this.authService.getCurrentUser().subscribe(
      (user) => this.isLoggedIn = user != undefined && user != null
    );
  }
}
