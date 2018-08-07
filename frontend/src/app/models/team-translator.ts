import { Translator } from "./translator";
import { Team } from "./team";
import { TranslatorRight } from "./translator-right";

export interface TeamTranslator {
    id: number;
    teamId: number;
    team: Team;
    translatorId: number;
    translator: Translator;
    translatorRights: Array<TranslatorRight>;
}
