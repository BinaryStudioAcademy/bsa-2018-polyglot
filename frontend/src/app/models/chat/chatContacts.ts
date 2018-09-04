import { ChatUser } from "..";

export interface ChatContacts {
    chatUserId: number;

    contactList: ChatUser[];
}