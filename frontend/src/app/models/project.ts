import { Manager } from "./manager";
import { Language } from "./language";
import { ProjectTag } from "./project-tag";
import { ProjectGlossary } from "./project-glossary";
import { ProjectLanguage } from "./project-language";
import { Translation } from "./translation";
import { Team } from "./team";

export interface Project {
    Id: number;
    Name: string;
    Description: string;
    Technology: string;
    ImageUrl: string;
    CreatedOn: Date;
    Manager: Manager;
    MainLanguage: Language;
    Teams: Array<Team>;
    Translations: Array<Translation>;
    ProjectLanguageses: Array<ProjectLanguage>;
    ProjectGlossaries: Array<ProjectGlossary>;
    ProjectTags: Array<ProjectTag>;
}
