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
        this.connect(
            groupName,
            hubUrl
        ).then(() => {
            this.connection.onclose(err => {
                console.log(`SignalR hub ${hubUrl} disconnected.`);
                this.createConnection(groupName, hubUrl);
            });
        });
    }

    public closeConnection(groupName: string) {
        if (this.connection) {
            this.connection.send("leaveProjectGroup", groupName);
            this.connection.stop();
        }
    }

    connect(groupName: string, hubUrl: string): Promise<void> {
        if (!this.connection) {
            console.log(`SignalR hub ${hubUrl} connection is corrupted.Creating new one...`);
            this.connection = new signalR.HubConnectionBuilder()
                .withUrl(`${environment.apiUrl}/${hubUrl}`)
                .build();
        }
        console.log(`SignalR hub ${hubUrl} reconnection started...`);
        return this.connection
            .start()
            .catch(err => console.log("SignalR ERROR " + err))
            .then(data => {
                console.log(`SignalR hub ${hubUrl} connected.Creating group ${groupName}.`);
                this.connection.send("joinProjectGroup", groupName);
            });
    }
}
