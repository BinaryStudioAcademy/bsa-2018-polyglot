import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Observable } from 'rxjs';
import { GlossaryTerm } from '../models/glossaryTerm';
import { forEach } from '@angular/router/src/utils/collection';

@Injectable({
  providedIn: 'root'
})
export class GlossaryService {

    api: string;
    constructor(private dataService: HttpService) {
        this.api = 'glossary';
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