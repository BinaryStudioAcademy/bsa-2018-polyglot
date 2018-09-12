import { Component, OnInit, OnDestroy, DoCheck, KeyValueDiffers, AfterViewInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Subscription, forkJoin } from "rxjs";
import { Project, UserProfile, Language } from "../../models";
import { ProjectService } from "../../services/project.service";
import { MatDialog } from "@angular/material";
import { StringDialogComponent } from "../../dialogs/string-dialog/string-dialog.component";
import { SnotifyService } from "ng-snotify";
import { AppStateService } from "../../services/app-state.service";
import { UserService } from "../../services/user.service";
import { ComplexStringService } from "../../services/complex-string.service";
import { SignalrGroups } from "../../models/signalrModels/signalr-groups";
import { SignalrService } from "../../services/signalr.service";
import { SignalrSubscribeActions } from "../../models/signalrModels/signalr-subscribe-actions";
import { EventService } from "../../services/event.service";
import { Hub } from "../../models/signalrModels/hub";

@Component({
    selector: "app-workspace",
    templateUrl: "./workspace.component.html",
    styleUrls: ["./workspace.component.sass"]
})
export class WorkspaceComponent implements OnInit, OnDestroy, DoCheck {
    public project: Project;
    public keys: any[] = [];
    public searchQuery: string = ' ';
    public currentSearchQuery: string = '';
    public selectedKey: any;
    public isEmpty;
    public currentPath;
    public basicPath;
    user: UserProfile;
    private currentPage = 0;
    private elementsOnPage = 7;
    public isLoad: boolean;
    public projectLanguagesCount: number;
    public stringsInProgress: number[] = [];
    public projectTags: string[] = [];
    private routeSub: Subscription;
    private loop: any;
    private currentKeyId: number;
    private previousKeyId: number;
    private projectId: number;
    private div;
    private differ;
    private madiv;
    private signalRConnection;
    isEditing: boolean;

    filters: Array<string>;

    filterOptions: string[] = [
        "Translated",
        "Untranslated",
        "With Photo"
    ];

    constructor(
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private dialog: MatDialog,
        private projectService: ProjectService,
        private snotifyService: SnotifyService,
        private appState: AppStateService,
        private userService: UserService,
        private complexStringService: ComplexStringService,
        private signalrService: SignalrService,
        private eventService: EventService,
        private differs: KeyValueDiffers
    ) {
        this.user = userService.getCurrentUser();
        this.eventService.listen().subscribe(
            (result) => {
                if (result.isEditing !== undefined) {
                    this.isEditing = result.isEditing
                } else if (result.status && !this.stringsInProgress.includes(result.keyId)) {
                    this.sendStringStatusMessage(result.keyId);
                } else {
                    this.complexStringService.changeStringStatus(result.keyId, `${SignalrGroups[SignalrGroups.project]}${this.project.id}`, result.status).subscribe(() => {});
                }            
            }
        );
        this.differ = differs.find({}).create();
    }

    description: string = "Are you sure you want to remove the project?";
    btnOkText: string = "Delete";
    btnCancelText: string = "Cancel";
    answer: boolean;

    ngOnInit() {
        console.log(this.stringsInProgress);
        this.filters = [];
        // this.searchQuery = "";
        this.routeSub = this.activatedRoute.params.subscribe(params => {
            //making api call using service service.get(params.projectId); ..
            forkJoin(
                this.projectService.getById(params.projectId),
                this.projectService.getProjectLanguages(params.projectId),
                this.projectId = params.projectId
            ).subscribe(result => {
                this.project = result[0];
                this.projectTags = this.project.tags.map(x => x.name);
                this.projectService
                    .getProjectLanguages(this.project.id)
                    .subscribe(
                        (d: Language[]) => {
                            this.projectLanguagesCount = d.length;
                            const workspaceState = {
                                projectId: this.project.id,
                                languages: d
                            };

                            this.appState.setWorkspaceState = workspaceState;
                            this.signalRConnection = this.signalrService.connect(
                                `${SignalrGroups[SignalrGroups.project]}${
                                    this.project.id
                                }`,
                                Hub.workspaceHub
                            );
                            this.subscribeProjectChanges();

                        },
                        err => {
                            this.keys = null;
                            this.isLoad = false;
                            console.log("err", err);
                        }
                    );
            });
            this.basicPath = "workspace/" + params.projectId;
            this.currentPath = "workspace/" + params.projectId + "/key";
            this.projectService
                .getProjectStringsWithPagination(
                    params.projectId,
                    this.elementsOnPage,
                    0,
                    this.searchQuery.trim()
                )
                .subscribe((data: any) => {
                    if (data) {
                        this.keys = data;
                        this.isLoad = true;
                        this.onSelect(this.keys[0]);
                        let keyId: number;
                        if (this.keys.length !== 0) {
                            keyId = this.keys[0].id;
                            this.currentKeyId = keyId;
                            this.router.navigate([this.currentPath, keyId]);
                        } else {
                            this.isLoad = true;
                        }
                    }
                });

            this.currentPage++;
        });
    }

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

    sendStringStatusMessage(keyId: number) {
        this.previousKeyId = this.currentKeyId;
        this.complexStringService.changeStringStatus(keyId, `${SignalrGroups[SignalrGroups.project]}${this.project.id}`, true).subscribe(() => {});
        this.loop = setInterval(() => {
            if (this.previousKeyId !== this.currentKeyId || !this.isEditing) {
                this.complexStringService.changeStringStatus(this.previousKeyId, `${SignalrGroups[SignalrGroups.project]}${this.project.id}`, false).subscribe(() => {});
                clearInterval(this.loop);
            } else {
                this.complexStringService.changeStringStatus(keyId, `${SignalrGroups[SignalrGroups.project]}${this.project.id}`, true).subscribe(() => {});
            }   
        }, 10000);
    }

    onAddNewStringClick() {
        let dialogRef = this.dialog.open(StringDialogComponent, {
            data: {
                projectId: this.project.id,
                tags : this.project.tags,
                string: {
                    id: 0,
                    key: '',
                    base: '',
                    description: '',
                    tags: [],
                    projectId: this.project.id,
                    translations: [],
                    comments: [],
                    createdBy: 0,
                    createdOn: new Date()
                }
            }
        });
        dialogRef.componentInstance.onAddString.subscribe(result => {
            if (result) {
                this.keys.push(result);
                result.tags.map(x => x.name)
                .forEach(element => {
                    if(!this.projectTags.includes(element)){
                        this.projectTags.push(element);
                    }
                })
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
        this.currentKeyId = this.selectedKey.id;
    }

    ngOnDestroy() {
        this.routeSub.unsubscribe();
        this.signalrService.leaveGroup(
            `${SignalrGroups[SignalrGroups.project]}${this.project.id}`,
            Hub.workspaceHub
        );
    }

    getProjById(id: number) {
        this.projectService.getById(id).subscribe(proj => {
            this.project = proj;
        });
    }

    removeComplexString(complexStringId: number) {
        let temp = this.keys.findIndex(x => x.id === complexStringId);

        if (this.selectedKey.id === this.keys[temp].id)
            this.selectedKey = this.keys[temp - 1]
                ? this.keys[temp - 1]
                : this.keys[temp + 1];

        this.keys.splice(temp, 1);

        this.getKeys(this.currentPage, keys => {
            this.keys = this.keys.concat(keys);
        });

        if (this.keys.length > 0) {
            this.router.navigate([this.currentPath, this.selectedKey.id]);
        } else {
            this.router.navigate([this.basicPath]);
        }
    }

    editComplexString(complexStringId: number){
        this.getKeys(this.currentPage, keys => {
            this.keys = this.keys.concat(keys);
            this.router.navigate([this.basicPath]);
        });

        if (this.keys.length > 0) {
            this.router.navigate([this.currentPath, this.selectedKey.id]);
        }
    }

    subscribeProjectChanges() {
        this.signalRConnection.on(
            SignalrSubscribeActions[SignalrSubscribeActions.complexStringAdded],
            (response: any) => {
                if (this.signalrService.validateResponse(response)) {
                    this.complexStringService
                        .getById(response.ids[0])
                        .subscribe(newStr => {
                            if (newStr) {
                                this.keys.push(newStr);
                            }
                        });
                }
            }
        );
        this.signalRConnection.on(
            SignalrSubscribeActions[
                SignalrSubscribeActions.complexStringRemoved
            ],
            (response: any) => {
                if (this.signalrService.validateResponse(response)) {
                    this.removeComplexString(response.ids[0]);
                }
            }
        );
        this.signalRConnection.on(
            SignalrSubscribeActions[
                SignalrSubscribeActions.complexStringTranslatingStarted
            ],
            (response: any) => {
                console.log(response.ids[0]);
                if (!this.stringsInProgress.includes(response.ids[0])) {
                    this.stringsInProgress.push(response.ids[0]);
                    setTimeout(() => {
                        if (this.stringsInProgress.includes(response.ids[0])) {
                            this.stringsInProgress.splice(this.stringsInProgress.indexOf(response.ids[0]), 1);
                        }
                    }, 9700);
                }            
            }
        );
        this.signalRConnection.on(
            SignalrSubscribeActions[
                SignalrSubscribeActions.complexStringTranslatingFinished
            ],
            (response: any) => {
                console.log(response);
                if (this.stringsInProgress.includes(response.ids[0])) {
                    this.stringsInProgress.splice(this.stringsInProgress.indexOf(response.ids[0]), 1);
                }
            }
        );
        this.signalRConnection.on(
            SignalrSubscribeActions[
                SignalrSubscribeActions.languageTranslationCommitted
            ],
            (response: any) => {
                this.projectService.getProjectStrings(this.project.id).subscribe(
                    responseKeys => {
                        this.keys = responseKeys;
                    }
                );
            }
        );
    }

    onFilterApply() {
        //If the filters сontradict each other
        this.contradictoryСhoise(["filter/Translated", "filter/Untranslated"]);
        this.projectService
            .getProjectStringsByFilter(this.project.id, this.filters)
            .subscribe(res => {
                this.keys = res;
            });
    }

    contradictoryСhoise(options: string[]) {
        if (
            this.filters.includes(options[0]) &&
            this.filters.includes(options[1])
        ) {
            options.forEach(element => {
                this.filters = this.filters.filter(x => {
                    return x !== element;
                });
            });
        }
    }

    selectFilterOption($event, index) {
        if ($event.checked) {
            this.filters.push("filter/" + this.filterOptions[index]);
        } else {
            this.filters = this.filters.filter(x => {
                return x !== "filter/" + this.filterOptions[index];
            });
        }
    }

    selectTag($event, index) {
        if ($event.checked) {
            this.filters.push("tags/" + this.projectTags[index]);
        } else {
            this.filters = this.filters.filter(x => {
                return x !== "tags/" + this.projectTags[index];
            });
        }
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
        return this.projectService
            .getProjectStringsWithPagination(
                this.project.id,
                this.elementsOnPage,
                this.currentPage,
                this.currentSearchQuery.trim()
            )
            .subscribe((keys: any) => {
                this.currentPage++;
                saveResultsCallback(keys);
            });
    }

    /*     test(id){
    var checkbox = document.getElementById("mat-checkbox-"+id);
    console.log(checkbox.classList.contains("mat-checkbox-checked"));
    } */

    highlightStringStatus(key) {
        if (this.stringsInProgress.includes(key.id)) {
            return '';
        } else if (key.translations.length === 0) {
            return "7px solid #a91818"; // not started
        } else if (key.translations.length < this.projectLanguagesCount) {
            return "7px solid #ffcc00"; // partially
        } else if (key.translations.length === this.projectLanguagesCount) {
            return "7px solid #00b300"; // completed
        }
    }

    isStringInProgress(key) {
        return this.stringsInProgress.includes(key.id);
    }

    searchChanges() {
        
        this.currentSearchQuery = this.searchQuery;
        this.currentPage = 0;
        this.projectService
                .getProjectStringsWithPagination(
                    this.projectId,
                    this.elementsOnPage,
                    this.currentPage,
                    this.currentSearchQuery.trim()
                )
                .subscribe((data: any) => {
                    if (data) {
                        this.keys = data;
                        this.isLoad = true;
                        this.onSelect(this.keys[0]);
                        let keyId: number;
                        if (this.keys.length !== 0) {
                            keyId = this.keys[0].id;
                            this.currentKeyId = keyId;
                            this.router.navigate([this.currentPath, keyId]);
                        } else {
                            this.isLoad = true;
                        }
                    }
                    let list = this.keys.filter(x => x.tags.length > 0);
                    this.projectTags = [].concat.apply(
                        [],
                        list.map(x => x.tags)
                    );
                    this.projectTags = Array.from(new Set(this.projectTags));
                });
        this.currentPage++;
    }
}
