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
    @ViewChild('mainwindow') mainWindow: ElementRef;
    mobileQuery: MediaQueryList;
    private _mobileQueryListener: () => void;
    public currentMessage: string = "";

    messages = [];

    constructor(
        private renderer: Renderer2,
        private changeDetectorRef: ChangeDetectorRef,
        private media: MediaMatcher,
        private projectService: ProjectService,
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
        this.messages = MOCK_MESSAGES;
    }

    ngOnDestroy() {
        this.mobileQuery.removeListener(this._mobileQueryListener);
        this.signalRService.closeConnection(
            SignalrGroups[SignalrGroups.chatShared]
        );
    }

    subscribeChatEvents() {
        this.signalRService.connection.on(
            ChatActions[ChatActions.messageReceived],
            (message: string) => {
                this.messages.push({
                    body: message,
                    date: "21.2.2018",
                    user: {
                        fullName: "Julia Louis-Dreyfus",
                        avatarUrl:
                            "https://www.randomlists.com/img/people/julia_louis_dreyfus.jpg",
                        isOnline: true
                    }
                });
              this.renderer.setProperty(this.mainWindow.nativeElement, 'scrollTop', '99999');
            }
        );
    }

    sendMessage() {
        if (this.currentMessage.length > 0) {
            this.signalRService.sendMessage("", this.currentMessage);
        }
    }
}

const MOCK_MESSAGES = [
    {
        body:
            "Do you know the difference between education and experience? Education is what you get when you read the fine print; experience is what you get when you don't",
        date: "21.2.2018",
        user: {
            fullName: "Julia Louis-Dreyfus",
            avatarUrl:
                "https://www.randomlists.com/img/people/julia_louis_dreyfus.jpg",
            isOnline: false
        }
    },
    {
        body: "No wonder you're tired! You understood so much today. ",
        date: "21.2.2018",
        user: {
            fullName: "Jennifer Love Hewitt",
            avatarUrl:
                "https://www.randomlists.com/img/people/jennifer_love_hewitt.jpg",
            isOnline: true
        }
    },
    {
        body:
            "My father, a good man, told me, 'Never lose your ignorance; you cannot replace it.'",
        date: "21.2.2018",
        user: {
            fullName: "Natalya Rudakova",
            avatarUrl:
                "https://www.randomlists.com/img/people/natalya_rudakova.jpg",
            isOnline: true
        }
    },
    {
        body:
            "If truth is beauty, how come no one has their hair done in the library?",
        date: "21.2.2018",
        user: {
            fullName: "Natalya Rudakova",
            avatarUrl:
                "https://www.randomlists.com/img/people/natalya_rudakova.jpg",
            isOnline: true
        }
    },
    {
        body:
            "Ignorance must certainly be bliss or there wouldn't be so many people so resolutely pursuing it.",
        date: "21.2.2018",
        user: {
            fullName: "Jennifer Love Hewitt",
            avatarUrl:
                "https://www.randomlists.com/img/people/jennifer_love_hewitt.jpg",
            isOnline: true
        }
    },
    {
        body: "the high school after high school!",
        date: "21.2.2018",
        user: {
            fullName: "Natalya Rudakova",
            avatarUrl:
                "https://www.randomlists.com/img/people/natalya_rudakova.jpg",
            isOnline: true
        }
    },
    {
        body: "A professor is one who talks in someone else's sleep. ",
        date: "21.2.2018",
        user: {
            fullName: "Natalya Rudakova",
            avatarUrl:
                "https://www.randomlists.com/img/people/natalya_rudakova.jpg",
            isOnline: true
        }
    },
    {
        body: "Never let your schooling interfere with your education. ",
        date: "21.2.2018",
        user: {
            fullName: "Jennifer Love Hewitt",
            avatarUrl:
                "https://www.randomlists.com/img/people/jennifer_love_hewitt.jpg",
            isOnline: true
        }
    },
    {
        body:
            "About all some men accomplish in life is to send a son to Harvard. ",
        date: "21.2.2018",
        user: {
            fullName: "Jennifer Love Hewitt",
            avatarUrl:
                "https://www.randomlists.com/img/people/jennifer_love_hewitt.jpg",
            isOnline: true
        }
    },
    {
        body: " He that teaches himself has a fool for a master",
        date: "21.2.2018",
        user: {
            fullName: "Hugh Jackman",
            avatarUrl:
                "https://www.randomlists.com/img/people/hugh_jackman.jpg",
            isOnline: false
        }
    },
    {
        body:
            "You may have heard that a dean is to faculty as a hydrant is to a dog",
        date: "21.2.2018",
        user: {
            fullName: "Natalya Rudakova",
            avatarUrl:
                "https://www.randomlists.com/img/people/natalya_rudakova.jpg",
            isOnline: true
        }
    },
    {
        body:
            "The world is coming to an end! Repent and return those library books!",
        date: "21.2.2018",
        user: {
            fullName: "Jennifer Love Hewitt",
            avatarUrl:
                "https://www.randomlists.com/img/people/jennifer_love_hewitt.jpg",
            isOnline: true
        }
    },
    {
        body: "This is the sort of English up with which I will not put",
        date: "21.2.2018",
        user: {
            fullName: "Hugh Jackman",
            avatarUrl:
                "https://www.randomlists.com/img/people/hugh_jackman.jpg",
            isOnline: false
        }
    },
    {
        body:
            "So, is the glass half empty, half full, or just twice as large as it needs to be? ",
        date: "21.2.2018",
        user: {
            fullName: "Julia Louis-Dreyfus",
            avatarUrl:
                "https://www.randomlists.com/img/people/julia_louis_dreyfus.jpg",
            isOnline: false
        }
    },
    {
        body: "OK, now let's look at four dimensions on the blackboard.",
        date: "21.2.2018",
        user: {
            fullName: "Hugh Jackman",
            avatarUrl:
                "https://www.randomlists.com/img/people/hugh_jackman.jpg",
            isOnline: false
        }
    },
    {
        body: "Having a wonderful wine, wish you were beer. ",
        date: "21.2.2018",
        user: {
            fullName: "Theodore Roosevelt",
            avatarUrl:
                "https://www.randomlists.com/img/people/theodore_roosevelt.jpg",
            isOnline: false
        }
    }
];