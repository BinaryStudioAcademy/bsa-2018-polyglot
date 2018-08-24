import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Observable } from 'rxjs';
import { UserProfile, Rating } from '../models';
import { AppStateService } from './app-state.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  api: string;
  private endpoint: string = "userprofiles";
  constructor(private dataService: HttpService, private appState: AppStateService) {
    this.api = "userprofiles";
   }

  getCurrentUser(){
    let user = this.appState.currentDatabaseUser;

    if (user) {
      if (!user.avatarUrl || user.avatarUrl == '') {
        user.avatarUrl = '/assets/images/default-avatar.jpg';
      }
    }
    
    return user;
  }

  getAndUpdate() {
    this.getUser().subscribe(
      (user)=> {
        this.updateCurrrentUser(user);
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

  updateCurrrentUser (userProfile: any) {
    if (userProfile.avatarUrl == undefined || userProfile.avatarUrl == null || userProfile.avatarUrl == '') {
      userProfile.avatarUrl = '/assets/images/default-avatar.jpg';
    }
    // can add more default values
    
    this.appState.currentDatabaseUser = userProfile;
  }

  getUser() : Observable<UserProfile> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/user');
  }

  getUserRatings(id: number) : Observable<Rating> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/ratings');
  }

  getOne(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, id);
  }

  isUserInDb() : Observable<boolean> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/isInDb');
  }

  getAll() : Observable<any[]> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api);
  }

  create(body) : Observable<UserProfile>{
    return this.dataService.sendRequest(RequestMethod.Post, this.api, undefined, body);
  }

  update(id: number, body) : Observable<UserProfile>{
    return this.dataService.sendRequest(RequestMethod.Put, this.api, id, body);
  }

  delete(id: number) : Observable<UserProfile>{
    return this.dataService.sendRequest(RequestMethod.Delete, this.api, id);
  }
}
