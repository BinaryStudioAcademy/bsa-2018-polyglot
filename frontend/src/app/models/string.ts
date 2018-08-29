import { Translation } from "./translation";
import { Comment } from "./comment";

export interface IString {
    id: number;
    key: string;
    base: string;
    description: string;
    tags: string[];
    comments: Comment[];
    projectId: number;
    translations: Translation[];
    createdBy: number;
    createdOn: Date;
}

