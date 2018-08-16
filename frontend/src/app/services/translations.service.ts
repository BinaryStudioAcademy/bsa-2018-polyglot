import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Translation } from '../models/translation';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TranslationsService {

  api: string;
  constructor(private dataService: HttpService) { 
    this.api = "ComplexStrings";
  }

  getStringTranslations(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/translationss', undefined, undefined);
  }

  updateStringTranslations(translations: Translation[], id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api + '/' + id + '/translationss', undefined, translations);
  }
}
