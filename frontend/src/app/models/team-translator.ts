import { Translator } from "./translator";
import { Team } from "./team";
import { TranslatorRight } from "./translator-right";

export interface TeamTranslator {
    Id: number;
    TeamId: number;
    Team: Team;
    TranslatorId: number;
    Translator: Translator;
    TranslatorRights: Array<TranslatorRight>;
}
