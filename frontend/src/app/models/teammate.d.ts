import { Right } from './right';

export interface Teammate {
  id: number;
  fullName: string;
  email: string;
  rights: Right[];
  teamId: number;
}