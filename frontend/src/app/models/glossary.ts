import { ProjectGlossary } from "./project-glossary";
import { GlossaryString } from "./glossary-string";
import { Language } from "./language";
import { UserProfile } from "./user-profile";

export interface Glossary {
    id: number;
    glossaryStrings : Array<GlossaryString>;
    name: string;
    originLanguage: Language;
    projectGlossaries:  Array<ProjectGlossary>;
    userProfile: UserProfile
}
