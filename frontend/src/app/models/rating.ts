import { UserProfile } from "./user-profile";

export interface Rating {
    id: number;
    rate: number;
    comment: string;
    createdBy: UserProfile;
    createdAt: Date;
}
