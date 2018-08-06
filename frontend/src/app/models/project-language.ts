import { Language } from "./language";
import { Project } from "./project";

export interface ProjectLanguage {
    ProjectId: number;
    Project: Project;
    LanguageId: number;
    Language: Language;
}
