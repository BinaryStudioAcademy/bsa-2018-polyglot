import { TranslatorRight } from "./translator-right";

export interface Right {
    id: number;
    definition: string;
    translatorRights: Array<TranslatorRight>;
}
