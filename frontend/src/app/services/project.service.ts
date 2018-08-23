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
    return this.dataService.sendRequest(RequestMethod.Get, this.api);
  }

  getById(id: number) : Observable<Project> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, id);
  }

  create(project: FormData) : Observable<Project> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api, '', project, undefined, 'form-data');
  }

  update(project: FormData, id: number) : Observable<Project> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api, id, project,  undefined, 'form-data');
  }

  delete(id: number) : Observable<Project> {
    return this.dataService.sendRequest(RequestMethod.Delete, this.api, id);
  }

  getProjectStrings(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/complexStrings');
  }

  postFile(id: number, file: FormData) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api + '/' + id +  '/dictionary' , '' , file, 'blob', 'form-data');
  }

  getProjectTeams(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/teams');
  }

  assignTeamsToProject(projectId: number, teamIds: Array<number>) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api + '/' + projectId + '/teams/', undefined, teamIds);
  }

  dismissProjectTeam(projectId: number, teamId: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Delete, this.api + '/' + projectId + '/teams/' + teamId);
  }

  getProjectLanguages(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/languages');
  }

  addLanguagesToProject(projectId: number, languageIds: Array<number>) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api + '/' + projectId + '/languages', undefined, languageIds);
  }

  deleteProjectLanguage(projectId: number, languageId: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Delete, this.api + '/' + projectId + '/languages/' + languageId);
  }


  getProjectFile(projectId: number, languageId: number, extension: string) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + projectId + '/export/',
                  '?langId=' + languageId + '&extension=' + extension, undefined, 'blob', 'form-data');
  }

  getProjectStringsByFilter(projectId: number,options: Array<string>) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api + '/' + projectId + '/filteredstring', undefined, options);

  }

  getProjectActivitiesById(projectId: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + projectId + '/activities');
  }
}
