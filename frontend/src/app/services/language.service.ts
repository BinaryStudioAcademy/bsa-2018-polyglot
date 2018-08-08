import { Injectable } from '@angular/core';
import { DataService, RequestMethod } from './data.service';
import { Observable } from 'rxjs';
import { Language } from '../models/language';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {
  api: string;
  constructor(private dataService: DataService) { 
    this.api = "languages";
  }

  getAll() : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, undefined, undefined);
  }

  getById(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, id, undefined);
  }

  create(language: Language) : Observable<Language> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api, '', language);
  }

  update(language: Language, id: number) : Observable<Language> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api, id, language);
  }

  delete(id: number) : Observable<Language> {
    return this.dataService.sendRequest(RequestMethod.Delete, this.api, id, undefined);
  }
}
