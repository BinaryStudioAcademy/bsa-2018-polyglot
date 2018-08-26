import { ProjectGlossary } from "./project-glossary";
import { GlossaryString } from "./glossary-string";

export interface Glossary {
    id: number;
    glossaryStrings : Array<GlossaryString>;
    name: string;
    originLanguage: string;
    projectGlossaries:  Array<ProjectGlossary>;
}
