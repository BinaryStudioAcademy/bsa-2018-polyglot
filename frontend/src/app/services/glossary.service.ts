import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Observable } from 'rxjs';
import { Glossary } from '../models';
import { GlossaryString } from '../models/glossary-string';
import { GlossaryTerm } from '../models/glossaryTerm';
import { forEach } from '@angular/router/src/utils/collection';

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

  addString(id : number, string : GlossaryString) : Observable<any>{
    return this.dataService.sendRequest(RequestMethod.Post, this.api + `/${id}/strings`, undefined, string);
  }

  editString(id : number, string : GlossaryString) : Observable<any>{
    return this.dataService.sendRequest(RequestMethod.Put, this.api + `/${id}/strings`, undefined, string);
  }

  deleteString(id : number, stringId : number) : Observable<any>{
    return this.dataService.sendRequest(RequestMethod.Delete, this.api + `/${id}/strings/${stringId}`, undefined, undefined);
  }
  
  fakeGlossaryParse(base: string, translation: string) {
    var glossary: GlossaryTerm[] = [];
    var selected: GlossaryTerm[] = [];
    let term1: GlossaryTerm = {expression: 'DTO', definition: 'Data Transfer Object'};
    let term2: GlossaryTerm = {expression: 'OOP', definition: 'Object-oriented programming'};
    let term3: GlossaryTerm = {expression: 'JSON', definition: 'JavaScript Object Notation'};

    glossary = [term1, term2, term3];

    glossary.forEach(
      function(element){
        if (base.toLowerCase().includes(element.expression.toLowerCase()) ||
            translation.toLowerCase().includes(element.expression.toLowerCase())) {

            selected.push(element);
        }
      }
    );

    return selected;
  }
}
