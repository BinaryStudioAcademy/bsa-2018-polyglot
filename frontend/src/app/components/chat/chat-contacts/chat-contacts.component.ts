import { Component, OnInit, EventEmitter, Output } from "@angular/core";
import { ChatService } from "../../../services/chat.service";
import { ChatUser, Project, Team, GroupType } from "../../../models";

@Component({
    selector: "app-chat-contacts",
    templateUrl: "./chat-contacts.component.html",
    styleUrls: ["./chat-contacts.component.sass"]
})
export class ChatContactsComponent implements OnInit {
    @Output()
    onItemSelect = new EventEmitter<any>(true);
    step = 2;
    unreadProject: number[];
    unreadTeam: number[];
    unreadPerson: number[];

    teamBadge = true;
    projectBadge = true;
    personBadge = false;

    private users: any; //ChatUser[];
    private projects: Project[] = [];
    private teams: Team[] = [];

    constructor(private chatService: ChatService) {}

    ngOnInit() {
        this.users = MOCK_USERS;
        this.onItemSelect.emit(this.users[0]);

        setTimeout(() => {
            this.chatService
                .getProjectsList()
                .subscribe((projects: Project[]) => {
                    if (projects) {
                        debugger;
                        this.projects = projects;
                    }
                });
        }, 500);

        setTimeout(() => {
            this.chatService
                .getContacts(GroupType.users, 1)
                .subscribe(users => {
                    debugger;
                    if (
                        users &&
                        users.contactList &&
                        users.contactList.length
                    ) {
                        Array.prototype.push.apply(
                            this.users,
                            users.contactList
                        );
                    }
                });
        }, 500);

        this.chatService.getTeamsList().subscribe((teams: Team[]) => {
            if (teams) {
                debugger;
                this.teams = teams;
            }
        });
    }

    selectUser(user: ChatUser) {
        this.onItemSelect.emit(user);
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

const MOCK_USERS = [
    {
        id: 1,
        fullName: "Theodore Roosevelt",
        avatarUrl:
            "https://www.randomlists.com/img/people/theodore_roosevelt.jpg",
        isOnline: true,
        lastSeen: new Date("February 4, 2016 10:13:00"),
        lastMessageText: "Mock message from front"
    },
    {
        id: 1,
        fullName: "Jennifer Love Hewitt",
        avatarUrl:
            "https://www.randomlists.com/img/people/jennifer_love_hewitt.jpg",
        isOnline: true,
        lastSeen: new Date("February 4, 2016 10:13:00"),
        lastMessageText: "Mock message from front"
    },
    {
        id: 1,
        fullName: "Hugh Jackman",
        avatarUrl: "https://www.randomlists.com/img/people/hugh_jackman.jpg",
        isOnline: false,
        lastSeen: new Date("May 1, 2018 12:17:00"),
        lastMessageText: "Mock message from front"
    },
    {
        id: 1,
        fullName: "George Pal",
        avatarUrl: "https://www.randomlists.com/img/people/george_pal.jpg",
        isOnline: false,
        lastSeen: new Date("February 4, 2016 10:13:00"),
        lastMessageText: "Mock message from front"
    },
    {
        id: 1,
        fullName: "Tina Fey",
        avatarUrl: "https://www.randomlists.com/img/people/tina_fey.jpg",
        isOnline: false,
        lastSeen: new Date("May 13, 2017 12:17:00"),
        lastMessageText: "Mock message from front"
    },
    {
        id: 1,
        fullName: "Owen Wilson",
        avatarUrl: "https://www.randomlists.com/img/people/owen_wilson.jpg",
        isOnline: false,
        lastSeen: new Date("August 15, 2018 5:17:00"),
        lastMessageText: "Mock message from front"
    },
    {
        id: 1,
        fullName: "Robert De Niro",
        avatarUrl: "https://www.randomlists.com/img/people/robert_de_niro.jpg",
        isOnline: true,
        lastSeen: new Date("February 4, 2016 10:13:00"),
        lastMessageText: "Mock message from front"
    },
    {
        id: 1,
        fullName: "Julia Louis-Dreyfus",
        avatarUrl:
            "https://www.randomlists.com/img/people/julia_louis_dreyfus.jpg",
        isOnline: false,
        lastSeen: new Date("July 27, 2017 17:26:00"),
        lastMessageText: "Mock message from front"
    },
    {
        id: 1,
        fullName: "Natalya Rudakova",
        avatarUrl:
            "https://www.randomlists.com/img/people/natalya_rudakova.jpg",
        isOnline: false,
        lastSeen: new Date("July 11, 2018 12:17:00"),
        lastMessageText: "Mock message from front"
    },
    {
        id: 1,
        fullName: "Jennifer Love Hewitt",
        avatarUrl:
            "https://www.randomlists.com/img/people/jennifer_love_hewitt.jpg",
        isOnline: false,
        lastSeen: new Date("May 1, 2018 12:17:00"),
        lastMessageText: "Mock message from front"
    },
    {
        id: 1,
        fullName: "William Shatner",
        avatarUrl: "https://www.randomlists.com/img/people/william_shatner.jpg",
        isOnline: false,
        lastSeen: new Date("May 18, 2018 18:45:00"),
        lastMessageText: "Mock message from front"
    }
];
