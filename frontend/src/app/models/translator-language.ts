import { Translator } from "./translator";
import { Language } from "./language";

export interface TranslatorLanguage {
    TranslatorId: number;
    Translator: Translator;
    LanguageId: number;
    Language: Language;
    Proficiency: string;
}
