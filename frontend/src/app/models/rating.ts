import { UserProfile } from "./user-profile";

export interface Rating {
    Id: number;
    Rate: number;
    Comment: string;
    CreatedBy: UserProfile;
    CreatedAt: Date;
}
