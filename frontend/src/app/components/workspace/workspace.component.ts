import { Component, OnInit, OnDestroy, DoCheck } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Subscription } from "rxjs";
import { Project, Language, UserProfile } from "../../models";
import { ProjectService } from "../../services/project.service";
import { MatDialog } from "@angular/material";
import { StringDialogComponent } from "../../dialogs/string-dialog/string-dialog.component";
import { SnotifyService } from "ng-snotify";
import { FormControl } from "../../../../node_modules/@angular/forms";
import { AppStateService } from "../../services/app-state.service";
import { UserService } from "../../services/user.service";
import { ComplexStringService } from "../../services/complex-string.service";
import { SignalrGroups } from "../../models/signalrModels/signalr-groups";
import { SignalrService } from "../../services/signalr.service";
import { SignalrSubscribeActions } from "../../models/signalrModels/signalr-subscribe-actions";

@Component({
    selector: "app-workspace",
    templateUrl: "./workspace.component.html",
    styleUrls: ["./workspace.component.sass"]
})
export class WorkspaceComponent implements OnInit, OnDestroy, DoCheck {
    public project: Project;
    public keys: any[] = [];
    public searchQuery: string;
    public selectedKey: any;
    public isEmpty;
    public currentPath;
    public basicPath;
    user: UserProfile;
    private currentPage = 0;
    private elementsOnPage = 7;
    public isLoad: boolean;

    private routeSub: Subscription;

    options = new FormControl();

    filterOptions: string[] = [
        "Translated",
        "Untranslated",
        "Human Translation",
        "Machine Translation",
        "With Tags"
    ];

    constructor(
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private dataProvider: ProjectService,
        private dialog: MatDialog,
        private projectService: ProjectService,
        private snotifyService: SnotifyService,
        private appState: AppStateService,
        private userService: UserService,
        private complexStringService: ComplexStringService,
        private signalrService: SignalrService
    ) {
        this.user = userService.getCurrentUser();
    }

    description: string = "Are you sure you want to remove the project?";
    btnOkText: string = "Delete";
    btnCancelText: string = "Cancel";
    answer: boolean;

    ngOnInit() {
        this.searchQuery = "";
        this.routeSub = this.activatedRoute.params.subscribe(params => {
            //making api call using service service.get(params.projectId); ..
            this.dataProvider.getById(params.projectId).subscribe(proj => {
                this.project = proj;

                this.projectService.getProjectLanguages(this.project.id).subscribe(
                    (d: Language[]) => {
                        const workspaceState = {
                            projectId: this.project.id,
                            languages: d
                        };

                        this.appState.setWorkspaceState = workspaceState;
                        this.signalrService.createConnection(
                            `${SignalrGroups[SignalrGroups.project]}${
                            this.project.id
                            }`,
                            "workspaceHub"
                        );
                        this.subscribeProjectChanges();
                    },
                    err => {
                        this.keys = null;
                        this.isLoad = false;
                        console.log("err", err);
                    },
                );

            });
            this.basicPath = 'workspace/' + params.projectId;
            this.currentPath = 'workspace/' + params.projectId + '/key';
            this.dataProvider.getProjectStringsWithPagination(params.projectId, this.elementsOnPage, 0)
                .subscribe((data: any) => {
                    if (data) {
                        this.keys = data;
                        this.isLoad = true;
                        this.onSelect(this.keys[0]);
                        let keyId: number;
                        if (this.keys.length !== 0) {
                            keyId = this.keys[0].id;
                            this.router.navigate([this.currentPath, keyId]);
                        }
                        else {
                            this.isLoad = true;
                        }
                    }
                });

            this.currentPage++;
        });
    }

    onAdvanceSearchClick() { }

    ngDoCheck() {
        if (
            this.project && 
            this.keys &&
            this.router.url == `/workspace/${this.project.id}` &&
            this.keys.length !== 0
        ) {
            this.router.navigate([this.currentPath, this.keys[0].id]);
        }
    }

    onAddNewStringClick() {
        let dialogRef = this.dialog.open(StringDialogComponent, {
            data: {
                projectId: this.project.id
            }
        });
        dialogRef.componentInstance.onAddString.subscribe(result => {
            if (result) {
                this.keys.push(result);
                this.selectedKey = result;
                let keyId = this.keys[0].id;
                this.router.navigate([this.currentPath, keyId]);
                this.isEmpty = false;
            }
        });
        dialogRef.afterClosed().subscribe(() => {
            dialogRef.componentInstance.onAddString.unsubscribe();
        });
    }

    onSelect(key: any) {
        this.selectedKey = key;
    }

    ngOnDestroy() {
        this.routeSub.unsubscribe();
        this.signalrService.closeConnection(
            `${SignalrGroups[SignalrGroups.project]}${this.project.id}`);
    }

    getProjById(id: number) {
        this.dataProvider.getById(id).subscribe(proj => {
            this.project = proj;
        });
    }

    removeKeyById($event) {
        let temp = this.keys.findIndex(x => x.id === $event);
        if (this.selectedKey.id === this.keys[temp].id)
            this.selectedKey = this.keys[temp - 1]
                ? this.keys[temp - 1]
                : this.keys[temp + 1];

        this.keys.splice(temp, 1);

        if (this.keys.length > 0) {
            this.router.navigate([this.currentPath, this.selectedKey.id]);
        } else {
            this.router.navigate([this.basicPath]);
        }
    }

    subscribeProjectChanges() {
        this.signalrService.connection.on(
            SignalrSubscribeActions[SignalrSubscribeActions.complexStringAdded],
            (newStringId: number) => {
                this.complexStringService
                    .getById(newStringId)
                    .subscribe(newStr => {
                        if (newStr) {
                            this.keys.push(newStr);
                        }
                    });
            }
        );
        this.signalrService.connection.on(
            SignalrSubscribeActions[
            SignalrSubscribeActions.complexStringRemoved
            ],
            (deletedStringId: number) => {
                this.removeKeyById(deletedStringId);
            }
        );
    }

    OnSelectOption() {
        //If the filters сontradict each other
        this.ContradictoryСhoise(["Translated", "Untranslated"]);
        this.ContradictoryСhoise(["Human Translation", "Machine Translation"]);

        this.dataProvider
            .getProjectStringsByFilter(this.project.id, this.options.value)
            .subscribe(res => {
                this.keys = res;
            });
        console.log(this.options.value);
    }

    public onScrollUp(): void {
        this.getKeys(this.currentPage, keys => {
            this.keys = keys.concat(this.keys);
        });
    }

    public onScrollDown(): void {
        this.getKeys(this.currentPage, keys => {
            this.keys = this.keys.concat(keys);
        });
    }

    getKeys(page: number = 1, saveResultsCallback: (keys) => void) {
        return this.dataProvider
            .getProjectStringsWithPagination(
                this.project.id,
                this.elementsOnPage,
                this.currentPage
            )
            .subscribe((keys: any) => {
                this.currentPage++;
                saveResultsCallback(keys);
            });
    }

    ContradictoryСhoise(options: string[]) {
        if (
            this.options.value.includes(options[0]) &&
            this.options.value.includes(options[1])
        ) {
            options.forEach(element => {
                let index = this.options.value.indexOf(element);
                this.options.value.splice(index, 1);
            });
        }
    }
}
