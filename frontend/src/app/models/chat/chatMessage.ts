export interface ChatMessage {
    
    id: number;

    senderId: number;

    body: string;

    receivedDate: Date;

    isRead: boolean;

    dialogId: number;
}