// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
    production: false,
    firebase: {
        apiKey: "AIzaSyDgXd8_8yIRLd5G5KWHmH60NlRl_qY6vGU",
        authDomain: "polyglot-dbc9a.firebaseapp.com",
        databaseURL: "https://polyglot-dbc9a.firebaseio.com",
        projectId: "polyglot-dbc9a",
        storageBucket: "polyglot-dbc9a.appspot.com",
        messagingSenderId: "366581495425"
    },
    apiUrl: 'localhost:58828'
};


/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
