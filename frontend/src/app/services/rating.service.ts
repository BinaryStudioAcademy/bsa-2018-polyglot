import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Observable } from 'rxjs';
import { Rating } from '../models';

@Injectable({
  providedIn: 'root'
})
export class RatingService { 
  api: string;
  private endpoint: string = "ratings";
  constructor(private dataService: HttpService) {
    this.api = "ratings";
   }

  getRating(id: number) : Observable<Rating> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, id);
  }

  getAll() : Observable<any[]> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, undefined, undefined);
  }

  create(body) : Observable<Rating>{
    return this.dataService.sendRequest(RequestMethod.Post, this.api, undefined, body);
  }

  update(id: number, body) : Observable<Rating>{
    return this.dataService.sendRequest(RequestMethod.Put, this.api, id, body);
  }

  delete(id: number) : Observable<Rating>{
    return this.dataService.sendRequest(RequestMethod.Delete, this.api, id);
  }
}
