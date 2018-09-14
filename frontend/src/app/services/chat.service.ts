import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpService, RequestMethod } from './http.service';
import { GroupType, Project, Team, ChatMessage, UserProfile } from '../models';
import { ChatDialog } from '../models/chat/chatDialog';
import { UserState } from '../models/chat/userState';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  api: string;
  constructor(private dataService: HttpService) { 
    this.api = "chat";
  }

  getDialogs() : Observable<ChatDialog[]> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + `/dialogs`);
  }
  
  addDialog(dialog: ChatDialog) : Observable<ChatDialog> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api + '/dialogs', undefined, dialog);
  }

  deleteDialog(dialogId: number) : Observable<boolean> {
    return this.dataService.sendRequest(RequestMethod.Delete, this.api + '/dialogs', dialogId);
  }

  getProjectsList() : Observable<ChatDialog[]> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/projects');
  }

  getTeamsList() : Observable<ChatDialog[]> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/teams');
  }

  getDialogMessages(group: GroupType, targetGroupDialogId: number) : Observable<ChatMessage[]> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + `/dialogs/${GroupType[group]}/${targetGroupDialogId}/messages`);
  }

  getMessage(targetMessageId: number) : Observable<ChatMessage> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + `/messages`, targetMessageId);
  }

  sendMessage(group: GroupType, message) : Observable<ChatMessage> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api + `/${GroupType[group]}/dialogs/messages`, undefined, message);
  }

  startChatWithUser(user: UserProfile) : Observable<ChatDialog> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api + '/startDialog', undefined, user);
  }

  getUserState(id: number) : Observable<UserState> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api + '/users/' + id + '/state');
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
//
  //deleteContact(project: FormData) : Observable<Project> {
  //  return this.dataService.sendRequest(RequestMethod.Post, this.api, '', project, undefined, 'form-data');
  //}

}
