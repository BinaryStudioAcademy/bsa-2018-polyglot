import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpService, RequestMethod } from './http.service';
import { GroupType, ChatContacts, Project, Team, ChatMessage } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  api: string;
  constructor(private dataService: HttpService) { 
    this.api = "chat";
  }

  getContacts(targetGroup: GroupType, targetGroupId: number) : Observable<ChatContacts> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + `/${GroupType[targetGroup]}/${targetGroupId}/contacts`);
  }

  getProjectsList() : Observable<Project[]> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/projects');
  }

  getTeamsList() : Observable<Team[]> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/teams');
  }

  getMessagesHistory(group: GroupType, targetGroupId: number) : Observable<ChatMessage[]> {
    debugger;
    return this.dataService.sendRequest(RequestMethod.Get, this.api + `/${GroupType[group]}/${targetGroupId}/messages`);
  }

  getMessage(group: GroupType, targetGroupId: number, targetMessageId: number) : Observable<ChatMessage> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + `/${group}/${targetGroupId}/messages`, targetMessageId);
  }

  //createMessage(project: FormData) : Observable<Project> {
  //  return this.dataService.sendRequest(RequestMethod.Post, this.api, '', project, undefined, 'form-data');
  //}
//
  //editMessage(project: FormData) : Observable<Project> {
  //  return this.dataService.sendRequest(RequestMethod.Post, this.api, '', project, undefined, 'form-data');
  //}
//
  //deleteMessage(project: FormData) : Observable<Project> {
  //  return this.dataService.sendRequest(RequestMethod.Post, this.api, '', project, undefined, 'form-data');
  //}
//
  //addContact(project: FormData) : Observable<Project> {
  //  return this.dataService.sendRequest(RequestMethod.Post, this.api, '', project, undefined, 'form-data');
  //}
//
  //deleteContact(project: FormData) : Observable<Project> {
  //  return this.dataService.sendRequest(RequestMethod.Post, this.api, '', project, undefined, 'form-data');
  //}

}
