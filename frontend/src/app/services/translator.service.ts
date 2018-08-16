import { Injectable } from '@angular/core';
import { Observable } from '../../../node_modules/rxjs';
import { HttpService, RequestMethod } from './http.service';
import { Translator } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TranslatorService {

  api: string;
  constructor(private dataService: HttpService) { 
    this.api = "translators";
  }

  getAll() : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, undefined, undefined);
  }

  getById(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, id, undefined);
  }

  create(translator: FormData) : Observable<Translator> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api, '', translator);
  }

  update(translator: Translator, id: number) : Observable<Translator> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api, id, translator);
  }

  delete(id: number) : Observable<Translator> {
    return this.dataService.sendRequest(RequestMethod.Delete, this.api, id, undefined);
  }
}
