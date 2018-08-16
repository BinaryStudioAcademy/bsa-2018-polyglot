import { Translation } from "./translation";

export interface IString {
    id: number;
    key: string;
    base: string;
    description: string;
    tags: string[];
    projectId: number;
    translations: Translation[];
}
