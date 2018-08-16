import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Translation } from '../models/translation';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TranslationsService {

  constructor() { }
}
