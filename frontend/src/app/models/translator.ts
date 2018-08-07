import { Rating } from "./rating";
import { UserProfile } from "./user-profile";
import { TeamTranslator } from "./team-translator";

export interface Translator {
    id: number;
    userProfile: UserProfile;
    rating: Rating;
    teamTranslators: Array<TeamTranslator>;
}
