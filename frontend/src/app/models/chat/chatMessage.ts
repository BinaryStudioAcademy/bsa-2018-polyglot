export interface ChatMessage {
    
    id: number;

    clientId: number;
    
    senderId: number;

    body: string;

    receivedDate: Date;

    isRead: boolean;

    isRecieved: boolean;

    isRecieving: boolean;

    dialogId: number;
}