import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Observable, pipe } from 'rxjs';
import { map } from 'rxjs/operators';
import { Team, Right } from '../models';
import { Teammate } from '../models/teammate';

@Injectable({
  providedIn: 'root'
})
export class TeamService {

  private team: Team;

  
  private api: string = "teams";
  constructor(private dataService: HttpService) { }
  
  getAll() : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, undefined, undefined);
  }

  getAllTeammates(teamId: number) : Observable<Teammate[]> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, teamId)
                .pipe(map<Teammate[], any>(data => {
                  return data.map(function(teammate: any) {
                    debugger;
                    return {
                      id: teammate.id,
                      fullName: teammate.fullName,
                      email: teammate.email,

                      rights: teammate.rights.map(right => {
                            return {
                              id: right.id,
                              definition: right.definition
                            }
                      } ),

                      teamId: teammate.teamId
                    }
                  })
                }))
  }

  create(body){
    return this.dataService.sendRequest(RequestMethod.Post, this.api, undefined, body);
  }

  update(id: number, body){
    return this.dataService.sendRequest(RequestMethod.Put, this.api, id, body);
  }

  delete(id: number){
    return this.dataService.sendRequest(RequestMethod.Delete, this.api, id);
  }
}
