import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { UserProfile } from '../models';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppStateService {
  
  // Firebase user
  private currentFirebaseUserSubject: BehaviorSubject<firebase.User> = new BehaviorSubject<firebase.User>(null);

  public get currentFirebaseUser(): firebase.User {
    return this.currentFirebaseUserSubject.value; 
  }

  public set currentFirebaseUser(v: firebase.User) {
    this.currentFirebaseUserSubject.next(v);
  }

  // Database user
  public currentDatabaseUser: UserProfile;

  // Firebase token
  private currentFirebaseTokenSubject: BehaviorSubject<string> = new BehaviorSubject<string>('');

  public get currentFirebaseToken(): string {
    return this.currentFirebaseTokenSubject.value; 
  }

  public set currentFirebaseToken(v: string) {
    this.currentFirebaseTokenSubject.next(v);
  }

  // Login status
  private LoginStatusSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  public get LoginStatus(): boolean {
    return this.LoginStatusSubject.value; 
  }

  public set LoginStatus(v: boolean) {
    this.LoginStatusSubject.next(v);
  }


  constructor() {
    // getting from localStorage
    this.currentFirebaseToken = localStorage.getItem('currentFirebaseToken');
    this.LoginStatus = localStorage.getItem('LoginStatus') === 'true';
  }

  updateState(user: firebase.User, token: string, loginStatus: boolean) {
    this.currentFirebaseUser = user;
    this.currentFirebaseToken = token;
    this.LoginStatus = loginStatus;

    //localStorage
    localStorage.setItem('currentFirebaseToken', this.currentFirebaseToken);
    localStorage.setItem('LoginStatus', `${this.LoginStatus}`);
  }
}
