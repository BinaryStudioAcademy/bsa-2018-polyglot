import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Observable } from 'rxjs';
import { Team } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TeamService {

  private team: Team;

  
  private api: string = "teams";
  constructor(private dataService: HttpService) { }
  
  getAll() : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, undefined, undefined);
  }

  getOne(id: number){
    return this.dataService.sendRequest(RequestMethod.Get, this.api, id);
  }

  create(body){
    return this.dataService.sendRequest(RequestMethod.Post, this.api, undefined, body);
  }

  update(id: number, body){
    return this.dataService.sendRequest(RequestMethod.Put, this.api, id, body);
  }

  delete(id: number){
    return this.dataService.sendRequest(RequestMethod.Delete, this.api, id);
  }
}
