import { Right } from "./right";
import { TeamTranslator } from "./team-translator";

export interface TranslatorRight {
    teamTranslatorId: number;
    teamTranslator: TeamTranslator;
    rightId: number;
    right: Right;
}
