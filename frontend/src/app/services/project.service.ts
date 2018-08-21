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

  getProjectTeams(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/teams', undefined, undefined);
  }

  assignTeamsToProject(projectId: number, teamIds: Array<number>) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api + '/' + projectId + '/teams/', undefined, teamIds);
  }

  dismissProjectTeam(projectId: number, teamId: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Delete, this.api + '/' + projectId + '/teams/' + teamId, undefined, undefined);
  }

  getProjectLanguages(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/languages', undefined, undefined);
  }

  addLanguagesToProject(projectId: number, languageIds: Array<number>) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api + '/' + projectId + '/languages', undefined, languageIds);
  }

  deleteProjectLanguage(projectId: number, languageId: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Delete, this.api + '/' + projectId + '/languages/' + languageId, undefined, undefined);
  }

  getProjectFile(projectId: number, languageId: number, extension: string) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + projectId + '/export/',
                  '?langId=' + languageId + '&extension=' + extension, undefined);
  }
}
