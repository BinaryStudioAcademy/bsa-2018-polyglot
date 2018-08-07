import { ProjectGlossary } from "./project-glossary";

export interface Glossary {
    id: number;
    termText: string;
    explanationText: string;
    originLanguage: string;
    projectGlossaries:  Array<ProjectGlossary>;
}
