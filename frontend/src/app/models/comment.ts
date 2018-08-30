import { UserProfile } from ".";

export interface Comment {
    id: string
    user: UserProfile;
    text: string;
    createdOn: Date;
    isEditting?: boolean;
}