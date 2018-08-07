import { Manager } from "./manager";
import { Language } from "./language";
import { ProjectTag } from "./project-tag";
import { ProjectGlossary } from "./project-glossary";
import { ProjectLanguage } from "./project-language";
import { Translation } from "./translation";
import { Team } from "./team";

export interface Project {
    id: number;
    name: string;
    description: string;
    technology: string;
    imageUrl: string;
    createdOn: Date;
    manager: Manager;
    mainLanguage: Language;
    teams: Array<Team>;
    translations: Array<Translation>;
    projectLanguageses: Array<ProjectLanguage>;
    projectGlossaries: Array<ProjectGlossary>;
    projectTags: Array<ProjectTag>;
}
