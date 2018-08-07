import { Tag } from "./tag";
import { Project } from "./project";

export interface ProjectTag {
    projectId: number;
    project: Project;
    tagId: number;
    tag: Tag;
}
