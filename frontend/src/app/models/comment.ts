import { UserProfile } from ".";

export interface Comment {
    user: UserProfile;
    text: string;
    createdOn: Date;
    id: string;
}