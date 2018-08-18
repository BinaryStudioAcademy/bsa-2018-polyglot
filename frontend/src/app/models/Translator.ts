import { Right } from './right';

export interface Translator {
  id: number;
  fullName: string;
  email: string;
  rights: Right[];
  rating: number,
  teamId: number;
}