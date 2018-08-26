import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Observable, of } from 'rxjs';
import { Language, Project, LanguageStatistic } from  '../models';
import { map, filter } from '../../../node_modules/rxjs/operators';
import { debug } from 'util';

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

  assignTeamsToProject(projectId: number, teamIds: number[]) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api + '/' + projectId + '/teams/', undefined, teamIds);
  }

  dismissProjectTeam(projectId: number, teamId: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Delete, this.api + '/' + projectId + '/teams/' + teamId);
  }

  getProjectLanguages(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/languages');
  }

  getProjectLanguagesStatistic(projectId: number) : Observable<LanguageStatistic[]> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + projectId + '/languages/stat', undefined, undefined)
    .pipe(map<LanguageStatistic[],any>(data => {
      return data.map(function(langStat: any){
        return {
          id: langStat.id, 
          name: langStat.name,
          code: langStat.code,
          translatedStringsCount: langStat.translatedStringsCount,
          complexStringsCount: langStat.complexStringsCount,
          progress: langStat.complexStringsCount < 1 ? 0 : 100 / langStat.complexStringsCount *  langStat.translatedStringsCount
        };
      });
    }));
}

getProjectLanguageStatistic(projectId: number, langId: number) : Observable<LanguageStatistic> {
  return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + projectId + '/languages/' + langId + '/stat', undefined, undefined)
  .pipe(map<LanguageStatistic,any>(function(langStat) {
    return {
        id: langStat.id, 
        name: langStat.name,
        code: langStat.code,
        translatedStringsCount: langStat.translatedStringsCount,
        complexStringsCount: langStat.complexStringsCount,
        progress: langStat.complexStringsCount < 1 ? 0 : 100 / langStat.complexStringsCount *  langStat.translatedStringsCount
    }
  }));
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

  getProjectStringsWithPagination(projectId: number, itemsOnPage: number, page: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + projectId + '/','paginatedStrings?itemsOnPage='+itemsOnPage+'&page='+page);
  }

  getProjectActivitiesById(projectId: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + projectId + '/activities');
  }

  getProjectReports(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/reports', undefined, undefined);
  }
}
