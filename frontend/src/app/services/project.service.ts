import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Project } from '../models/project';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  api: string;
  constructor(private dataService: HttpService) { 
    this.api = "projects";
  }

  getAll() : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, undefined, undefined);
  }

  getById(id: number) : Observable<Project> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, id, undefined);
  }

  create(project: FormData) : Observable<Project> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api, '', project);
  }

  update(project: FormData, id: number) : Observable<Project> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api, id, project);
  }

  delete(id: number) : Observable<Project> {
    return this.dataService.sendRequest(RequestMethod.Delete, this.api, id, undefined);
  }

  getProjectStrings(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/complexStrings', undefined, undefined);
  }

  postFile(id: number, file: FormData) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api + '/' + id +  '/dictionary' , '' , file);
  }

  getProjectTeam(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/teams', undefined, undefined);
  }

  assignTeamToProject(projectId: number, teamId: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api + '/' + projectId + '/teams/' + teamId, undefined, undefined);
  }

  getProjectLanguages(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/languages', undefined, undefined);
  }

  addLanguageToProject(projectId: number, languageId: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api + '/' + projectId + '/languages/' + languageId, undefined, undefined);
  }

  deleteProjectLanguage(projectId: number, languageId: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Delete, this.api + '/' + projectId + '/languages/' + languageId, undefined, undefined);
  }
}
