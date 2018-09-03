import { Project } from ".";

export interface TeamProject {
    id?: number;
    project: Project;
    projectId?: number;
    teamId: number;
}