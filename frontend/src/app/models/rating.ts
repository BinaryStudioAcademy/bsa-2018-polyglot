import { UserProfile } from "./user-profile";

export interface Rating {
    id: number;
    rate: number;
    comment: string;
    user: UserProfile;
    createdBy: UserProfile;
    userId: number;
    createdById: number;
    createdAt: Date;
}
