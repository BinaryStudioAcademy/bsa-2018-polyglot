import { Rating } from "./rating";
import { UserProfile } from "./user-profile";
import { TeamTranslator } from "./team-translator";

export interface Translator {
    Id: number;
    UserProfile: UserProfile;
    Rating: Rating;
    TeamTranslators: Array<TeamTranslator>;
}
