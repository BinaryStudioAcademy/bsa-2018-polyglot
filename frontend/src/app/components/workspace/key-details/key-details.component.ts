import { Component, OnInit, ViewChild } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { MatTableDataSource, MatPaginator, MatDialog } from "@angular/material";
import { ComplexStringService } from "../../../services/complex-string.service";
import { Language, Translation } from "../../../models";
import { SnotifyService } from "ng-snotify";
import { SaveStringConfirmComponent } from "../../../dialogs/save-string-confirm/save-string-confirm.component";
import { TabHistoryComponent } from "./tab-history/tab-history.component";
import { TranslationType } from "../../../models/TranslationType";
import { AppStateService } from "../../../services/app-state.service";
import * as signalR from "../../../../../node_modules/@aspnet/signalr";
import { SignalrService } from "../../../services/signalr.service";
import { TranslationState } from "../../../models/translation-state";
import { TranslationService } from "../../../services/translation.service";
import { SignalrSubscribeActions } from "../../../models/signalrModels/signalr-subscribe-actions";
import { SignalrGroups } from "../../../models/signalrModels/signalr-groups";
import { ProjectService } from "../../../services/project.service";

@Component({
    selector: "app-workspace-key-details",
    templateUrl: "./key-details.component.html",
    styleUrls: ["./key-details.component.sass"]
})
export class KeyDetailsComponent implements OnInit {
    @ViewChild(MatPaginator)
    paginator: MatPaginator;
    @ViewChild(TabHistoryComponent)
    history: TabHistoryComponent;

    public keyDetails: any;
    public translationsDataSource: MatTableDataSource<any>;
    public IsEdit: boolean = false;
    public IsPagenationNeeded: boolean = true;
    public pageSize: number = 5;
    public Id: string;
    public isEmpty: boolean;
    projectId: number;
    languages: Language[];
    expandedArray: TranslationState[];
    isLoad: boolean;
    comments: Comment[];
    description: string = "Do you want to save changes?";
    btnYesText: string = "Yes";
    btnNoText: string = "No";
    btnCancelText: string = "Cancel";
    answer: number;
    keyId: number;
    isDisabled: boolean;
    dataIsLoaded: boolean = false;
    isMachineTranslation: boolean;
    public MachineTranslation: string;
    public previousTranslation: string;
    currentTranslation: string;

    constructor(
        private route: ActivatedRoute,
        private dataProvider: ComplexStringService,
        public dialog: MatDialog,
        private snotifyService: SnotifyService,
        private appState: AppStateService,
        private signalrService: SignalrService,
        private service: TranslationService,
        private projectService: ProjectService
    ) {}

    ngOnInit() {
        this.dataIsLoaded = true;
        this.isMachineTranslation = false;

        this.route.params.subscribe(value => {
            this.keyId = value.keyId;
            this.isLoad = false;
            this.dataProvider.getById(value.keyId).subscribe((data: any) => {
                this.isLoad = false;
                this.keyDetails = data;
                this.projectId = this.keyDetails.projectId;
                this.signalrService.createConnection(
                    `${SignalrGroups[SignalrGroups.complexString]}${
                        this.keyDetails.id
                    }`,
                    "workspaceHub"
                );
                this.subscribeProjectChanges();
                this.getLanguages();
            });
            this.dataProvider
                .getCommentsByStringId(this.keyId)
                .subscribe(comments => {
                    this.comments = comments;
                });
        });
    }

    ngOnDestroy() {
        this.signalrService.closeConnection(
            `${SignalrGroups[SignalrGroups.complexString]}${this.keyDetails.id}`
        );
    }

    ngOnChanges() {
        if (this.keyDetails && this.keyDetails.translations) {
            this.IsPagenationNeeded =
                this.keyDetails.translations.length > this.pageSize;
            this.translationsDataSource = new MatTableDataSource(
                this.keyDetails.translations
            );

            if (this.IsPagenationNeeded) {
                this.paginator.pageSize = this.pageSize;
                this.translationsDataSource.paginator = this.paginator;
            }
        } else this.IsPagenationNeeded = false;
    }

    subscribeProjectChanges() {
        this.signalrService.connection.on(
            SignalrSubscribeActions[SignalrSubscribeActions.changedTranslation],
            (translation: any) => {
                this.setNewValueTranslation(translation);
            }
        );
        this.signalrService.connection.on(
            SignalrSubscribeActions[SignalrSubscribeActions.commentAdded],
            (comments: any) => {
                this.comments = comments;
            }
        );
        this.signalrService.connection.on(
            SignalrSubscribeActions[SignalrSubscribeActions.languageRemoved],
            (languageId: number) => {
                if (
                    languageId &&
                    this.keyDetails &&
                    this.keyDetails.translations
                ) {
                    let langName: string;
                    const currentState = this.appState.getWorkspaceState;
                    const deletedLanguage = currentState.languages.filter(
                        l => l.id === languageId
                    );
                    if (!deletedLanguage || deletedLanguage.length < 1) {
                        return;
                    }

                    langName = deletedLanguage[0].name;
                    this.appState.setWorkspaceState = {
                        projectId: currentState.projectId,
                        languages: currentState.languages.filter(
                            l => l.id !== languageId
                        )
                    };
                    this.languages = this.appState.getWorkspaceState.languages;
                    this.keyDetails.translations = this.keyDetails.translations.filter(
                        t => t.languageId !== languageId
                    );
                    this.snotifyService.info(
                        `${langName} was deleted`,
                        "Language deleted"
                    );
                }
            }
        );
        this.signalrService.connection.on(
            SignalrSubscribeActions[SignalrSubscribeActions.languagesAdded],
            (languagesIds: number[]) => {
                this.isLoad = true;
                this.projectService
                    .getProjectLanguages(this.projectId)
                    .subscribe(
                        languages => {
                            const currentState = this.appState
                                .getWorkspaceState;
                            const currentLanguages = currentState.languages;
                            const newLanguages = languages.filter(function(
                                language
                            ) {
                                return (
                                    currentLanguages.filter(
                                        l => l.id === language.id
                                    ).length < 1 &&
                                    languagesIds.filter(l => l === language.id)
                                        .length > 0
                                );
                            });
                            currentState.languages = languages;
                            this.languages = languages;
                            this.appState.setWorkspaceState = currentState;
                            for (var i = 0; i < newLanguages.length; i++) {
                                this.expandedArray.push({
                                    isOpened: false,
                                    oldValue: ""
                                });
                            }
                            Array.prototype.push.apply(
                                this.keyDetails.translations,
                                newLanguages.map(element => {
                                    return {
                                        languageName: element.name,
                                        languageId: element.id,
                                        languageCode: element.code,
                                        ...this.getProp(element.id)
                                    };
                                })
                            );
                            this.isLoad = false;
                        },
                        err => {
                            this.snotifyService.error(
                                "Languages update failed",
                                "Error"
                            );
                            this.isLoad = false;
                        }
                    );
            }
        );
    }

    setStep(index: number) {
        if (index === undefined) {
            return;
        }
        this.expandedArray[index] = {
            isOpened: true,
            oldValue: this.keyDetails.translations[index].translationValue
        };
        for (let i = 0; i < this.expandedArray.length; i++) {
            if (i != index) {
                this.expandedArray[i].isOpened = false;
            }
        }
        this.currentTranslation = this.keyDetails.translations[
            index
        ].translationValue;

        this.history.showHistory(
            this.keyId,
            this.keyDetails.translations[index].id
        );
    }

    setNewValueTranslation(translation: any) {
        const lenght = this.keyDetails.translations.length;
        for (var i = 0; i < lenght; i++) {
            if (
                this.keyDetails.translations[i].languageId ===
                translation.languageId
            ) {
                this.keyDetails.translations[i] = {
                    languageName: this.keyDetails.translations[i].languageName,
                    languageId: this.keyDetails.translations[i].languageId,
                    ...translation
                };
            }
        }
    }

    getLanguages() {
        if (
            !this.appState.getWorkspaceState ||
            !this.appState.getWorkspaceState.languages
        )
            return;

        this.languages = this.appState.getWorkspaceState.languages;

        const temp = this.languages.length;
        this.expandedArray = new Array();
        for (var i = 0; i < temp; i++) {
            this.expandedArray.push({ isOpened: false, oldValue: "" });
        }
        this.isEmpty = false;
        this.keyDetails.translations = this.languages.map(element => {
            return {
                languageName: element.name,
                languageId: element.id,
                languageCode: element.code,
                ...this.getProp(element.id)
            };
        });
        this.isLoad = true;
    }

    getProp(id: number) {
        const searchedElement = this.keyDetails.translations.filter(
            el => el.languageId === id
        );
        return searchedElement.length > 0 ? searchedElement[0] : null;
    }

    onSave(index: number, t: any) {
        this.currentTranslation = "";

        // 'Save' button not work if nothing has been changed
        if (!t.translationValue || (this.expandedArray[index].oldValue === t.translationValue && !this.isMachineTranslation)) {
            this.expandedArray[index].isOpened = false;
            return;
        }

        if (this.isMachineTranslation) {
            t.Type = TranslationType.Machine;
            this.isMachineTranslation = false;
        } else {
            t.Type = TranslationType.Human;
        }

        if (t.id != "00000000-0000-0000-0000-000000000000" && t.id) {
            this.dataProvider
                .editStringTranslation(t, this.keyId)
                .subscribe(
                    (d: any[]) => {
                        //console.log(this.keyDetails.translations);
                        this.expandedArray[index] = {
                            isOpened: false,
                            oldValue: ""
                        };
                        this.history.showHistory(
                            this.keyId,
                            this.keyDetails.translations[index].id
                        );
                    },
                    err => {
                        this.snotifyService.error(err);
                    }
                );
        } else {
            t.createdOn = new Date();
            this.dataProvider
                .createStringTranslation(t, this.keyId)
                .subscribe(
                    (d: any) => {
                        this.expandedArray[index] = {
                            isOpened: false,
                            oldValue: ""
                        };
                        this.history.showHistory(
                            this.keyId,
                            this.keyDetails.translations[index].id
                        );
                    },
                    err => {
                        console.log("err", err);
                    }
                );
        }
    }
    onClose(index: number, translation: any) {
        if (this.expandedArray[index].oldValue === translation.translationValue && !this.isMachineTranslation) {
            this.expandedArray[index].isOpened = false;
            this.currentTranslation = "";
            return;
        }
        const dialogRef = this.dialog.open(SaveStringConfirmComponent, {
            width: "500px",
            data: {
                description: this.description,
                btnYesText: this.btnYesText,
                btnNoText: this.btnNoText,
                btnCancelText: this.btnCancelText,
                answer: this.answer
            }
        });
        dialogRef.afterClosed().subscribe(result => {
            if (dialogRef.componentInstance.data.answer === 1) {
                this.onSave(index, translation);
                this.isMachineTranslation = false;
            } else if (dialogRef.componentInstance.data.answer === 0) {
                this.keyDetails.translations[
                    index
                ].translationValue = this.expandedArray[index].oldValue;
                this.expandedArray[index] = { isOpened: false, oldValue: "" };
                if (this.isMachineTranslation) {
                    this.keyDetails.translations[
                        index
                    ].translationValue = this.previousTranslation;
                    this.isMachineTranslation = false;
                }
                this.currentTranslation = "";
            }
        });
    }

    onMachineTranslationMenuClick(item: any): void {
        this.service
            .getTransation({ q: this.keyDetails.base, target: item })
            .subscribe((res: any) => {
                this.MachineTranslation = res[0].translatedText;
            });
    }

    toggle() {
        this.IsEdit = !this.IsEdit;
    }

    selectTranslation($event) {
        this.previousTranslation = this.keyDetails.translations[
            $event.keyId
        ].translationValue;

        this.isMachineTranslation = true;

        this.keyDetails.translations[$event.keyId].translationValue =
            $event.translation;

        this.expandedArray[$event.keyId].isOpened = true;
    }

    toggleDisable() {
        this.isDisabled = !this.isDisabled;
    }

    highlightString(index: number) {
        if (this.expandedArray[index].isOpened) {
            return "2px ridge #6495ED";
        }
        return "";
    }
}
