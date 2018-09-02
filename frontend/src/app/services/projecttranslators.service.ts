import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Observable } from 'rxjs';
import { UserProfilePrev } from '../models/user/user-profile-prev';

@Injectable({
  providedIn: 'root'
})
export class ProjecttranslatorsService {

  api: string;
  constructor(private dataService: HttpService) {
    this.api = "projecttranslators";
  }

  getById(id: number) : Observable<UserProfilePrev[]> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, id);
  }
}
