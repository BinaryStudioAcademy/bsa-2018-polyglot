import { Component, OnInit, Input } from '@angular/core';
import { NotificationService } from '../../services/notification.service';
import { OptionDefinition } from '../../models/optionDefinition';
import { Notification } from '../../models/notification';
import { TeamService } from '../../services/teams.service';
import { SnotifyService } from 'ng-snotify';
import { NotificationAction } from '../../models/NotificationAction';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.sass']
})
export class NotificationsComponent implements OnInit {

    constructor(private notificationService: NotificationService,
                private teamService: TeamService,
                private snotifyService: SnotifyService) { }

    @Input() userNotifications: Notification[];
    ngOnInit() {    }

    getStringEnum(enumNumber: number){
        return OptionDefinition[enumNumber];
    }

    deleteNotification(notificationId: number){
        this.notificationService.removeNotification(notificationId).subscribe(()=>{
            let index = this.userNotifications.findIndex(n => n.id == notificationId);
            this.userNotifications.splice(index, 1);
        });
    }

    buttonClick(notification: Notification, notificationOption: number){
        switch(notification.notificationAction)
        {
            case NotificationAction.None:
                break; //Do some stuff here if notification havent actions

            case NotificationAction.JoinTeam:
            switch(notificationOption){
                case OptionDefinition.Accept:
                    this.teamService.activateCurrentUserInTeam(notification.payload).subscribe((trans)=>{
                        this.notificationService.removeNotification(notification.id).subscribe((notifications)=>{
                            this.userNotifications = notifications;
                            this.snotifyService.success(`You accepted an invitation`, "Success!");
                        });        
                    });
                    break;
                    
                case OptionDefinition.Decline:
                    this.notificationService.removeNotification(notification.id).subscribe((notifications)=>{
                        this.userNotifications = notifications;
                        this.snotifyService.warning(`You declined an invitation`, "Warning!");
                    }); 
                    break;  
                    
            }
            break;
            
        }
    }

}
