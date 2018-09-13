import { Injectable } from '@angular/core';
import { UserProfile, Role } from '../models';
import { BehaviorSubject } from 'rxjs';
import { WorkspaceState } from '../models/workspace-state';

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

    getFirebaseUser(){
        return this.currentFirebaseUserSubject.asObservable();
    }

    // Database user
    private currentDatabaseUserSubject: BehaviorSubject<UserProfile> = new BehaviorSubject<UserProfile>(/*{ userRole: Role.Translator }*/null);

    public get currentDatabaseUser(): UserProfile {
        return this.currentDatabaseUserSubject.value;
    }

    public set currentDatabaseUser(v: UserProfile) {
        
        this.currentDatabaseUserSubject.next(v);
    }

    getDatabaseUser(){
        return this.currentDatabaseUserSubject.asObservable();
    }
    // Firebase token
    private currentFirebaseTokenSubject: BehaviorSubject<string> = new BehaviorSubject<string>('');

    public get currentFirebaseToken(): string {
        return this.currentFirebaseTokenSubject.value;
    }

    public set currentFirebaseToken(v: string) {
        localStorage.setItem('currentFirebaseToken', v);
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

    private workspaceState: BehaviorSubject<WorkspaceState> = new BehaviorSubject<WorkspaceState>(null);


    public set setWorkspaceState(workspaceState: WorkspaceState) {
        this.workspaceState.next(workspaceState);
        //localStorage.setItem('workspaceState',  JSON.stringify(this.workspaceState.value));
    }
    public get Layout(): string {
        return localStorage.getItem('LayoutView');
    }

    public set Layout(status: string) {
        localStorage.setItem('LayoutView', status);
    }

    public get getWorkspaceState(): WorkspaceState {
        return this.workspaceState.value;
    }

    constructor() {
        // getting from localStorage
        this.currentFirebaseToken = localStorage.getItem('currentFirebaseToken');
        this.LoginStatus = localStorage.getItem('LoginStatus') === 'true';
        //this.workspaceState.next(JSON.parse(localStorage.getItem('workspaceState')));
    }
  

    updateState(user?: firebase.User, token?: string, loginStatus?: boolean, dbUser?: UserProfile) {
        this.currentFirebaseUser = user;
        this.currentFirebaseToken = token;
        this.LoginStatus = loginStatus;
        this.currentDatabaseUser = dbUser;

        //localStorage
        localStorage.setItem('currentFirebaseToken', this.currentFirebaseToken);
        localStorage.setItem('LoginStatus', `${this.LoginStatus}`);
    }
}
