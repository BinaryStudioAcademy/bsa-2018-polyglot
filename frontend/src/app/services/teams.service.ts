import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Observable, pipe } from 'rxjs';
import { map } from 'rxjs/operators';
import { Team, Right, Language } from '../models';
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

  getFilteredTranslators(prof: number, languages: Language[]): Observable<Translator[]> {
    var searchQuery = `?prof=${prof}`;
    if(languages) {
        languages.forEach(function(item, i, languages) {
            searchQuery += `&languages=${item.id}`;
       });
    }
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/filteredtranslators', searchQuery, undefined);
  }

  GetTranslator(id: number): Observable<Translator> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/translators/', id, undefined);
  }

  GetTranslatorRating(translatorId: number): Observable<Translator> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/translators/' + translatorId + '/rating', undefined, undefined);
  }

  formTeam(translatorIds: Array<number>, name : string): Observable<Team> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api, undefined, {translatorIds:translatorIds,name:name});
  }

  deletedTeamTranslators(teamTranslatorIds: Array<number>): Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Delete, this.api + '/translators', undefined, teamTranslatorIds);
  }

  addTeamTranslators(teamTranslatorIds: Array<number>, teamId: number, teamName: string): Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api + '/translators', undefined, {translatorIds: teamTranslatorIds, teamId : teamId, teamName: teamName});
  }

 // create(body){
 //   return this.dataService.sendRequest(RequestMethod.Post, this.api, undefined, body);
 // }

  activateCurrentUserInTeam(teamId: number): Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api, teamId + "/activate");
  }

  update(id: number, body){
    return this.dataService.sendRequest(RequestMethod.Put, this.api, id, body);
  }

  delete(id: number): Observable<any>{
    return this.dataService.sendRequest(RequestMethod.Delete, this.api, id);
  }

  removeUserFromTeam(userId: number, teamId: number): Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Delete, this.api, teamId + "/removeUser/" + userId);
  }
}
