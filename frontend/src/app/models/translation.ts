import { AdditionalTranslation } from "./additionalTranslation";

export interface Translation {

    id: string;
    userId: number;
    translationValue: string;
    languageId: number;
    createdOn: Date;
    isConfirmed: boolean;

    history: AdditionalTranslation[];
    optionalTranslations: AdditionalTranslation[]


    assignedTranslatorId: number;
    assignedTranslatorName: string;
    assignedTranslatorAvatarUrl: string;
}

