import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService, RequestMethod } from './http.service';
import { Translator } from '../models/Translator';
import { RightDefinition } from '../models/rightDefinition';

@Injectable({
    providedIn: 'root'
})
export class RightService {
    api: string;
    constructor(private dataService: HttpService) {
        this.api = 'teams';
    }

    setTranslatorRight(teamId: number, translatorId: number, rightDefinition: RightDefinition): Observable<Translator>{
        return this.dataService.sendRequest(RequestMethod.Put, this.api + '/' + teamId + "/addTranslatorRight", translatorId, rightDefinition);
    }

    removeTranslatorRight(teamId: number, translatorId: number, rightDefinition: RightDefinition): Observable<Translator>{
        return this.dataService.sendRequest(RequestMethod.Put, this.api + '/' + teamId + "/removeTranslatorRight", translatorId, rightDefinition);
    }

    checkIfTranslatorCan(teamId: number, translatorId: number, rightDefinition: RightDefinition): Observable<boolean>{
        return this.dataService.sendRequest(RequestMethod.Get, this.api + '/' + teamId + "/user/", translatorId + '/right/' + rightDefinition);
    }
}
