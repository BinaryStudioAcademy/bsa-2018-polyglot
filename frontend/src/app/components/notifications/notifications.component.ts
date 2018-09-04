import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../../services/notification.service';
import { OptionDefinition } from '../../models/optionDefinition';
import { Notification } from '../../models/notification';

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

    getStringEnum(enumNumber: number){
        return OptionDefinition[enumNumber];
    }

    deleteNotification(notificationId: number){
        this.notificationService.removeNotification(notificationId).subscribe(()=>{
            let index = this.userNotifications.findIndex(n => n.id == notificationId);
            this.userNotifications.splice(index, 1);
        });
    }

}
