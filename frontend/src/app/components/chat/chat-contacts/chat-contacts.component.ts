import { Component, OnInit, EventEmitter, Output } from "@angular/core";
import { ChatService } from "../../../services/chat.service";
import { ChatUser, Project, Team, GroupType } from "../../../models";
import { ChatDialog } from "../../../models/chat/chatDialog";
import { AppStateService } from "../../../services/app-state.service";
import { SignalrService } from "../../../services/signalr.service";
import { SignalrGroups } from "../../../models/signalrModels/signalr-groups";
import { Hub } from "../../../models/signalrModels/hub";
import { ChatActions } from "../../../models/signalrModels/chat-actions";

@Component({
    selector: "app-chat-contacts",
    templateUrl: "./chat-contacts.component.html",
    styleUrls: ["./chat-contacts.component.sass"]
})
export class ChatContactsComponent implements OnInit {
    @Output() onItemSelect = new EventEmitter<any>(true);
    public isOnPersonsPage: boolean;
    currentUserId: number;
    selectedDialogId: number;
    step = 2;

    unreadMessagesTotal = {
        persons: 0,
        groups: 0,
        teams: 0,
        projects: 0
    };

    private dialogs: ChatDialog[] = []; //ChatUser[];
    private projects: ChatDialog[] = [];
    private teams: Team[] = [];

    constructor(
        private chatService: ChatService,
        private appState: AppStateService,
        private signalRService: SignalrService
    ) {
        this.isOnPersonsPage = true;
    }

    ngOnInit() {

        this.currentUserId = this.appState.currentDatabaseUser.id;
        this.signalRService.createConnection(
            `${SignalrGroups[SignalrGroups.direct]}${this.currentUserId}`,
            Hub[Hub.chatHub]
        );
        this.subscribeChatEvents();

        setTimeout(() => {
        this.chatService
                .getProjectsList()
                .subscribe((dialogs: ChatDialog[]) => {
                    if (dialogs) {
                        this.projects = dialogs;
                    }
                });
            }, 500);

        setTimeout(() => {
            this.chatService
                .getDialogs()
                .subscribe((dialogs: ChatDialog[]) => {
                    debugger;
                    if (
                        dialogs &&
                        dialogs.length > 0
                    ) {
                        this.dialogs = dialogs.filter(d => d.participants.length > 0);
                    }
                });
        }, 1000);

        setTimeout(() => {
            this.chatService.getTeamsList().subscribe((teams: Team[]) => {
                if (teams) {
                    this.teams = teams;
                }
            });
        }, 500);
    }

    ngOnDestroy(){
        this.signalRService.closeConnection(
            `${SignalrGroups[SignalrGroups.direct]}${this.currentUserId}`
       );
    }

    subscribeChatEvents() {
        this.signalRService.connection.on(
            ChatActions[ChatActions.messageReceived],
            (responce: any) => {
                if(this.signalRService.validateChatResponse(responce) 
                && this.selectedDialogId 
                && this.selectedDialogId != responce.dialogId)
                {
                    let targetDialogIndex = this.dialogs.findIndex(d => d.id == responce.dialogId);
                    if(targetDialogIndex >= 0)
                    {
                        this.dialogs[targetDialogIndex].lastMessageText = responce.text;
                        this.dialogs[targetDialogIndex].unreadMessagesCount++;
                        this.unreadMessagesTotal['persons']++;
                    }
                }
            }
        );
    }

    selectDialog(dialog: ChatDialog) {
        dialog.unreadMessagesCount = 0;
        this.selectedDialogId = dialog.id;
        this.onItemSelect.emit(dialog);
    }

    setStep(index: number) {
        this.step = index;
    }

    nextStep() {
        this.step++;
    }

    prevStep() {
        this.step--;
    }
}