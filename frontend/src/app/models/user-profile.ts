import { Rating } from ".";
import { Role } from "./role";

export interface UserProfile {
    id?: number;
    firstName?: string;
    lastName?: string;
    birthDate?: Date;
    registrationDate?: Date;
    country?: string;
    city?: string;
    region?: string;
    postalCode?: string;
    address?: string;
    phone?: string;
    avatarUrl?: string;
    fullName?: string;
    ratings?: Array<Rating>;
    userRole? : Role;
    uid?: string;
}
