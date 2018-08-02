import { Injectable } from '@angular/core';
import { WithId } from '../models/interfaces/WithId';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService <T> {

  constructor(
    private httpClient: HttpClient,
    private url: string,
    private endpoint: string,) { }

    public create(item: T): Observable<T> {
      return this.httpClient
        .post<T>(`${this.url}/${this.endpoint}`, item);
    }
  
    public update(id: number, item: T): Observable<T> {
      return this.httpClient
        .put<T>(`${this.url}/${this.endpoint}/${id}`, item);
    }
  
    public getOne (id: number): Observable<T> {
      return this.httpClient
        .get<T>(`${this.url}/${this.endpoint}/${id}`);
    }
  
    public getList(): Observable<T> {
      return this.httpClient
        .get<T>(`${this.url}`) as Observable<T>
    }
  
    public delete(id: number) : Observable<T> {
      return this.httpClient
        .delete<T>(`${this.url}/${this.endpoint}/${id}`); 
    }

  // TODO: GET RIG OF IT!!!
  getAppTitle() {
    return 'Polyglot';
  }
}
