import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Observable, pipe } from 'rxjs';
import { map } from 'rxjs/operators';
import { Team, Right } from '../models';
import { Translator } from '../models/Translator';

@Injectable({
  providedIn: 'root'
})
export class TeamService {

  private api: string = "teams";
  constructor(private dataService: HttpService) { }

  getAllTeams(): Observable<Team[]> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, undefined, undefined);
  }

  getTeam(id: number): Observable<Team> {
    
    return this.dataService.sendRequest(RequestMethod.Get, this.api, id, undefined);
  }

  getAllTranslators(): Observable<Translator[]> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/translators', undefined, undefined);
  }

  GetTranslator(id: number): Observable<Translator> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/translators/', id, undefined);
  }

  GetTranslatorRating(translatorId: number): Observable<Translator> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/translators/' + translatorId + '/rating', undefined, undefined);
  }

  formTeam(translatorsIds: Array<number>): Observable<Team> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api, undefined, translatorsIds);
  }
 // create(body){
 //   return this.dataService.sendRequest(RequestMethod.Post, this.api, undefined, body);
 // }

  update(id: number, body){
    return this.dataService.sendRequest(RequestMethod.Put, this.api, id, body);
  }

  delete(id: number): Observable<any>{
    return this.dataService.sendRequest(RequestMethod.Delete, this.api, id);
  }
}
