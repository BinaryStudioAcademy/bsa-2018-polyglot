import { Component, OnInit, ChangeDetectorRef, ViewChild, ElementRef, Renderer2 } from "@angular/core";
import { Observable, of } from "rxjs";
import { MediaMatcher } from "@angular/cdk/layout";
import { SignalrService } from "../../services/signalr.service";
import { ChatActions } from "../../models/signalrModels/chat-actions";
import { SignalrGroups } from "../../models/signalrModels/signalr-groups";
import { Hub } from "../../models/signalrModels/hub";
import { ProjectService } from "../../services/project.service";
import { ChatService } from "../../services/chat.service";
import { AppStateService } from "../../services/app-state.service";
import { ChatMessage } from "../../models";

@Component({
    selector: "app-chat",
    templateUrl: "./chat.component.html",
    styleUrls: ["./chat.component.sass"]
})
export class ChatComponent implements OnInit {
    selectedUser: any = 
    {
        fullName: ""
    };
    mobileQuery: MediaQueryList;
    private _mobileQueryListener: () => void;

    constructor(
        private renderer: Renderer2,
        private changeDetectorRef: ChangeDetectorRef,
        private media: MediaMatcher,
        private signalRService: SignalrService,
        private chatService: ChatService,
        private appState: AppStateService
    ) {
        this.mobileQuery = media.matchMedia("(max-width: 600px)");
        this._mobileQueryListener = () => changeDetectorRef.detectChanges();
        this.mobileQuery.addListener(this._mobileQueryListener);
        //this.renderer.setStyle(this.el.nativeElement, 'color', 'blue');
        
    }

    ngOnInit() {
        setTimeout(() => {
            this.signalRService.createConnection(
                SignalrGroups[SignalrGroups.chatShared],
                Hub[Hub.chatHub]
            );
        }, 1000);

        this.signalRService.createConnection(
            `${SignalrGroups[SignalrGroups.chatUser]}${this.appState.currentDatabaseUser.id}`,
            Hub[Hub.chatHub]
        );
        this.subscribeChatEvents();
    }

    ngOnDestroy() {
        this.mobileQuery.removeListener(this._mobileQueryListener);
        this.signalRService.closeConnection(
            SignalrGroups[SignalrGroups.chatShared]
        );
        this.signalRService.closeConnection(
            `${SignalrGroups[SignalrGroups.chatUser]}
            ${this.appState.currentDatabaseUser.id}`
        );
    }

    onSelected($event){
        debugger;
        this.selectedUser = $event;
    }

    subscribeChatEvents() {
        this.signalRService.connection.on(
            ChatActions[ChatActions.messageReceived],
            (message: ChatMessage) => {
                console.log("message received");
                console.log(message);
            }
        );
    }

    
}