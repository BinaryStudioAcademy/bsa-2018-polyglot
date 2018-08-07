import { Language } from "./language";
import { Project } from "./project";

export interface ProjectLanguage {
    projectId: number;
    project: Project;
    languageId: number;
    language: Language;
}
