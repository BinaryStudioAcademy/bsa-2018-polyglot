import { ProjectTag } from "./project-tag";

export interface Tag {
    Id: number;
    Color: string;
    Name: string;
    ProjectTags: Array<ProjectTag>;
}
