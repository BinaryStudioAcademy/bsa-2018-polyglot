import {
    Component,
    OnInit,
    ViewChild,
    ElementRef,
    Renderer2,
    Input,
    SimpleChanges,
    Output,
    EventEmitter
} from "@angular/core";
import { ProjectService } from "../../../services/project.service";
import { MatSnackBar } from "@angular/material";
import { ChatService } from "../../../services/chat.service";
import { GroupType, ChatMessage, ChatUser } from "../../../models";
import { SignalrService } from "../../../services/signalr.service";
import { SignalrGroups } from "../../../models/signalrModels/signalr-groups";
import { AppStateService } from "../../../services/app-state.service";
import { Hub } from "../../../models/signalrModels/hub";
import { ChatActions } from "../../../models/signalrModels/chat-actions";
import { ChatDialog } from "../../../models/chat/chatDialog";

@Component({
    selector: "app-chat-window",
    templateUrl: "./chat-window.component.html",
    styleUrls: ["./chat-window.component.sass"]
})
export class ChatWindowComponent implements OnInit {
    @ViewChild("mainwindow") mainWindow: ElementRef;
    @Output() onPersonSelect = new EventEmitter<any>(true);
    @Input() dialog: any;
    interlocutors = {};
    currentInterlocutorId: number;
    messages = [];
    
    public currentMessage: string = "";
    public isDirect: boolean;
    public currentUserId: number;
    public currentGroupName: string;
    public groupParticipantsCount: number;
    public groupParticipantsOnlineCount: number;

    private signalRConnection;

    constructor(
        private appState: AppStateService,
        private renderer: Renderer2,
        private chatService: ChatService,
        public snackBar: MatSnackBar,
        private signalRService: SignalrService
    ) {}

    ngOnInit() {
        this.currentUserId = this.appState.currentDatabaseUser.id;
        this.subscribeChatEvents();
    }

    ngOnDestroy() {
        this.signalRService.leaveGroup(`${SignalrGroups[SignalrGroups.dialog]}${this.dialog.id}`, Hub.chatHub);
    }

    subscribeChatEvents() {
        this.signalRConnection.on(
            ChatActions[ChatActions.messageReceived],
            (responce: any) => {
                if(this.dialog.id == responce.dialogId && this.signalRService.validateChatResponse(responce))
                {
                    this.chatService.getMessage(responce.messageId)
                        .subscribe((message: ChatMessage) => {
                            this.messages.push(message);
                        });
                }
            }
        );

        this.signalRConnection.on(
            ChatActions[ChatActions.messageRead],
            (userId: number) => {
                debugger;
                if(this.interlocutors[userId]){
                    for(let i = 0; i < this.messages.length; i++){
                        this.messages[i].isRead = true;
                    }
                }
            }
        );
    }

    ngOnChanges(changes: SimpleChanges) {
        debugger;
        this.messages = [];
        
        if(changes.dialog.previousValue) {
            this.signalRService.leaveGroup(
                `${SignalrGroups[SignalrGroups.dialog]}${changes.dialog.previousValue.id}`,
                Hub.chatHub
            );
        }

        if(this.dialog){
            this.signalRConnection = this.signalRService.connect(
                `${SignalrGroups[SignalrGroups.dialog]}${this.dialog.id}`,
                Hub.chatHub
            );
            this.interlocutors = {};
            this.currentInterlocutorId = -1;

            switch(this.dialog.dialogType){
                case(0):
                case(1):
                this.currentInterlocutorId = this.dialog.participants[0].id;
                this.interlocutors[this.currentInterlocutorId] = this.dialog.participants[0];
                this.isDirect = true;
                break;
                case(2):
                for(let i = 0; i < this.dialog.participants.length; i++)
                {
                    this.interlocutors[this.dialog.participants[i].id] = this.dialog.participants[i];
                }
                this.currentGroupName = this.dialog.dialogName; 
                this.groupParticipantsCount = this.dialog.participants.length;
                this.groupParticipantsOnlineCount = this.dialog.participants.filter(p => p.isOnline).length;
                this.isDirect = false;
                break;
                case(3):
                for(let i = 0; i < this.dialog.participants.length; i++)
                {
                    this.interlocutors[this.dialog.participants[i].id] = this.dialog.participants[i];
                }
                this.currentGroupName = this.dialog.dialogName; 
                this.groupParticipantsCount = this.dialog.participants.length;
                this.groupParticipantsOnlineCount = this.dialog.participants.filter(p => p.isOnline).length;
                this.isDirect = false;
                break;
            }
        }
        this.getMessagesHistory();
    }

    ngAfterViewChecked() {
        let scrollHeight = this.mainWindow.nativeElement.scrollHeight;
        this.renderer.setProperty(
            this.mainWindow.nativeElement,
            "scrollTop",
            scrollHeight
        );
    }

    getMessagesHistory() {

        if (this.dialog && this.dialog.participants && this.dialog.participants.length > 0) {
            let targetGroup;
            let targetGroupDialogId;
            switch(this.dialog.dialogType){
                case(0):
                case(1):
                targetGroup = GroupType.users;
                targetGroupDialogId = this.currentInterlocutorId;
                break;
                case(2):
                targetGroup = GroupType.projects;
                targetGroupDialogId = this.dialog.identifier;
                break;
                case(3):
                targetGroupDialogId = this.dialog.identifier;
                targetGroup = GroupType.teams;
                break;
            }
            this.chatService
                .getDialogMessages(targetGroup, targetGroupDialogId)
                .subscribe(messages => {
                    if (messages) {
                        debugger;

                        if(this.isDirect)
                        {
                            this.messages = messages.filter(m => m.senderId == this.currentInterlocutorId || 
                                    m.senderId == this.currentUserId);
                            this.signalRService.readMessage(this.currentInterlocutorId);
                        }
                        else 
                        {
                            this.messages = messages.filter(m => this.interlocutors[m.senderId]);
                        }
                    }
                });
        }
    }

    sendMessage() {
        if (this.currentMessage.length > 0) {
            let message = {
            dialogId: this.dialog.id,
            body: this.currentMessage
            };
            this.currentMessage = "";

            this.chatService.sendMessage(GroupType.users,
                message).subscribe((message: ChatMessage) => {
                    debugger;
                    if(message){
                        this.messages.push(message);
                    }
                });
        }
    }

    selectPerson(person){
        if(person)
        {
            this.onPersonSelect.emit(person);
        }
    }

    toggleSelection(message) {
        this.openSnackBar();
    }

    openSnackBar() {
        this.snackBar.open("mmmmmmmm", "sdfsdf", {
            duration: 2000
        });
    }
}