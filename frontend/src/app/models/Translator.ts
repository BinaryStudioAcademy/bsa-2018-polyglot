import { Right, TranslatorLanguage } from '.';

export interface Translator {
  id: number;
  fullName: string;
  avatarUrl: string;
  email: string;
  rights: Right[];
  translatorLanguages: TranslatorLanguage[];
  rating: number,
  teamId: number;
  userId: number;
}