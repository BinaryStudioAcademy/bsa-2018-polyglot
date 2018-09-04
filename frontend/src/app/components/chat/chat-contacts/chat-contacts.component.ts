import { Component, OnInit, EventEmitter, Output } from "@angular/core";
import { ChatService } from "../../../services/chat.service";
import { ChatUser, Project, Team, GroupType } from "../../../models";
import { ChatDialog } from "../../../models/chat/chatDialog";

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

    private dialogs: ChatDialog[] = []; //ChatUser[];
    private projects: Project[] = [];
    private teams: Team[] = [];

    constructor(private chatService: ChatService) {}

    ngOnInit() {
        this.chatService
                .getProjectsList()
                .subscribe((projects: Project[]) => {
                    if (projects) {
                        this.projects = projects;
                    }
                });

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
        }, 500);

        setTimeout(() => {
            this.chatService.getTeamsList().subscribe((teams: Team[]) => {
                if (teams) {
                    this.teams = teams;
                }
            });
        }, 500);
    }

    selectDialog(dialog: ChatDialog) {
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