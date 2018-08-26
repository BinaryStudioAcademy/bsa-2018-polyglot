import { Injectable } from "@angular/core";
import * as signalR from "@aspnet/signalr";
import { environment } from "../../environments/environment";
import { HubConnection } from "@aspnet/signalr";

@Injectable({
    providedIn: "root"
})
export class SignalrService {
    connection: HubConnection;

    constructor() {}

    public createConnection(groupName: string, hubUrl: string) {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(`${environment.hubUrl}/${hubUrl}`)
            .build();

        var connectionPromise = this.connection
            .start()
            .catch(err => console.log("ERROR " + err));

        connectionPromise.then(data => {
            debugger;
            console.log(data);
            if (data) {
                this.connection.send("joinProjectGroup", groupName);
            }
        });
    }

    public closeConnection(groupName: string) {
        this.connection.send("leaveProjectGroup", groupName);
        this.connection.stop();
    }
}
