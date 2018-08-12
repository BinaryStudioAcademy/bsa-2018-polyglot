import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpService, RequestMethod } from './http.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FileStorageService {

  constructor(private httpService: HttpService,private httpClient: HttpClient) { }

  uploadFile(file: FormData) : Observable<string>{
    return this.httpService.sendRequest(RequestMethod.Post, "FilesStorage", undefined, file);
  }
}
