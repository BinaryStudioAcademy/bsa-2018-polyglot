import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { SessionStorage } from "ngx-store";
import { environment } from '../../environments/environment';
import { AppStateService } from './app-state.service';

@Injectable({
    providedIn: 'root'
})
export class HttpService {

    private url: string = environment.apiUrl;

    // private _token: string;

    // public set token(v : string) {
    //     this._token = v;
    // }
    
    public get token(): string {
        return `Bearer ${this.appState.currentFirebaseToken}`;
    }


    constructor(private httpClient: HttpClient, private appState: AppStateService) { 
    }

    sendRequest(
        type: RequestMethod,
        endpoint: string,
        params: number | string = "",
        body: any = {},
        respType: string = 'json',
        typeOfContent: string = "json") {

        let headers;
        if ((type === RequestMethod.Post || type === RequestMethod.Put) && typeOfContent == 'json') {
            headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': this.token });
        } else {
            headers = new HttpHeaders({ 'Authorization': this.token });
        }
        headers.append('Access-Control-Allow-Origin', '*');

        let request: Observable<any>;

        switch (type) {
            case RequestMethod.Get:
                if(respType === 'json'){
                    request = this.httpClient.get(`${this.url}/${endpoint}/${params}`, { responseType: 'json' , headers });
                }else if(respType === 'blob'){
                    request = this.httpClient.get(`${this.url}/${endpoint}/${params}`, { responseType: 'blob' , headers });
                }
                break;
            case RequestMethod.Post:
                request = this.httpClient.post(`${this.url}/${endpoint}/${params}`, body, { headers });
                break;
            case RequestMethod.Put:
                request = this.httpClient.put(`${this.url}/${endpoint}/${params}`, body, { headers });
                break;
            case RequestMethod.Delete:
                const httpOptions = {
                    headers: headers, body: body
                };
                request = this.httpClient.delete(`${this.url}/${endpoint}/${params}`, httpOptions);
                break;
        }
        
        return request.pipe(
            catchError((res: HttpErrorResponse) => this.handleError(res))
        );
    }

    protected handleError(error: HttpErrorResponse | any): any {
        let errMsg: string;
        let errorData: any;
        if (error instanceof HttpErrorResponse) {
            errorData = error.error || '';
            const err = errorData || JSON.stringify(errorData);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.Message ? error.Message : error.toString();
        }
        console.error(errMsg);

        if (errorData) {
            return throwError(errorData);
        }

        return throwError(errMsg);
    }
}


export enum RequestMethod {
    Get,
    Post,
    Put,
    Delete,
}
