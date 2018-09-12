import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, from, of } from 'rxjs';
import { catchError, flatMap } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { environment } from '../../environments/environment';
import { AppStateService } from './app-state.service';
import { AuthService } from './auth.service';

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

    constructor(private httpClient: HttpClient, private appState: AppStateService, private authService: AuthService) {
    }

    sendRequest(
        type: RequestMethod,
        endpoint: string,
        params: number | string = "",
        body: any = {},
        respType: string = 'json',
        typeOfContent: string = "json") {

        return this.createRequest(type, endpoint, params, body, respType, typeOfContent).pipe(
            catchError((res: HttpErrorResponse) => this.handleError(res)),
            flatMap((response: any) => {
                if (response === "T") {
                    return from(this.authService.refreshToken()).pipe(flatMap(
                        () => this.createRequest(type, endpoint, params, body, respType, typeOfContent)
                            .pipe(
                                catchError((res: HttpErrorResponse) => this.handleError(res)),
                                flatMap((response: any) => {
                                    if (response === "T") {
                                        from(this.authService.logout()).subscribe(() => {})
                                    } else {
                                        return of(response);
                                    }
                                })
                            )
                    ))
                } else {
                    return of(response);
                }
            })
        );
    }

    createRequest(type: RequestMethod,
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
                if (respType === 'json') {
                    request = this.httpClient.get(`${this.url}/${endpoint}/${params}`, { responseType: 'json', headers });
                } else if (respType === 'blob') {
                    request = this.httpClient.get(`${this.url}/${endpoint}/${params}`, { responseType: 'blob', headers });
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

        return request;
    }



    protected handleError(error: HttpErrorResponse | any): any {
        let errMsg: string;
        let errorMsg = 'Error: Unable to complete request.';
        let errorData: any;
        if (error instanceof HttpErrorResponse) {
            errorData = error.error || '';
            const err = errorData || JSON.stringify(errorData);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;

            errorMsg = err.message;
            if (error.status === 401) {
                console.log('The authentication session expires or the user is not authorised. Force refresh of the current page.');
                return 'T';

            }

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
