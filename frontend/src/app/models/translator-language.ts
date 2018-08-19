import { Language } from "./language";
import { UserProfile } from "./user-profile";

export interface TranslatorLanguage {
    languageId: number;
    language: Language;
    proficiency: number;
}
