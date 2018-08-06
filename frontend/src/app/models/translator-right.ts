import { Right } from "./right";
import { TeamTranslator } from "./team-translator";

export interface TranslatorRight {
    TeamTranslatorId: number;
    TeamTranslator: TeamTranslator;
    RightId: number;
    Right: Right;
}
