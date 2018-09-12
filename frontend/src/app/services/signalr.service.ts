import { Injectable } from "@angular/core";
import * as signalR from "@aspnet/signalr";
import { environment } from "../../environments/environment";
import { ChatActions } from "../models/signalrModels/chat-actions";
import { AppStateService } from "./app-state.service";
import { GroupType } from "../models";
import { Hub } from "../models/signalrModels/hub";
import { SignalrGroups } from "../models/signalrModels/signalr-groups";
import { EventEmitter } from "@angular/core";

@Injectable({
    providedIn: "root"
})
export class SignalrService {
    private userConnections: UserSignalRConnection[] = [];

    constructor(private appState: AppStateService) {}

    public connect(groupName: string, hub: Hub) {
        let targetConnection = this.userConnections.find(c => c.hub == hub);
        if (!targetConnection) {
            // создаем подключение
            targetConnection = new UserSignalRConnection(hub, this.appState);
            this.userConnections.push(targetConnection);
            targetConnection.isClosedByUser.subscribe(hub => {
                this.userConnections = this.userConnections.filter(
                    c => c.hub != hub
                );
            });
            targetConnection.connect().then(d => {
                targetConnection.joinGroup(groupName);
            });
        } else {
            targetConnection.joinGroup(groupName);
        }

        return targetConnection;
    }

    public leaveGroup(groupName: string, hub: Hub) {
        const targetConnection = this.userConnections.find(c => c.hub === hub);
        if (targetConnection) {
            targetConnection.leaveGroup(groupName);
        }
    }

    public validateResponse(responce: any): boolean {
        if (
            responce.ids &&
            responce.ids.length > 0 &&
            responce.senderId &&
            responce.senderId !== this.appState.currentDatabaseUser.id
        ) {
            return true;
        } else {
            return false;
        }
    }

    public validateChatResponse(responce: any): boolean {
        if (
            responce.senderId &&
            responce.senderId !== this.appState.currentDatabaseUser.id &&
            responce.dialogId &&
            responce.text &&
            responce.text.length > 0
        ) {
            return true;
        } else {
            return false;
        }
    }

    public readMessage(dialogId: number, interlocutorId: number) {
        let chatHub = this.userConnections.find(c => c.hub == Hub.chatHub);
        if(chatHub)
        {
            chatHub.connection.send(ChatActions[ChatActions.messageRead], dialogId, `${GroupType[GroupType.direct]}${interlocutorId}`);
        }
    }
}

const maxJoinAttemptsСount = 25;
const maxConnectAttemptsСount = 50;

export class UserSignalRConnection {
    private joinGroupAttemptsСount = maxJoinAttemptsСount;
    private connectAttemptsCount = maxJoinAttemptsСount;
    private subscribeAttemptsCount = maxJoinAttemptsСount;
    private groups: string[] = [];
    private isDisconnectedByUser: boolean;
    public connection: any;
    public isClosedByUser = new EventEmitter<Hub>(true);

    public constructor(public hub: Hub, private appState: AppStateService) {}

    public joinGroup(groupName: string) {
        if (this.groups.includes(groupName)) {
            return;
        }

        // 0: 'connecting', 1: 'connected', 2: 'reconnecting', 4: 'disconnected'
        switch (this.connection.connection.connectionState) {
            case 1:
                console.log(
                    `Connection state is ok, joining to group ${groupName}`
                );
                this.connection.send("joinGroup", groupName);
                this.groups.push(groupName);
                this.joinGroupAttemptsСount = maxJoinAttemptsСount;
                break;
            case 0: {
                if (this.joinGroupAttemptsСount < 1) {
                    console.log(
                        `Maximum number of attempts to join group ${groupName} was exhausted`
                    );
                    this.joinGroupAttemptsСount = maxJoinAttemptsСount;
                } else {
                    console.log(`Connection is on connecting state`);
                    setTimeout(() => {
                        this.joinGroupAttemptsСount--;
                        this.joinGroup(groupName);
                        return;
                    }, 1000);
                }
                break;
            }
            case 2: {
                if (this.joinGroupAttemptsСount < 1) {
                    console.log(
                        `Maximum number of attempts to join group ${groupName} was exhausted`
                    );
                    this.joinGroupAttemptsСount = maxJoinAttemptsСount;
                } else {
                    console.log(`Connection is on reconnecting state`);
                    setTimeout(() => {
                        this.joinGroupAttemptsСount--;
                        this.joinGroup(groupName);
                        return;
                    }, 1000);
                }
                break;
            }
            case 4: {
                if (this.joinGroupAttemptsСount < 1) {
                    console.log(
                        `Maximum number of attempts to join group ${groupName} was exhausted`
                    );
                    this.joinGroupAttemptsСount = maxJoinAttemptsСount;
                } else {
                    console.log(`Connection is on disconnected state`);
                    this.joinGroupAttemptsСount--;
                    this.groups.push(groupName);
                    this.reconnect();
                }
                break;
            }
        }
    }

    public leaveGroup(groupName: string) {
        if (this.groups.includes(groupName)) {
            if (this.connection.connection.connectionState === 1) {
                console.log(`Disconnecting from group ${groupName}`);
                this.connection.send("leaveGroup", groupName);
            }
            this.groups = this.groups.filter(g => g !== groupName);
            if (this.groups.length < 1) {
                console.log(`Stoping SignalR connection to ${Hub[this.hub]}`);
                this.isDisconnectedByUser = true;
                this.isClosedByUser.emit(this.hub);
                this.connection.stop();
            }
        }
    }

    public on(methodName: string, newMethod: (...args: any[]) => void): void {
        if (this.connection) {
            this.connection.on(methodName, newMethod);
            this.subscribeAttemptsCount = maxJoinAttemptsСount;
        } else if (this.subscribeAttemptsCount > 0) {
            setTimeout(() => {
                this.subscribeAttemptsCount--;
                this.on(methodName, newMethod);
                return;
            }, 1000);
        }
    }

    public off(methodName: string): void {
        if (this.connection) {
            this.connection.off(methodName);
            this.subscribeAttemptsCount = maxJoinAttemptsСount;
        } else if (this.subscribeAttemptsCount > 0) {
            setTimeout(() => {
                this.subscribeAttemptsCount--;
                this.off(methodName);
                return;
            }, 1000);
        }
    }

    connect(): Promise<void> {
        if (this.connectAttemptsCount < 1) {
            console.log(
                `Maximum number of attempts to connect ${
                    Hub[this.hub]
                } was exhausted`
            );
            return;
        } else {
            if (!this.connection) {
                console.log(`Creating SignalR ${Hub[this.hub]} connection...`);

                switch (this.hub) {
                    case Hub.chatHub:
                        this.connection = new signalR.HubConnectionBuilder()
                            .withUrl(`${environment.apiUrl}/${Hub[this.hub]}`, {
                                accessTokenFactory: () =>
                                    this.appState.currentFirebaseToken
                            })
                            .build();
                        break;
                    default: {
                        this.connection = new signalR.HubConnectionBuilder()
                            .withUrl(`${environment.apiUrl}/${Hub[this.hub]}`)
                            .build();
                        break;
                    }
                }

                this.connection.onclose(err => {
                    console.log(`SignalR hub ${Hub[this.hub]} disconnected.`);
                    setTimeout(() => {
                        if (!this.isDisconnectedByUser) {
                            this.connectAttemptsCount--;
                            this.reconnect();
                        }
                    }, 500);
                });
            }

            return this.reconnect();
        }
    }

    reconnect(): Promise<void> {
        if (!this.connection) {
            this.connect();
        } else {
            console.log(`SignalR hub ${Hub[this.hub]} reconnection started...`);
            if (this.connection.connection.connectionState === 2) {
                const connPromise = this.connection.start();

                connPromise.catch(err => {
                    console.log("SignalR ERROR " + err);
                    setTimeout(() => {
                        this.connectAttemptsCount--;
                        this.reconnect();
                    }, 500);
                });

                connPromise.then(d => {
                    for (let i = 0; i < this.groups.length; i++) {
                        this.joinGroup(this.groups[i]);
                    }
                    this.connectAttemptsCount = maxConnectAttemptsСount;
                });
                return connPromise;
            }
        }
    }
}
