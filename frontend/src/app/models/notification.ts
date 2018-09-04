import { UserProfile } from ".";
import { Option } from "./option";

export class Notification{
    id?: number;
    receiver?: UserProfile;
    receiverId: number;
    SenderId?: number;
    sender?: UserProfile;
    message: string;
    options?: Option[];
}