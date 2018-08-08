import { ProjectTag } from "./project-tag";

export interface Tag {
    id: number;
    color: string;
    name: string;
    projectTags: Array<ProjectTag>;
}
