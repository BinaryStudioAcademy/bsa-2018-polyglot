import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Observable, of } from 'rxjs';
import { UserProfile, Rating, Team } from '../models';
import { AppStateService } from './app-state.service';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class UserService {

    api: string;
    private endpoint: string = "userprofiles";
    constructor(private dataService: HttpService, private appState: AppStateService,
        private router: Router) {
        this.api = "userprofiles";
    }

    isCurrentUserManager(): boolean {
        return this.getCurrentUser() && this.getCurrentUser().userRole == 1;
    }

    getCurrentUser() {
        return this.appState.currentDatabaseUser;
    }

    getAndUpdate() {
        this.getUser().subscribe(
            (user) => {
                this.updateCurrentUser(user);
            },
            err => {
                console.log('err', err);
            }
        );
    }

    // use this when logout
    removeCurrentUser() {
        this.appState.currentDatabaseUser = undefined;
    }

    updateCurrentUser(userProfile: any) {
        this.appState.currentDatabaseUser = userProfile;
    }

    getUser(): Observable<UserProfile> {
        return this.dataService.sendRequest(RequestMethod.Get, this.api + '/user');
    }

    getUserRatings(id: number): Observable<Rating[]> {
        return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/ratings');
    }

    getUserTeams(id): Observable<Team[]> {
        return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/teams');
    }

    getOne(id: number): Observable<any> {
        return this.dataService.sendRequest(RequestMethod.Get, this.api, id);
    }

    isUserInDb(): Observable<boolean> {
        return this.dataService.sendRequest(RequestMethod.Get, this.api + '/isInDb');
    }

    getAll(): Observable<any[]> {
        return this.dataService.sendRequest(RequestMethod.Get, this.api);
    }

    create(body): Observable<UserProfile> {
        return this.dataService.sendRequest(RequestMethod.Post, this.api, undefined, body);
    }

    update(id: number, body): Observable<UserProfile> {
        return this.dataService.sendRequest(RequestMethod.Put, this.api, id, body);
    }

    updatePhoto(photo: FormData): Observable<UserProfile> {
        return this.dataService.sendRequest(RequestMethod.Put, this.api + '/photo', undefined, photo, undefined, 'form-data');
    }

    delete(id: number): Observable<UserProfile> {
        return this.dataService.sendRequest(RequestMethod.Delete, this.api, id);
    }

    redirectById(id: number) {
        if (this.getCurrentUser().id == id) {
            this.router.navigate(['/profile']);
        }
        else {
            this.router.navigate(['/user', id]);
        }
    }


    getUserProfilesByNameStartWith(startsWith: string) {
        return this.dataService.sendRequest(RequestMethod.Get, this.api + '/name', startsWith);
    }
}
