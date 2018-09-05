import { UserProfile } from ".";
import { Option } from "./option";
import { NotificationAction } from "./NotificationAction";


export class Notification{
    id?: number;
    receiver?: UserProfile;
    receiverId: number;
    SenderId?: number;
    sender?: UserProfile;
    message: string;
    options?: Option[];
    payload: number;
    notificationAction: NotificationAction;
}