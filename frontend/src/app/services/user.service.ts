import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private endpoint: string = "users";
  constructor(private dataService: HttpService) { }

   getAll(){
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
