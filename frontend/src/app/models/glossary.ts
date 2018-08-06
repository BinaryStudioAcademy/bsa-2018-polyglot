import { ProjectGlossary } from "./project-glossary";

export interface Glossary {
    Id: number;
    TermText: string;
    ExplanationText: string;
    OriginLanguage: string;
    ProjectGlossaries:  Array<ProjectGlossary>;
}
