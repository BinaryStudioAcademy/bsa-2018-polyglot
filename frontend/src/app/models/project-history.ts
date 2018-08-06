import { UserProfile } from "./user-profile";
import { Project } from "./project";

export interface ProjectHistory {
    id: number;
    project: Project;
    actor: UserProfile;
    tableName: string;
    actionType: string;
    originValue: string;
    time: Date;
}
