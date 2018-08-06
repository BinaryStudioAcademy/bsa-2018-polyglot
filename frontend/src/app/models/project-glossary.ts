import { Project } from "./project";
import { Glossary } from "./glossary";

export interface ProjectGlossary {
    GlossaryId: number;
        
    Glossary: Glossary;
        
    ProjectId: number;
        
    Project: Project;        
}
