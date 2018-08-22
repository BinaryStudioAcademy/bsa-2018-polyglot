import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Observable, of } from 'rxjs';
import { GoogleTranslation } from '../models/googleTranslation';

@Injectable({
  providedIn: 'root'
})
export class TranslationService {

  api: string;
  constructor(private dataService: HttpService) { 
    this.api = "transation";
  }

  getTransation(item : GoogleTranslation) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api, undefined, item);
  }
}
