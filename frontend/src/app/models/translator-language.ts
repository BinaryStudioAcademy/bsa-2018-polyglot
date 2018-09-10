import { Language } from "./language";
import { UserProfile } from "./user-profile";
import { Proficiency } from "./proficiency";

export interface TranslatorLanguage {
    language: Language;
    proficiency: Proficiency;
}
