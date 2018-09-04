import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RequestMethod, HttpService } from './http.service';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  api: string;
  private endpoint: string = "notifications";
  constructor(private dataService: HttpService) {
    this.api = "notifications";
   }

  getCurrenUserNotifications() : Observable<Notification[]> {
    return this.dataService.sendRequest(RequestMethod.Get, this.api);
  }

  sendNotification(notification: any) : Observable<any> {
    return this.dataService.sendRequest(RequestMethod.Post, this.api, undefined, notification);
  }

}
