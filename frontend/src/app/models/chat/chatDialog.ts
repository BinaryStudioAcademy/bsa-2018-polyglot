import { ChatUser } from "..";

export interface ChatDialog {
    id: number;

    lastMessageText: string;

    unreadMessagesCount: number;

    participants: ChatUser[];

    identifier: number; 

    
}