import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Observable } from 'rxjs';
import { UserProfile } from '../models';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private user: UserProfile;

  
  private endpoint: string = "userprofiles/user";
  constructor(private dataService: HttpService) { }

  getCurrrentUser(){
    return this.user;
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

  saveUser(userProfile: any) {
    this.user = userProfile;
  }

  getUser(): Observable<UserProfile> {
    return this.dataService.sendRequest(RequestMethod.Get, this.endpoint);
  }

  getOne(id: number){
    return this.dataService.sendRequest(RequestMethod.Get, this.endpoint, id);
  }

  create(body){
    return this.dataService.sendRequest(RequestMethod.Post, this.endpoint, undefined, body);
  }

  update(id: number, body){
    return this.dataService.sendRequest(RequestMethod.Put, this.endpoint, id, body);
  }

  delete(id: number){
    return this.dataService.sendRequest(RequestMethod.Delete, this.endpoint, id);
  }
}
