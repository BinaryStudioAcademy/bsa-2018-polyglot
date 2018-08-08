import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient} from '@angular/common/http';
import { Translator } from '../models/Translator';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor() { }

  FindTranslatorsByEmail(email: string) : Observable<Translator[]>{
    
    return of([{
      id: 1,
      userProfile: {
        id: 1,
        firstName: 's_Vasilij',
        lastName: 's_Polietilen',
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
        firstName: 's_Grigorij',
        lastName: 's_Butylka',
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
        firstName: 's_Alexej',
        lastName: 's_Chibo',
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
        firstName: 's_Andrej',
        lastName: 's_Mers',
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
        firstName: 's_Viktor',
        lastName: 's_Rozembaum',
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
        firstName: 's_Alexander',
        lastName: 's_Denisov',
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
        firstName: 's_Viktor',
        lastName: 's_Boroda',
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
  }]);
  }

}
