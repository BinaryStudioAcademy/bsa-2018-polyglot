// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
    production: false,
    firebase: {
    apiKey: "AIzaSyBHSSnXoPRFhPcX02D09cKW58KxyH581bc",
    authDomain: "polyglot-bachelor.firebaseapp.com",
    databaseURL: "https://polyglot-bachelor.firebaseio.com",
    projectId: "polyglot-bachelor",
    storageBucket: "polyglot-bachelor.appspot.com",
    messagingSenderId: "158754008633"
  },
    apiUrl: 'http://localhost:58828'
};


/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
