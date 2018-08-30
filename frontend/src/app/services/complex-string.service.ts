import { Injectable } from '@angular/core';
import { HttpService, RequestMethod } from './http.service';
import { IString } from '../models/string';
import { Translation } from '../models/translation'
import { Observable } from 'rxjs';
import { Comment } from '../models/comment'

@Injectable({
  providedIn: 'root'
})

export class ComplexStringService {
  api: string;
  constructor(private dataService: HttpService) {
    this.api = "complexstrings";
  }

  getAll(): Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api);
  }

  getById(id: number): Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api, id);
  }

  create(data: FormData): Observable<IString> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api, '', data, undefined, 'form-data');
  }

  update(iString: IString, id: number): Observable<IString> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api, id, iString);
  }

  getStringTranslations(id: number): Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/translations');
  }

  createStringTranslation(translation: Translation, id: number): Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api + '/' + id + '/translations', undefined, translation);
  }

  editStringTranslation(translation: Translation, id: number): Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api + '/' + id + '/translations', undefined, translation);
  }

  delete(id: number): Observable<IString> {
    return this.dataService.sendRequest(RequestMethod.Delete, this.api, id, undefined);
  }

  updateStringComments(comments: Comment[], id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api + '/' + id + '/comments', undefined, comments);
  }

  createStringComment(comment: Comment, id: number): Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api + '/' + id + '/comments', undefined, comment)
  }

  editStringComment(comment: Comment, id: number): Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Put, this.api + '/' + id + '/comments', undefined, comment)
  }

  getCommentsByStringId(id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/comments', undefined);
  }

  deleteStringComment(commentId: string, id: number) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Delete, this.api + '/' + id + '/comments/' + commentId, undefined);
  }

  getTranslationHistory(id: number, translationId: string) {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + id + '/history/' + translationId, undefined);
  };
}
