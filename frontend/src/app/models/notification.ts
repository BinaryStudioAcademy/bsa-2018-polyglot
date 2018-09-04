import { UserProfile } from ".";
import { OptionDefinition } from "./optionDefinition";
import { Option } from "./option";

export class Notification{
    receiver?: UserProfile;
    receiverId: number;
    SenderId?: number;
    sender?: UserProfile;
    message: string;
    options?: Option[];
}