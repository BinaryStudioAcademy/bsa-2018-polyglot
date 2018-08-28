import { Injectable } from "@angular/core";
import * as signalR from "@aspnet/signalr";
import { environment } from "../../environments/environment";
import { HubConnection } from "@aspnet/signalr";

@Injectable({
    providedIn: "root"
})
export class SignalrService {
    connection: any;
    isWorking: boolean = true;

    constructor() {}

    public createConnection(groupName: string, hubUrl: string) {
        if (
            (!this.connection ||
                this.connection.connection.connectionState === 2) &&
            this.isWorking
        ) {
            this.connect(
                groupName,
                hubUrl
            ).then(data => {
                console.log(`SignalR hub ${hubUrl} connected.`);
                if (this.connection.connection.connectionState === 1) {
                    console.log(`Connecting to group ${groupName}`);
                    this.connection.send("joinProjectGroup", groupName);
                }
            });
        } else {
            if (this.connection.connection.connectionState === 1) {
                console.log(`Connecting to group ${groupName}`);
                this.connection.send("joinProjectGroup", groupName);
            }
        }
    }

    public closeConnection(groupName: string) {
        this.isWorking = false;
        if (
            this.connection &&
            this.connection.connection.connectionState === 1
        ) {
            console.log(`Disconnecting from group ${groupName}`);
            this.connection.send("leaveProjectGroup", groupName);
            console.log(`Stoping SignalR connection`);
            this.connection.stop();
        }
    }

    connect(groupName: string, hubUrl: string): Promise<void> {
        if (!this.connection) {
            console.log(
                `SignalR hub ${hubUrl} connection is corrupted.Creating new one...`
            );
            this.connection = new signalR.HubConnectionBuilder()
                .withUrl(`${environment.apiUrl}/${hubUrl}`)
                .build();

            this.connection.onclose(err => {
                console.log(`SignalR hub ${hubUrl} disconnected.`);
                this.createConnection(groupName, hubUrl);
            });
        }
        console.log(`SignalR hub ${hubUrl} reconnection started...`);
        return this.connection
            .start()
            .catch(err => console.log("SignalR ERROR " + err));
    }
}
