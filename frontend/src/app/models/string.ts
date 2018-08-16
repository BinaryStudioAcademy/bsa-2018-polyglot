import { Translation } from "./translation";
import { Comment } from "./comment";

export interface IString {
    id: number;
    key: string;
    base: string;
    description: string;
    tags: string[];
    translations: Translation[];
    comments: Comment[];
    projectId: number;
    translations: Translation[];
}
