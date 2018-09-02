export interface ChatMessage {
    
    id: number;

    senderId: number;

    recipientId: number;

    body: string;

    receivedDate: Date;

    IsRead: boolean;
}