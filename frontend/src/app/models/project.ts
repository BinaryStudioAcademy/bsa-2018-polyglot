import { Language } from "./language";
import { ProjectGlossary } from "./project-glossary";
import { ProjectLanguage } from "./project-language";
import { Translation } from "./translation";
import { Team } from "./team";
import { UserProfile } from "./user-profile";
import { Tag } from ".";
import { ProjectTranslationStatistics } from "./projectTranslationStatistics";

export interface Project {
    id?: number;
    name: string;
    description?: string;
    technology: string;
    imageUrl?: string;
    createdOn?: Date;
    userProfile?: UserProfile;
    mainLanguage?: Language;
    teams?: Array<Team>;
    translations?: Array<Translation>;
    projectLanguageses?: Array<ProjectLanguage>;
    projectGlossaries?: Array<ProjectGlossary>;
    tags?: Array<Tag>;
    progress: number;
}
