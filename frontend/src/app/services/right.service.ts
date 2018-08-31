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

    checkIfTranslatorCanInProj(projId: number, rightDefinition: RightDefinition){
        return this.dataService.sendRequest(RequestMethod.Get, 'projects/' + projId + '/right/' + rightDefinition);
    }

    getUserRights(){
        return this.dataService.sendRequest(RequestMethod.Get, 'userprofiles/rights');
    }

    getUserRightsInProject(projId: number){
        return this.dataService.sendRequest(RequestMethod.Get, 'userprofiles' + '/rights/' + projId);
    }
}
