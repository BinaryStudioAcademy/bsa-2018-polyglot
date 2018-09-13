import { Role } from "..";

export interface ChatUser {
    id: number;

    uid: string;

    fullName: string;

    email: string;

    role: Role;

    lastSeen: Date;

    isOnline: boolean;
}