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

  GetTranslatorsByTeam(teamId: number) : Observable<Translator[]>{
    // собираем translator + email + rights
    return of([
    {
      id: 1,
      avatarUrl: '',
      translatorLanguages: [],
      fullName: 'William Pool',
      email: 'w.pool@gmail.com',
      rights: [{
                id: 1,
                definition: "key",
                translatorRights: undefined
              },
              {
                id: 2,
                definition: "lanGuAge-add  ",
                translatorRights: undefined
              }],
      teamId: 88,
      rating: 1,
    },
    {
      id: 2,
      avatarUrl: '',
      translatorLanguages: [],
      fullName: 'Alex Komarov',
      email: 'komarow.alex@gmail.com',
      rights: [
              {
                id: 2,
                definition: "lanGuAge-add  ",
                translatorRights: undefined
              }],
      teamId: 88,
      rating: 1,
    },
    {
      id: 3,
      avatarUrl: '',
      translatorLanguages: [],
      fullName: 'Vasia Kakojto',
      email: 'koko.vs@gmail.com',
      rights: [{
                id: 1,
                definition: "Language-select",
                translatorRights: undefined
              }],
      teamId: 88,
      rating: 1,
    },
    {
      id: 4,
      avatarUrl: '',
      translatorLanguages: [],
      fullName: 'Grisha Kolesnik',
      email: 'kes.grisha3@gmail.com',
      rights: undefined,
      teamId: 88,
      rating: 1,
    },
    {
      id: 5,
      avatarUrl: '',
      translatorLanguages: [],
      fullName: 'Grigorij Boroda',
      email: 'grg.boroda@gmail.com',
      rights: [{
                id: 1,
                definition: "Language-select",
                translatorRights: undefined
              },
              {
                id: 2,
                definition: "lanGuAge-add  ",
                translatorRights: undefined
              },
              {
                id: 3,
                definition: "key",
                translatorRights: undefined
              }],
      teamId: 21,
      rating: 1,
    },
    {
      id: 6,
      avatarUrl: '',
      translatorLanguages: [],
      fullName: 'Unknown Men',
      email: 'unknown.mm231@gmail.com',
      rights: [
              {
                id: 3,
                definition: "key",
                translatorRights: undefined
              }],
      teamId: 88,
      rating: 1,
    },
    ,
    {
      id: 7,
      avatarUrl: '',
      translatorLanguages: [],
      fullName: 'Alesha Greben',
      email: 'greben.alesha@gmail.com',
      rights: undefined,
      teamId: 88,
      rating: 1,
    },
    {
      id: 8,
      avatarUrl: '',
      translatorLanguages: [],
      fullName: 'Misha Mavashy',
      email: 'mavashy@gmail.com',
      rights: [{
                id: 1,
                definition: "key",
                translatorRights: undefined
              },
              {
                id: 2,
                definition: "lanGuAge-add  ",
                translatorRights: undefined
              }],
      teamId: 88,
      rating: 1,
    },
    {
      id: 9,
      avatarUrl: '',
      translatorLanguages: [],
      fullName: 'Viktor Piwo',
      email: 'piwo.ddd@gmail.com',
      rights: [{
                id: 1,
                definition: "Language-select",
                translatorRights: undefined
              }],
      teamId: 88,
      rating: 1,
    },
    {
      id: 10,
      avatarUrl: '',
      translatorLanguages: [],
      fullName: 'Vasia Shamsia',
      email: 'vasi1234@gmail.com',
      rights: undefined,
      teamId:88,
      rating: 1,
    },
    {
      id: 11,
      avatarUrl: '',
      translatorLanguages: [],
      fullName: 'Kateria Tarakanowa',
      email: 'katia.ss3@gmail.com',
      rights: undefined,
      teamId: 88,
      rating: 1,
    }
  ].filter(m => m.teamId == teamId));
  }


  FindTranslatorsByEmail(email: string) : Observable<Translator[]>{
    
    return of([
      {
        id: 144,
        fullName: 'Petr Brzeczyszsczykiewicz',
        email: 'searchTest@gmail.com',
        avatarUrl: '',
        translatorLanguages: [],
        rights: [{
                  id: 1,
                  definition: "key",
                  translatorRights: undefined
                },
                {
                  id: 2,
                  definition: "lanGuAge-add  ",
                  translatorRights: undefined
                },
                {
                  id: 4,
                  definition: "lanGuAge-select",
                  translatorRights: undefined
                }],
        teamId: 82,
        rating: 1,
      },
      {
        id: 22,
        avatarUrl: '',
        translatorLanguages: [],
        fullName: 'Grigorij Boroda',
        email: 'searchTest2@gmail.com',
        rights: [{
                  id: 1,
                  definition: "Language-select",
                  translatorRights: undefined
                },
                {
                  id: 2,
                  definition: "lanGuAge-add  ",
                  translatorRights: undefined
                }],
        teamId: 12,
        rating: 1,
      },
      {
        id: 42,
        avatarUrl: '',
        translatorLanguages: [],
        fullName: 'Vika Vitulskaja',
        email: 'searchTest3@gmail.com',
        rights: [{
                  id: 1,
                  definition: "key",
                  translatorRights: undefined
                },
                {
                  id: 2,
                  definition: "lanGuAge-select  ",
                  translatorRights: undefined
                }],
        teamId: 345,
        rating: 1,
      },
      {
        id: 43,
        avatarUrl: '',
        translatorLanguages: [],
        fullName: 'Don Jonh',
        email: 'searchTest4@gmail.com',
        rights: [
                {
                  id: 2,
                  definition: "lanGuAge-add  ",
                  translatorRights: undefined
                }],
        teamId: 34,
        rating: 1,
      },
      {
        id: 44,
        avatarUrl: '',
        translatorLanguages: [],
        fullName: 'Jon Don',
        email: 'searchTest4@gmail.com',
        rights: [{
                  id: 1,
                  definition: "key",
                  translatorRights: undefined
                },
                {
                  id: 2,
                  definition: "lanGuAge-add  ",
                  translatorRights: undefined
                }],
        teamId: 34,
        rating: 1,
      }].filter(m => m.email == email));
  }

}

