import { AdditionalTranslation } from "./additionalTranslation";

export interface Translation {
    userId: number;
    translationValue: string;
    language: string;
    createdOn: Date;

    history: AdditionalTranslation[];
    optionalTranslations: AdditionalTranslation[]
}
