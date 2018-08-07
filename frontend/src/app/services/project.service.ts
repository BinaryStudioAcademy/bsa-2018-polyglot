import { Injectable } from '@angular/core';
import { DataService, RequestMethod } from './data.service';
import { Project } from '../models/project';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  api: string;
  constructor(private dataService: DataService) { 
    this.api = "projects";
  }

  createEntity(project: Project) {
    return this.dataService.sendRequest(RequestMethod.Post, this.api, '', project);
  }
}
