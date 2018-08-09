import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient} from '@angular/common/http';
import { Teammate } from '../models/teammate';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor() { }

  GetTranslatorsByTeam(teamId: number) : Observable<Teammate[]>{
    // собираем translator + email + rights
    return of([
    {
      id: 1,
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
      teamId: 88
    },
    {
      id: 2,
      fullName: 'Alex Komarov',
      email: 'komarow.alex@gmail.com',
      rights: [
              {
                id: 2,
                definition: "lanGuAge-add  ",
                translatorRights: undefined
              }],
      teamId: 88
    },
    {
      id: 3,
      fullName: 'Vasia Kakojto',
      email: 'koko.vs@gmail.com',
      rights: [{
                id: 1,
                definition: "Language-select",
                translatorRights: undefined
              }],
      teamId: 88
    },
    {
      id: 4,
      fullName: 'Grisha Kolesnik',
      email: 'kes.grisha3@gmail.com',
      rights: undefined,
      teamId: 88
    },
    {
      id: 5,
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
      teamId: 21
    },
    {
      id: 6,
      fullName: 'Unknown Men',
      email: 'unknown.mm231@gmail.com',
      rights: [
              {
                id: 3,
                definition: "key",
                translatorRights: undefined
              }],
      teamId: 88
    },
    ,
    {
      id: 7,
      fullName: 'Alesha Greben',
      email: 'greben.alesha@gmail.com',
      rights: undefined,
      teamId: 88
    },
    {
      id: 8,
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
      teamId: 88
    },
    {
      id: 9,
      fullName: 'Viktor Piwo',
      email: 'piwo.ddd@gmail.com',
      rights: [{
                id: 1,
                definition: "Language-select",
                translatorRights: undefined
              }],
      teamId: 88
    },
    {
      id: 10,
      fullName: 'Vasia Shamsia',
      email: 'vasi1234@gmail.com',
      rights: undefined,
      teamId:88
    },
    {
      id: 11,
      fullName: 'Kateria Tarakanowa',
      email: 'katia.ss3@gmail.com',
      rights: undefined,
      teamId: 88
    }
  ].filter(m => m.teamId == teamId));
  }


  FindTranslatorsByEmail(email: string) : Observable<Teammate[]>{
    
    return of([
      {
        id: 144,
        fullName: 'Petr Brzeczyszsczykiewicz',
        email: 'searchTest@gmail.com',
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
        teamId: 82
      },
      {
        id: 22,
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
        teamId: 12
      },
      {
        id: 42,
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
        teamId: 345
      },
      {
        id: 43,
        fullName: 'Don Jonh',
        email: 'searchTest4@gmail.com',
        rights: [
                {
                  id: 2,
                  definition: "lanGuAge-add  ",
                  translatorRights: undefined
                }],
        teamId: 34
      },
      {
        id: 44,
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
        teamId: 34
      }].filter(m => m.email == email));
  }

}

