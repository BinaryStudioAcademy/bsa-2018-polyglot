import { Right, TranslatorLanguage } from '.';

export interface Translator {
  id: number;
  fullName: string;
  photo: string;
  email: string;
  rights: Right[];
  languages: TranslatorLanguage[];
  rating: number,
  teamId: number;
}