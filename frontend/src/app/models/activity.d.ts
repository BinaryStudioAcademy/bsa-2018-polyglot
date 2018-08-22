import { UserProfile } from ".";

export interface Activity{
    message: string;
    dateTime: Date;
    user: UserProfile
}