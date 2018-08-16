import { User } from "./user";

export interface Comment {
    user: User;
    text: string;
    createdOn: Date;
}