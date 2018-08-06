import { UserProfile } from "./user-profile";
import { Project } from "./project";

export interface ProjectHistory {
    Id: number;
    Project: Project;
    Actor: UserProfile;
    TableName: string;
    ActionType: string;
    OriginValue: string;
    Time: Date;
}
