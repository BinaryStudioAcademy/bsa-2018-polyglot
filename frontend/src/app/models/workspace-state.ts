import { Language } from "./language";

export interface WorkspaceState {
    projectId: number;
    languages: Language[];
}
