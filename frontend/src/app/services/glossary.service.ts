import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Observable } from 'rxjs';
import { Glossary } from '../models';

@Injectable()
export class GlossaryService {
  api: string;
  constructor(private dataService: HttpService) { 
    this.api = "glossaries";
  }

  getAll() : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, undefined, undefined);
  }

  getById(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, id, undefined);
  }

  create(glossary) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api, '', glossary);
  }

  update(glossary: Glossary, id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api, id, glossary);
  }

  delete(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Delete, this.api, id, undefined);
  }
}
