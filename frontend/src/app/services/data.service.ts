import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor() { }

  // TODO: GET RIG OF IT!!!
  getAppTitle() {
    return 'Polyglot';
  }
}
