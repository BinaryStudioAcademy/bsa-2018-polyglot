import { Project } from "./project";
import { Glossary } from "./glossary";

export interface ProjectGlossary {
    glossaryId: number;
        
    glossary: Glossary;
        
    projectId: number;
        
    project: Project;        
}
