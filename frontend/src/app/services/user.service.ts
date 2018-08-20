import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Observable } from 'rxjs';
import { UserProfile } from '../models';
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

  getCurrrentUser(){
    return this.appState.currentDatabaseUser;
  }

  getAndSave() {
    this.getUser().subscribe(
      (d)=> {
        this.saveUser(d);
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

  saveUser(userProfile: any) {
    if (userProfile.avatarUrl == undefined) {
      userProfile.avatarUrl = '/assets/images/default-avatar.jpg';
    }
    // can add more default values
    
    this.appState.currentDatabaseUser = userProfile;
  }

  getUser() : Observable<UserProfile> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/user');
  }

  getOne(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, id, undefined);
  }

  isUserInDb() : Observable<boolean> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/isInDb', undefined);
  }

  getAll() : Observable<any[]> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, undefined, undefined);
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
