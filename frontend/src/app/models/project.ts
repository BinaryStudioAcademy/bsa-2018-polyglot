import { Manager } from "./manager";
import { Language } from "./language";
import { ProjectTag } from "./project-tag";
import { ProjectGlossary } from "./project-glossary";
import { ProjectLanguage } from "./project-language";
import { Translation } from "./translation";
import { Team } from "./team";

export interface Project {
<<<<<<< HEAD
    Id: number;
    Name: string;
    Description: string;
    Technology: string;
    ImageUrl: string;
    CreatedOn: Date;
    Manager: Manager;
    MainLanguage: Language;
    Teams: Team[];
    Translations: Translation[];
    ProjectLanguageses: ProjectLanguage[];
    ProjectGlossaries: ProjectGlossary[];
    ProjectTags: ProjectTag[];
=======
    id?: number;
    name: string;
    description?: string;
    technology: string;
    imageUrl?: string;
    createdOn?: Date;
    manager?: Manager;
    mainLanguage?: Language;
    teams?: Array<Team>;
    translations?: Array<Translation>;
    projectLanguageses?: Array<ProjectLanguage>;
    projectGlossaries?: Array<ProjectGlossary>;
    projectTags?: Array<ProjectTag>;
>>>>>>> 8251e889947afb0da4b5330e7ee01f0f076f0b59
}
