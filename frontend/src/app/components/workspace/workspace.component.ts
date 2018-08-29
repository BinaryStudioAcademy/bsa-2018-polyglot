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
import * as signalR from "@aspnet/signalr";
import { environment } from "../../../environments/environment";
import { UserService } from "../../services/user.service";
import { ComplexStringService } from "../../services/complex-string.service";
import { SignalrGroups } from "../../models/signalrModels/signalr-groups";
import { SignalrService } from "../../services/signalr.service";
import { SignalrSubscribeActions } from "../../models/signalrModels/signalr-subscribe-actions";
import { IString } from "../../models/string";

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

    public projectTags: string[] = [];

    private routeSub: Subscription;

    filters : Array<string>

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
        this.filters = [];
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
                    this.keys.forEach(element => {
                        if (element.tags.length > 0) {
                            element.tags.forEach(tag => {
                                this.projectTags.push(tag);
                            });
                        }
                    });
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
        if (this.project) {
            this.signalrService.closeConnection(
                `${SignalrGroups[SignalrGroups.project]}${this.project.id}`
            );
        }
    }

    getProjById(id: number) {
        this.dataProvider.getById(id).subscribe(proj => {
            this.project = proj;
        });
    }

    receiveId($event) {
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
                debugger;
                this.receiveId(deletedStringId);
            }
        );
    }

    OnFilterApply() {
        //If the filters сontradict each other
        this.ContradictoryСhoise(["filter/Translated", "filter/Untranslated"]);
        this.ContradictoryСhoise(["filter/Human Translation", "filter/Machine Translation"]); 
        console.log(this.filters);
         
        this.dataProvider
            .getProjectStringsByFilter(this.project.id, this.filters)
            .subscribe(res => {
                this.keys = res;
            });
    }

     ContradictoryСhoise(options: string[]) {
        if (
            this.filters.includes(options[0]) &&
            this.filters.includes(options[1])
        ) {
            options.forEach(element => {
                this.filters = this.filters.filter( x => { return x !== element})
            });
        }
    } 

    selectFilterOption($event,index){
        if($event.checked){
            this.filters.push("filter/"+this.filterOptions[index])
        }
        else{
            this.filters = this.filters.filter( x => { return x !== "filter/" + this.filterOptions[index];});
        }
    }

    selectTag($event,index){
        if($event.checked){
            this.filters.push("tags/" + this.projectTags[index])
        }
        else{
            this.filters = this.filters.filter( x => { return x !== "tags/" + this.projectTags[index];});
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

/*     test(id){
    var checkbox = document.getElementById("mat-checkbox-"+id);
    console.log(checkbox.classList.contains("mat-checkbox-checked"));
    } */


    // subscribeProjectChanges() {

    //     this.connection.send("joinProjectGroup", `${this.project.id}`);

    //     this.connection.on("stringDeleted", (deletedStringId: number) => {
    //         if (deletedStringId) {
    //             debugger;
    //             this.snotifyService.info(
    //                 `Key ${deletedStringId} deleted`,
    //                 "String deleted"
    //             );
    //             this.receiveId(deletedStringId);
    //         }
    //     });

    //     this.connection.on("stringAdded", (newStringId: number) => {
    //         if (!this.keys.find(s => s.id == newStringId)) {
    //             if (!this.keys.find(s => s.id == newStringId)) {
    //                 this.complexStringService
    //                     .getById(newStringId)
    //                     .subscribe(newStr => {
    //                         if (newStr) {
    //                             this.snotifyService.info(
    //                                 `New key added`,
    //                                 "String added"
    //                             );
    //                             this.keys.push(newStr);
    //                         }
    //                     });
    //             }
    //         }
    //     });

    //     this.connection.on(
    //         "stringTranslated",
    //         (complexStringId: number, languageId: number) => {
    //             // получить строку с сервера, вывести уведомление
    //             this.snotifyService.info("String translated", "Translated");
    //         }
    //     );

    //     this.connection.on("languageAdded", (languagesIds: Array<number>) => {
    //         // обновить строку
    //         console.log(languagesIds);
    //         this.snotifyService.info(languagesIds.join(", "), "Language added");
    //     });

    //     this.connection.on("languageDeleted", (languageId: number) => {
    //         // обновить строку
    //         this.snotifyService.info(
    //             `lang with id =${languageId} removed`,
    //             "Language removed"
    //         );
    //     });
    // }

    // connectSignalR(){
    //   this.connection.start().catch(err => console.log("ERROR " + err));
    // }
}
