import { Role } from "..";

export interface ChatUser {
    id: number;

    fullName: string;

    email: string;

    role: Role;

    lastSeen: Date;

    isOnline: boolean;

    lastMessageText: string;
}