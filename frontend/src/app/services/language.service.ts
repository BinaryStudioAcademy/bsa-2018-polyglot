import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Observable } from 'rxjs';
import { Language } from '../models/language';
import { TranslatorLanguage } from '../models';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {
  api: string;
  constructor(private dataService: HttpService) { 
    this.api = "languages";
  }

  getAll() : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api);
  }

  getById(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, id);
  }

  create(language: Language) : Observable<Language> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api, '', language);
  }

  update(language: Language, id: number) : Observable<Language> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api, id, language);
  }

  delete(id: number) : Observable<Language> {
    return this.dataService.sendRequest(RequestMethod.Delete, this.api, id);
  }

  getTranslatorsLanguages(userId: number) : Observable<TranslatorLanguage[]> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, "user/" + userId);
  }

  SetCurrentUserLaguage(translaorLanguage: TranslatorLanguage) : Observable<TranslatorLanguage[]> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api, undefined, translaorLanguage);
  }
}
