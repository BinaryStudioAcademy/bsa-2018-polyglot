import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Observable } from 'rxjs';
import { Tag } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TagService {

  private api: string = "tags";
  constructor(private dataService: HttpService) { }

  getProjectTags(projectId:number): Observable<Tag[]> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/'+ projectId, undefined, undefined);
  }

  addTagsToProject(tags : Tag[],projectId:number): Observable<Tag[]> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api + '/' + projectId, undefined, tags);
  }



}
