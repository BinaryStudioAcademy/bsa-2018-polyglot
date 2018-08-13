import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { IString } from '../models/string';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ComplexStringService {
  api: string;
  constructor(private dataService: HttpService) { 
    this.api = "complexstrings";
  }

  getAll() : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, undefined, undefined);
  }

  getById(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, id, undefined);
  }

  create(iString: IString) : Observable<IString> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api, '', iString);
  }

  update(iString: IString, id: number) : Observable<IString> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api, id, iString);
  }

  delete(id: number) : Observable<IString> {
    return this.dataService.sendRequest(RequestMethod.Delete, this.api, id, undefined);
  }
}
