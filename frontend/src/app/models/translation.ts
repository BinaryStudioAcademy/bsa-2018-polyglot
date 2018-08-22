import { AdditionalTranslation } from "./additionalTranslation";

export interface Translation {

  id: string;
  userId: number;
  translationValue: string;
  language: string;
  createdOn: Date;

  history: AdditionalTranslation[];
  optionalTranslations: AdditionalTranslation[]

  Type : TranslationType

}

enum TranslationType {
  Human,
  Machine
}