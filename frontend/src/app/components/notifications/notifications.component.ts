import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.sass']
})
export class NotificationsComponent implements OnInit {

    constructor(private notificationService: NotificationService) { }

    userNotifications: Notification[];
    ngOnInit() {
        this.notificationService.getCurrenUserNotifications().subscribe(notifications => {
            this.userNotifications = notifications;
            console.log(this.userNotifications);
        });
    }

}
