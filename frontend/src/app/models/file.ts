import { UserProfile } from "./user-profile";
import { Project } from "./project";

export interface File {
    id: number;
    
    link: string;
    
    createdOn: Date;
    
    uploadedBy: UserProfile;
    
    project: Project;
}
