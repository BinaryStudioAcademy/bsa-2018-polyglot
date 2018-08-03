import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { User } from '../models/user';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService extends DataService<String> {

  protected endpoint: string = "users";
  constructor(httpClient: HttpClient) {
    super(httpClient);
   }
}
