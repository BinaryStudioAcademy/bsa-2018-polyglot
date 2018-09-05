import { Translation } from "./translation";
import { Comment } from "./comment";
import { Tag } from ".";

export interface IString {
    id: number;
    key: string;
    base: string;
    description: string;
    tags: Tag[];
    comments: Comment[];
    pictureLink: string;
    projectId: number;
    translations: Translation[];
    createdBy: number;
    createdOn: Date;
}

