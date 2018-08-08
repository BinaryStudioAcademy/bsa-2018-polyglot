import { Injectable } from '@angular/core';
import { Translator } from '../models/Translator';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor() { }

  FindTranslatorsByEmail(email: string) : Translator[]{
    let translators = 
[
  {
      id: 1,
      userProfile: {
        id: 1,
        firstName: 'Vasilij',
        lastName: 'Polietilen',
        birthDate: new Date(1992, 1,1),
        registrationDate: new Date(),
        country: 'Ukraine',
        city: 'Kiev',
        region: '',
        postalCode: '',
        address: '',
        phone: '',
        avatarUrl: ''
      },
      rating: null,
      teamTranslators: null
  },
  {
      id: 2,
      userProfile: {
        id: 1,
        firstName: 'Grigorij',
        lastName: 'Butylka',
        birthDate: new Date(1992, 1,1),
        registrationDate: new Date(),
        country: 'Ukraine',
        city: 'Kiev',
        region: '',
        postalCode: '',
        address: '',
        phone: '',
        avatarUrl: ''
      },
      rating: null,
      teamTranslators: null
  },
  {
      id: 3,
      userProfile: {
        id: 1,
        firstName: 'Alexej',
        lastName: 'Chibo',
        birthDate: new Date(1992, 1,1),
        registrationDate: new Date(),
        country: 'Ukraine',
        city: 'Kiev',
        region: '',
        postalCode: '',
        address: '',
        phone: '',
        avatarUrl: ''
      },
      rating: null,
      teamTranslators: null
  },
  {
      id: 4,
      userProfile: {
        id: 1,
        firstName: 'Andrej',
        lastName: 'Mers',
        birthDate: new Date(1992, 1,1),
        registrationDate: new Date(),
        country: 'Ukraine',
        city: 'Kiev',
        region: '',
        postalCode: '',
        address: '',
        phone: '',
        avatarUrl: ''
      },
      rating: null,
      teamTranslators: null
  },
  {
      id: 5,
      userProfile: {
        id: 1,
        firstName: 'Viktor',
        lastName: 'Rozembaum',
        birthDate: new Date(1992, 1,1),
        registrationDate: new Date(),
        country: 'Ukraine',
        city: 'Kiev',
        region: '',
        postalCode: '',
        address: '',
        phone: '',
        avatarUrl: ''
      },
      rating: null,
      teamTranslators: null
  },
  {
      id: 6,
      userProfile: {
        id: 1,
        firstName: 'Alexander',
        lastName: 'Denisov',
        birthDate: new Date(1992, 1,1),
        registrationDate: new Date(),
        country: 'Ukraine',
        city: 'Kiev',
        region: '',
        postalCode: '',
        address: '',
        phone: '',
        avatarUrl: ''
      },
      rating: null,
      teamTranslators: null
  },
  {
      id: 7,
      userProfile: {
        id: 1,
        firstName: 'Viktor',
        lastName: 'Boroda',
        birthDate: new Date(1992, 1,1),
        registrationDate: new Date(),
        country: 'Ukraine',
        city: 'Kiev',
        region: '',
        postalCode: '',
        address: '',
        phone: '',
        avatarUrl: ''
      },
      rating: null,
      teamTranslators: null
  }
];
    
  return translators;
  }

}
