import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { Comment } from '../models/comment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {

  api: string;
  constructor(private dataService: HttpService) { 
    this.api = "ComplexStrings";
  }

  getStringComments(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/comments', undefined, undefined);
  }

  updateStringComments(comments: Comment[], id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api + '/' + id + '/comments', undefined, comments);
  }

  getCommentsByStringId(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/comments', undefined);
  }
}
