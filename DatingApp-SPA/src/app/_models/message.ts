export interface Message {
    id: number;
    senderId: number;
    senderConhecidoComo: string;
    senderPhotoUrl: string;
    recipientId: number;
    recipientConhecidoComo: string;
    recipientPhotoUrl: string;
    content: string;
    isRead: boolean;
    dateRead: Date;
    messageSent: Date;
}

