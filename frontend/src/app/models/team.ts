import { Translator } from "./Translator";
import { TeamProject } from "./team-project";

export interface Team {
    id: number;
    name : string;
    teamTranslators: Array<Translator>;
    teamProjects: Array<TeamProject>;
}

