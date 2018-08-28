import { ProjectGlossary } from "./project-glossary";
import { GlossaryString } from "./glossary-string";
import { Language } from "./language";

export interface Glossary {
    id: number;
    glossaryStrings : Array<GlossaryString>;
    name: string;
    originLanguage: Language;
    projectGlossaries:  Array<ProjectGlossary>;
}
