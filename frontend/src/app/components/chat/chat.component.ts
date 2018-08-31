import { Component, OnInit, ChangeDetectorRef, ViewChild, ElementRef, Renderer2 } from "@angular/core";
import { Observable, of } from "rxjs";
import { MediaMatcher } from "@angular/cdk/layout";
import { SignalrService } from "../../services/signalr.service";
import { ChatActions } from "../../models/signalrModels/chat-actions";
import { SignalrGroups } from "../../models/signalrModels/signalr-groups";
import { Hub } from "../../models/signalrModels/hub";
import { ProjectService } from "../../services/project.service";

@Component({
    selector: "app-chat",
    templateUrl: "./chat.component.html",
    styleUrls: ["./chat.component.sass"]
})
export class ChatComponent implements OnInit {
    mobileQuery: MediaQueryList;
    private _mobileQueryListener: () => void;

    constructor(
        private renderer: Renderer2,
        private changeDetectorRef: ChangeDetectorRef,
        private media: MediaMatcher,
        private signalRService: SignalrService
    ) {
        this.mobileQuery = media.matchMedia("(max-width: 600px)");
        this._mobileQueryListener = () => changeDetectorRef.detectChanges();
        this.mobileQuery.addListener(this._mobileQueryListener);
        //this.renderer.setStyle(this.el.nativeElement, 'color', 'blue');
        
    }

    ngOnInit() {
        this.signalRService.createConnection(
            SignalrGroups[SignalrGroups.chatShared],
            Hub[Hub.chatHub]
        );
        this.subscribeChatEvents();
    }

    ngOnDestroy() {
        this.mobileQuery.removeListener(this._mobileQueryListener);
        this.signalRService.closeConnection(
            SignalrGroups[SignalrGroups.chatShared]
        );
    }

    subscribeChatEvents() {
        //this.signalRService.connection.on(
        //    ChatActions[ChatActions.messageReceived],
        //    (message: string) => {
        //        this.messages.push({
        //            body: message,
        //            date: "21.2.2018",
        //            user: {
        //                fullName: "Julia Louis-Dreyfus",
        //                avatarUrl:
        //                    "https://www.randomlists.com/img/people/julia_louis_dreyfus.jpg",
        //                isOnline: true
        //            }
        //        });
        //      this.renderer.setProperty(this.mainWindow.nativeElement, 'scrollTop', '99999');
        //    }
        //);
    }

    
}