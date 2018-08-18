import { Team } from "./team";
import { TranslatorRight } from "./translator-right";
import { UserProfile } from ".";

export interface TeamTranslator {
    id: number;
    teamId: number;
    team: Team;
    translatorId: number;
    translator: UserProfile;
    translatorRights: Array<TranslatorRight>;
}
