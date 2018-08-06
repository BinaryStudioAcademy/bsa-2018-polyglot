import { UserProfile } from "./user-profile";
import { Project } from "./project";

export interface File {
    Id: number;
    
    Link: string;
    
    CreatedOn: Date;
    
    UploadedBy: UserProfile;
    
    Project: Project;
}
