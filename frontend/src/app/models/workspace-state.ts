import { Language } from "./language";
import { Comment } from "./comment";

export interface WorkspaceState {
    projectId: number;
    languages?: Language[];
    comments?: Comment[];
}
