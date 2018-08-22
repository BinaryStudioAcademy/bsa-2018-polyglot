import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Observable, of } from 'rxjs';
import { Language, Project, LanguageStatistic } from  '../models';
import { map } from '../../../node_modules/rxjs/operators';

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

  getProjectLanguages(id: number) : Observable<Language[]> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/languages', undefined, undefined);
  }

  getProjectLanguagesStatistic(id: number) : Observable<LanguageStatistic[]> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/languages/stat', undefined, undefined)
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
    return this.dataService.sendRequest(RequestMethod.Delete, this.api + '/' + projectId + '/languages/' + languageId, undefined, undefined);
  }


  getProjectFile(projectId: number, languageId: number, extension: string) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + projectId + '/export/',
                  '?langId=' + languageId + '&extension=' + extension, undefined, 'blob');
  }

  getProjectStringsByFilter(projectId: number,options: Array<string>) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api + '/' + projectId + '/filteredstring', undefined, options);

  }

  getProjectActivitiesById(projectId: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + projectId + '/activities', undefined);
  }

  computeProgress(complexStringsCount: number, translatedStringsCount: number) : number
  {
    if(complexStringsCount < 1)
      return 0;
    else
    {
      return 100 / complexStringsCount * translatedStringsCount;
    }
  }
}
