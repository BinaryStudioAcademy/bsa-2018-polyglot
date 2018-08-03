import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export abstract class DataService <T> {

  private headers: HttpHeaders;
  protected  url: string= "http://localhost:58828/api";
  protected abstract endpoint: string;

  constructor(protected httpClient: HttpClient) { 
      this.headers = new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'});
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
  
    public getList(): Observable<T[]> {
      return this.httpClient
        .get<T[]>(`${this.url}/${this.endpoint}`, {headers: this.headers}) 
    }
  
    public delete(id: number) : Observable<T> {
      return this.httpClient
        .delete<T>(`${this.url}/${this.endpoint}/${id}`, {headers: this.headers}); 
    }

}
