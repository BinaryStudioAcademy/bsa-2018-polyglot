import { Translator } from "./Translator";

export interface Team {
    id: number;
    name : string;
    teamTranslators: Array<Translator>;
}

