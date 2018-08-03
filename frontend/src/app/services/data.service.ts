import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService <T> {

  private headers: HttpHeaders;
  protected url: string;
  protected endpoint: string;

  constructor(private httpClient: HttpClient, url: string,  endpoint: string,) { 
      this.headers = new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'});
      this.url = url;
      this.endpoint = endpoint;
    }

    public create(item: T): Observable<T> {
      return this.httpClient
        .post<T>(`${this.url}/${this.endpoint}`, item, {headers: this.headers});
    }
  
    public update(id: number, item: T): Observable<T> {
      return this.httpClient
        .put<T>(`${this.url}/${this.endpoint}/${id}`, item, {headers: this.headers});
    }
  
    public getOne (id: number): Observable<T> {
      return this.httpClient
        .get<T>(`${this.url}/${this.endpoint}/${id}`, {headers: this.headers});
    }
  
    public getList(): Observable<T> {
      return this.httpClient
        .get<T>(`${this.url}`) as Observable<T>
    }
  
    public delete(id: number) : Observable<T> {
      return this.httpClient
        .delete<T>(`${this.url}/${this.endpoint}/${id}`, {headers: this.headers}); 
    }

}
