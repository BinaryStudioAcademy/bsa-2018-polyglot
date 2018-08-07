import { Translator } from "./translator";
import { Language } from "./language";

export interface TranslatorLanguage {
    translatorId: number;
    translator: Translator;
    languageId: number;
    language: Language;
    proficiency: string;
}
