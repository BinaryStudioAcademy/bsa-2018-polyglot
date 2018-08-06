import { Tag } from "./tag";
import { Project } from "./project";

export interface ProjectTag {
    ProjectId: number;
    Project: Project;
    TagId: number;
    Tag: Tag;
}
