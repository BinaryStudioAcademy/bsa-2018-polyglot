import { Language } from "./language";
import { UserProfile } from "./user-profile";

export interface TranslatorLanguage {
    translatorId: number;
    translator: UserProfile;
    languageId: number;
    language: Language;
    proficiency: string;
}
