import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Team, Project } from '../models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ManagerService {

  api: string;
  constructor(private dataService: HttpService) { 
    this.api = "managers";
  }

  getAll() : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, undefined, undefined);
  }

  getById(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, id, undefined);
  }

  create(project: Team) : Observable<Team> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api, '', project);
  }

  update(project: Team, id: number) : Observable<Team> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api, id, project);
  }

  delete(id: number) : Observable<Team> {
    return this.dataService.sendRequest(RequestMethod.Delete, this.api, id, undefined);
  }

  getManagerTeams(id: number): Observable<Team[]> {
      return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/teams', undefined, undefined);
  }

  getManagerProjects(id: number): Observable<Project[]> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/projects', undefined, undefined);
}
}
