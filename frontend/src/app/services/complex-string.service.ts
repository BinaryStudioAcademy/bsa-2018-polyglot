import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { IString } from '../models/string';
import {Translation} from '../models/translation'
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

  create(data: FormData) : Observable<IString> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api, '', data);
  }

  update(iString: IString, id: number) : Observable<IString> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api, id, iString);
  }

  updateTranslation(translation: Translation, id: number) : Observable<Translation> {
    var str = this.api+"/4/translations";
    return this.dataService.sendRequest(RequestMethod.Put, str, '', translation);
  }

  delete(id: number) : Observable<IString> {
    return this.dataService.sendRequest(RequestMethod.Delete, this.api, id, undefined);
  }
}
