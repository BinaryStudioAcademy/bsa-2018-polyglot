import { Component, OnInit, ViewChild, AfterViewInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { MatTableDataSource, MatPaginator, MatDialog } from "@angular/material";
import { ComplexStringService } from "../../../services/complex-string.service";
import { Language, Translation, Role } from "../../../models";
import { SnotifyService } from "ng-snotify";
import { SaveStringConfirmComponent } from "../../../dialogs/save-string-confirm/save-string-confirm.component";
import { TabHistoryComponent } from "./tab-history/tab-history.component";
import { AppStateService } from "../../../services/app-state.service";
import { SignalrService } from "../../../services/signalr.service";
import { TranslationState } from "../../../models/translation-state";
import { TranslationService } from "../../../services/translation.service";
import { SignalrSubscribeActions } from "../../../models/signalrModels/signalr-subscribe-actions";
import { SignalrGroups } from "../../../models/signalrModels/signalr-groups";
import { ProjectService } from "../../../services/project.service";
import { ProjecttranslatorsService } from "../../../services/projecttranslators.service";
import { UserProfilePrev } from "../../../models/user/user-profile-prev";
import { TabOptionalComponent } from "./tab-optional/tab-optional.component";
import { EventService } from "../../../services/event.service";
import { Comment } from "../../../models/comment";
import { UserService } from "../../../services/user.service";
import { Hub } from "../../../models/signalrModels/hub";

@Component({
    selector: "app-workspace-key-details",
    templateUrl: "./key-details.component.html",
    styleUrls: ["./key-details.component.sass"]
})
export class KeyDetailsComponent implements OnInit, AfterViewInit {
    @ViewChild(MatPaginator)
    paginator: MatPaginator;
    @ViewChild(TabHistoryComponent)
    history: TabHistoryComponent;
    hideHistory() { this.history.hideHistory(); }
    @ViewChild(TabOptionalComponent)
    optional: TabOptionalComponent;

    public keyDetails: any;
    public translationsDataSource: MatTableDataSource<any>;
    public IsEdit: boolean = false;
    public IsPagenationNeeded: boolean = true;
    public pageSize: number = 5;
    public index: number;

    private currentPage = 0;
    private elementsOnPage = 7;

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
    currentKeyId: number;
    isDisabled: boolean;
    dataIsLoaded: boolean = false;
    isMachineTranslation: boolean;
    public MachineTranslation: string;
    public previousTranslation: string;
    currentTranslation: string;
    currentSuggestion: string;
    isSaveDisabled: boolean;
    translationDivs: any;
    currentUserRole: any;
    translationInputs: any;
    private signalRConnection;
    glossaryWords: any[] = [];
    divHidden: boolean;

    users: UserProfilePrev[] = [];
    currentUserId: number;
    selectedIndex = 0;

    constructor(
        private eventService: EventService,
        private route: ActivatedRoute,
        private dataProvider: ComplexStringService,
        public dialog: MatDialog,
        private snotifyService: SnotifyService,
        private appState: AppStateService,
        private signalrService: SignalrService,
        private service: TranslationService,
        private projectService: ProjectService,
        private translatorsService: ProjecttranslatorsService,
        private userService: UserService) {
        eventService.listen().subscribe((data: any) => {
            if (data=="reload") {
                this.reloadKeyDetails(this.currentKeyId);
            }
        });
    }

    ngOnInit() {
        this.refresh();
    }

    refresh(){
        this.dataIsLoaded = true;
        this.isMachineTranslation = false;
        this.currentUserId = this.appState.currentDatabaseUser.id;
        this.currentUserRole = this.appState.currentDatabaseUser.userRole;
        this.route.params.subscribe(value => {
            this.isLoad = false;
            this.dataProvider.getById(value.keyId).subscribe((data: any) => {
                this.isLoad = false;
                this.keyDetails = data;
                this.projectId = this.keyDetails.projectId;
                this.translatorsService.getById(this.projectId).subscribe((data: UserProfilePrev[]) => {
                    this.users = data;
                });
                this.projectService.getAssignedGlossaries(this.projectId).subscribe(
                    (glos) => {
                        for (let i = 0; i < glos.length; i++) {
                            for (let j = 0; j < glos[i].glossaryStrings.length; j++) {
                                this.glossaryWords.push(glos[i].glossaryStrings[j]);
                            }
                        }
                    }
                );
                if (this.currentKeyId && this.currentKeyId !== data.id) {
                    this.signalrService.leaveGroup(
                        `${SignalrGroups[SignalrGroups.complexString]}${
                        this.currentKeyId
                        }`,
                        Hub.workspaceHub
                    );

                    this.currentKeyId = data.id;
                    this.signalRConnection = this.signalrService.connect(
                        `${SignalrGroups[SignalrGroups.complexString]}${
                        this.currentKeyId
                        }`,
                        Hub.workspaceHub
                    );
                } else {
                    this.currentKeyId = data.id;
                    this.signalRConnection = this.signalrService.connect(
                        `${SignalrGroups[SignalrGroups.complexString]}${
                        this.currentKeyId
                        }`,
                        Hub.workspaceHub
                    );
                    this.subscribeProjectChanges();
                }

                this.getLanguages();
                this.dataProvider
                    .getCommentsWithPagination(this.currentKeyId, this.elementsOnPage, this.currentPage)
                    .subscribe(comments => {
                        this.comments = comments;
                    });
            });
        });

    }

    ngOnDestroy() {
        this.signalrService.leaveGroup(
            `${SignalrGroups[SignalrGroups.complexString]}${this.keyDetails.id}`,
            Hub.workspaceHub
        );
    }

    ngAfterViewInit() {
        this.translationDivs = document.getElementsByClassName('translation-div');
        this.translationInputs = document.getElementsByClassName('translation-input');
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
        this.signalRConnection.on(
            SignalrSubscribeActions[SignalrSubscribeActions.changedTranslation],
            (response: any) => {
                if (this.signalrService.validateResponse(response)) {
                    this.dataProvider
                        .getStringTranslations(this.currentKeyId)
                        .subscribe(translations => {
                            if (translations && translations.length > 0) {
                                if (
                                    !this.keyDetails.translations ||
                                    this.keyDetails.translations < 1
                                ) {
                                    this.keyDetails.translations = translations;

                                } else {
                                    this.setNewTranslations(
                                        translations,
                                        response.senderFullName
                                    );
                                    this.history.showHistory(this.currentKeyId, this.keyDetails.translations[this.index].id);
                                    this.expandedArray[this.index].oldValue=translations[this.index].translationValue;
                                    if(this.expandedArray[this.index].isOpened===false)
                                    {
                                        this.history.translationSelected = false;
                                    }
                                }
                            }
                        });
                }
            }
        );
        this.signalRConnection.on(
            SignalrSubscribeActions[SignalrSubscribeActions.commentsChanged],
            (response: any) => {
                if (this.signalrService.validateResponse(response)) {
                    this.dataProvider
                        .getCommentsWithPagination(this.currentKeyId, this.elementsOnPage, this.currentPage)
                        .subscribe(comments => {
                            this.comments = comments;
                        });
                }
            }
        );

        this.signalRConnection.on(
            SignalrSubscribeActions[SignalrSubscribeActions.languageRemoved],
            (response: any) => {
                if (this.signalrService.validateResponse(response)) {
                    if (this.keyDetails && this.keyDetails.translations) {
                        const removedLanguageId = response.ids[0];
                        let langName: string;
                        const currentState = this.appState.getWorkspaceState;
                        const deletedLanguage = currentState.languages.filter(
                            l => l.id === removedLanguageId
                        );
                        if (!deletedLanguage || deletedLanguage.length < 1) {
                            return;
                        }

                        langName = deletedLanguage[0].name;
                        this.appState.setWorkspaceState = {
                            projectId: currentState.projectId,
                            languages: currentState.languages.filter(
                                l => l.id !== removedLanguageId
                            )
                        };
                        this.languages = this.appState.getWorkspaceState.languages;
                        this.keyDetails.translations = this.keyDetails.translations.filter(
                            t => t.languageId !== removedLanguageId
                        );
                        this.snotifyService.info(
                            `${langName} was deleted`,
                            "Language deleted"
                        );
                    }
                }
            }
        );



        this.signalRConnection.on(
            SignalrSubscribeActions[SignalrSubscribeActions.languagesAdded],
            (response: any) => {
                if (this.signalrService.validateResponse(response)) {
                    this.handleNewLanguagesAdded(response.ids);
                }
            }
        );
    }

    onTextChange(i) {
        let words =  this.translationInputs.item(i).value.split(' ');
        let result = '';
        for (let i = 0; i < words.length; i++) {
            for (let j = 0; j < this.glossaryWords.length; j++) {
                if (words[i].toLowerCase() === this.glossaryWords[j].termText.toLowerCase()) {
                    words[i] = '<div style="display: inline; background: #fffa6b; border-radius: 10%;" class="child">' + words[i] + '<span style="position: absolute; display: inline-block; visibility: hidden; color: #6600cc; z-index: 5; background-color: #cce6ff;">' + this.glossaryWords[j].explanationText + '</span></div>';
                }
            }
            if (words[i] === '') {
                words[i] = '<span style="padding-left: 0.28571427715em"></span>';
            }
            if (i !== words.length - 1) {
                words[i] = words[i] + ' ';
            }
            result += words[i];
        }

        this.translationDivs.item(i).innerHTML = result;

        let glosWords: any =  document.querySelectorAll('.child');
        for (let n = 0; n < glosWords.length; n++) {
            glosWords[n].addEventListener('mouseover', function(e) {
                let chil: any = glosWords[n].children[0];
                chil.style.top = `${glosWords[n].offsetTop - 17}px`;
                chil.style.left = `${glosWords[n].offsetLeft}px`;
                chil.style.visibility = 'visible';           
            });
            glosWords[n].addEventListener('mouseout', function(e) {
                let chil: any = glosWords[n].children[0];
                chil.style.visibility = 'hidden';
            });
        }
    }

    enterPress($event) {
        if ($event.which === 13) {
            $event.preventDefault();
        }
    }

    setPosition(i) {
        let position;
        switch (i) {
            case 0:
                position = '38px';
                break;
            case 1:
                position = '86px';
                break;
            case 2:
                position = '134px';
                break;
            case 3:
                position = '182px';
                break;
            case 4:
                position = '230px';
                break;
        }
        return position;
    }

    handleNewLanguagesAdded(languagesIds) {
        this.isLoad = true;
        this.projectService.getProjectLanguages(this.projectId).subscribe(
            languages => {
                const currentState = this.appState.getWorkspaceState;
                const currentLanguages = currentState.languages;
                const newLanguages = languages.filter(function (language) {
                    return (
                        currentLanguages.filter(l => l.id === language.id)
                            .length < 1 &&
                        languagesIds.filter(l => l === language.id).length > 0
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
                this.snotifyService.error("Languages update failed", "Error");
                this.isLoad = false;
            }
        );
    }

    setNewTranslations(translations, callerName) {
        let targetTranslationIndex = -1;
        let targetExpandedArrayItem;
        let newTranslationValue;

        for (let i = 0; i < translations.length; i++) {
            targetTranslationIndex = this.keyDetails.translations
                .map(function (t) {
                    return t.languageId;
                })
                .indexOf(translations[i].languageId);

            if (targetTranslationIndex < 0) {
                targetTranslationIndex = this.keyDetails.translations.length;
            }

            targetExpandedArrayItem = this.expandedArray[
                targetTranslationIndex
            ];

            if (
                targetExpandedArrayItem &&
                targetExpandedArrayItem.isOpened &&
                targetExpandedArrayItem.oldValue &&
                targetExpandedArrayItem.oldValue !== "" &&
                targetExpandedArrayItem.oldValue !== this.currentTranslation
            ) {
                newTranslationValue = `Your work  ==========>
                                            ${
                    this.currentTranslation
                    }
                                            <========= ${callerName}'s changes
                                            =========>
                                            ${
                    translations[i].translationValue
                    }`;
            } else {
                newTranslationValue = translations[i].translationValue;
            }

            this.keyDetails.translations[
                targetTranslationIndex
            ].translationValue = newTranslationValue;
        }
    }

    setStep(index: number) {
        this.eventService.filter({
            isEditing: true
        });
        this.divHidden = false;
        this.onTextChange(index);
        this.index = index;
        this.eventService.filter({
                keyId: this.currentKeyId,
                status: true
            });
        this.history.translationSelected=true;
        if (index === undefined) {
            return;
        }
        this.expandedArray[index] = {
            isOpened: true,
            oldValue: this.keyDetails.translations[index].translationValue ? this.keyDetails.translations[index].translationValue : ""
        };
        for (let i = 0; i < this.expandedArray.length; i++) {
            if (i != index) {
                this.expandedArray[i].isOpened = false;
            }
        }
        this.currentTranslation = this.keyDetails.translations[
            index
        ].translationValue;

        this.isCanSave(index, this.keyDetails.translations[index]);

        this.history.showHistory(
            this.currentKeyId,
            this.keyDetails.translations[index].id
        );
        this.optional.showOptional(
            this.currentKeyId,
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

    isCanSave(i, t) {
        if (!t.translationValue || (this.expandedArray[i].oldValue === t.translationValue && !this.isMachineTranslation)) {
            this.isSaveDisabled = true;
        } else {
            this.isSaveDisabled = false;
        }
    }

    onSave(index: number, t: any) {
        this.eventService.filter({
            isEditing: false
        });
        this.eventService.filter({
            keyId: this.currentKeyId,
            status: false
        });
        this.currentTranslation = "";
        this.index = index;
        // 'Save' button not work if nothing has been changed
        if (
            !t.translationValue ||
            (this.expandedArray[index].oldValue === t.translationValue &&
                !this.isMachineTranslation)
        ) {
            this.expandedArray[index].isOpened = false;
            return;
        }

        /*
        if (this.isMachineTranslation) {
            t.Type = TranslationType.Machine;
            this.isMachineTranslation = false;
        } else {
            t.Type = TranslationType.Human;
        }*/
        if (t.id != "00000000-0000-0000-0000-000000000000" && t.id) {
            this.dataProvider
                .editStringTranslation(t, this.currentKeyId)
                .subscribe(
                    (d: any[]) => {
                        this.divHidden = true;
                        //console.log(this.keyDetails.translations);
                        this.expandedArray[index] = {
                            isOpened: false,
                            oldValue: ""
                        };
                        this.hideHistory();
                        this.optional.showOptional(
                            this.currentKeyId,
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
                .createStringTranslation(t, this.currentKeyId)
                .subscribe(
                    (d: any) => {
                        this.divHidden = true;
                        this.expandedArray[index] = {
                            isOpened: false,
                            oldValue: ""
                        };
                        this.keyDetails.translations[index].history=d.history;
                        this.hideHistory();
                        this.optional.showOptional(
                            this.currentKeyId,
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
        this.eventService.filter({
            isEditing: false
        });
        this.eventService.filter({
            keyId: this.currentKeyId,
            status: false
        });

        if (!translation.translationValue || (this.expandedArray[index].oldValue === translation.translationValue && !this.isMachineTranslation)) {
            this.divHidden = true;
            this.expandedArray[index].isOpened = false;
            this.currentTranslation = "";
            this.hideHistory();
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
            }
            else if (dialogRef.componentInstance.data.answer === 0) {
                this.divHidden = true;
                this.keyDetails.translations[
                    index
                ].translationValue = this.expandedArray[index].oldValue;
                this.expandedArray[index] = { isOpened: false, oldValue: "" };
                this.hideHistory();
                if (this.isMachineTranslation) {
                    this.keyDetails.translations[
                        index
                    ].translationValue = this.previousTranslation;
                    this.isMachineTranslation = false;
                }
                this.currentTranslation = "";
            }
        }
        );
    }

    onMachineTranslationMenuClick(item: any): void {
        this.service
            .getTranslation({ q: this.keyDetails.base, target: item })
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
        
        this.translationInputs[$event.keyId].value = $event.translation;
        this.setStep($event.keyId);
    }

    toggleDisable(status: boolean) {
        this.isDisabled = status;
    }

    highlightString(index: number) {
        if (this.expandedArray[index].isOpened) {
            return "2px ridge #6495ED";
        }
        return "";
    }


    onAssign() {
    }

    chooseUser($event) {
        if (!$event.translationId) {
            for (var i = 0; i < this.keyDetails.translations.length; i++) {
                if (this.keyDetails.translations[i].languageId === $event.langId) {
                    if (!$event.user) {
                        $event.user = {};
                    }
                    this.keyDetails.translations[i].assignedTranslatorId = $event.user.id;
                    this.keyDetails.translations[i].assignedTranslatorName = $event.user.fullName;
                    this.keyDetails.translations[i].assignedTranslatorAvatarUrl = $event.user.avatarUrl;
                    this.dataProvider.createStringTranslation(this.keyDetails.translations[i], this.keyDetails.id)
                        .subscribe(
                            (d: any[]) => {
                            },
                            err => {
                                this.snotifyService.error('User wasn`t assigned!');
                            }
                        );
                    break;
                }
            }
        }
        else {
            for (var i = 0; i < this.keyDetails.translations.length; i++) {
                if (this.keyDetails.translations[i].id === $event.translationId) {
                    if (!$event.user) {
                        $event.user = {};
                    }
                    this.keyDetails.translations[i].assignedTranslatorId = $event.user.id;
                    this.keyDetails.translations[i].assignedTranslatorName = $event.user.fullName;
                    this.keyDetails.translations[i].assignedTranslatorAvatarUrl = $event.user.avatarUrl;
                    this.dataProvider.editStringTranslation(this.keyDetails.translations[i], this.keyDetails.id)
                        .subscribe(
                            (d: any) => {
                            },
                            err => {
                                this.snotifyService.error('User wasn`t assigned!');
                            }
                        );
                    break;
                }
            }
        }
    }

    suggestTranslation(index, TranslationId, Suggestion) {
        this.dataProvider
            .addOptionalTranslation(
                this.currentKeyId,
                TranslationId,
                Suggestion
            )
            .subscribe(
                res => {
                    this.snotifyService.success("Your suggestion was added");
                    this.optional.showOptional(
                        this.currentKeyId,
                        this.keyDetails.translations[index].id
                    );
                },
                err => {
                    this.snotifyService.error("Your suggestion wasn`t added");
                }
            );
        this.currentSuggestion = "";
    }
    
    public onConfirm(translation: Translation){
        this.dataProvider.confirmTranslation(translation, this.keyDetails.id).subscribe(
            res => {
                this.refresh();
                this.snotifyService.success("Confirmed!");
                this.toggleDisable(false);
            },
            err => {
                this.snotifyService.error("Error!");
                console.log(err);
            }
        )
    }

    public onUnConfirm(translation: Translation){
        this.dataProvider.unConfirmTranslation(translation, this.keyDetails.id).subscribe(
            res => {
                this.refresh();
                this.snotifyService.success("Unconfirmed!");
                this.toggleDisable(false);
            },
            err => {
                this.snotifyService.error("Error!");
                console.log(err);
            }
        )
    }

    public canBeConfirmed(translation: Translation){
        return translation.id && !translation.isConfirmed && this.userService.getCurrentUser().userRole === Role.Manager;
    }

    public canUnBeConfirmed(translation: Translation){
        return translation.id && translation.isConfirmed && this.userService.getCurrentUser().userRole === Role.Manager;
    }


    // public showAssignButton(userId: number): boolean {
    //     var result = false;
    //     if (this.userService.getCurrentUser().userRole === 1) {
    //         result = false;
    //     }
    //     // else if (this.userService.getCurrentUser().userRole === 0 && this.userService.getCurrentUser().id === userId) {
    //     //     result = false;
    //     // }
    //     // else if (!userId) {
    //     //     result = false;
    //     // }
    //     else {
    //        result = true;
    //     }
    //     return result || !this.users.length;
    // }


    reloadKeyDetails(index) {
        this.dataIsLoaded = true;
        this.route.params.subscribe(value => {
            this.isLoad = false;
            this.dataProvider.getById(value.keyId).subscribe((data: any) => {
                if (data) {
                    this.isLoad = false;
                    this.keyDetails = data;
                    this.isLoad = true;
                }
                this.getLanguages();
                this.history = this.keyDetails.translations[index].history
                this.history.showHistory(this.currentKeyId, this.keyDetails.translations[index].id)
            });
        });
    }

    getPosition(e) {
        var posx = 0;
        var posy = 0;

        if (!e) { let e = window.event; }

        if (e.pageX || e.pageY) {
            posx = e.pageX;
            posy = e.pageY;
        } else if (e.clientX || e.clientY) {
            posx = e.clientX + document.body.scrollLeft + document.documentElement.scrollLeft;
            posy = e.clientY + document.body.scrollTop + document.documentElement.scrollTop;
        }

        return {
            x: posx,
            y: posy
        }
    }

    isVisible = false;
    textCommentForAdd: string;

    onRightClick($event) {
        let length = document.getSelection().toString().length;
        if (length > 1) {
            this.isVisible = true;
            $event.preventDefault();
            let menu = document.getElementById("main-input");

            menu.style.position = "absolute";
            menu.style.visibility = "visible";

            menu.style.marginLeft = `${(this.getPosition($event).x - 350).toString()}px`;
            menu.style.marginTop = `${(this.getPosition($event).y - 180).toString()}px`;

            this.isVisible = true;
        }
        else {
            this.isVisible = false;
        }
        return false;
    }

    onClickOnTranslation($event) {
        this.isVisible = false;
    }

    addComment() {
        this.textCommentForAdd = document.getSelection().toString();
        //If we use span for background
        // var comment = document.getElementById("comment");
        // comment.innerHTML = comment.innerHTML + `<span
        //   style ="background: #fffa6b;
        //   border-radius: 10%;
        //   opacity: 0.8;"
        // >${this.text}</span>`;
        this.selectTab(2);
        this.isVisible = false;
    }

    selectTab(index: number): void {
        this.selectedIndex = index;
    }
}
