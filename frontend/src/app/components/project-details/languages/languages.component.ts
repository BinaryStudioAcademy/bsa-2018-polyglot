import { Component, OnInit, Input } from "@angular/core";
import { ProjectService } from "../../../services/project.service";
import { LanguageService } from "../../../services/language.service";
import {
    SnotifyService,
    SnotifyPosition,
    SnotifyToastConfig
} from "ng-snotify";
import { DeleteProjectLanguageComponent } from "../../../dialogs/delete-project-language/delete-project-language.component";
import { MatDialog } from "@angular/material";
import { SelectProjectLanguageComponent } from "../../../dialogs/select-project-language/select-project-language.component";
import { LanguageStatistic } from "../../../models/languageStatistic";
import { Language } from "../../../models";
import * as signalR from "../../../../../node_modules/@aspnet/signalr";
import { environment } from "../../../../environments/environment";

@Component({
    selector: "app-languages",
    templateUrl: "./languages.component.html",
    styleUrls: ["./languages.component.sass"]
})
export class LanguagesComponent implements OnInit {
    @Input()
    projectId: number;
    mainLang: Language;
    public langs: LanguageStatistic[] = [];
    public IsLoad: boolean = true;
    public IsLangLoad: boolean = false;
    public IsLoading: any = {};

    constructor(
        private projectService: ProjectService,
        private langService: LanguageService,
        private snotifyService: SnotifyService,
        public dialog: MatDialog
    ) {}

    ngOnInit() {
        this.projectService
            .getProjectLanguagesStatistic(this.projectId)
            .subscribe(
                langs => {
                    this.IsLoad = false;
                    this.langs = langs;
                    this.langs.sort(this.compareProgress);
                },
                err => {
                    this.IsLoad = false;
                }
            );
        this.projectService.getById(this.projectId).subscribe((proj)=>{ 
            this.mainLang = proj.mainLanguage
        });
    }

    ngOnDestroy() {}

    selectNew() {
        this.IsLangLoad = true;
        const thisLangs = this.langs;

        this.langService.getAll().subscribe(langs => {
            let langsToSelect = langs.filter(function(language) {
                let l = thisLangs.find(t => t.id === language.id);
                if (l) return (language.id !== l.id && language.id != this.mainLang.id);
                return true;
            });

            langsToSelect = langsToSelect.filter(lang => {
                return lang.id !== this.mainLang.id

            });

            this.IsLangLoad = false;
            if (langsToSelect.length < 1) {
                this.snotifyService.info(
                    "No languages available to select, all of them already added",
                    "Sorry!"
                );
                return;
            }
            let dialogRef = this.dialog.open(SelectProjectLanguageComponent, {
                data: {
                    langs: langsToSelect
                }
            });

            dialogRef.componentInstance.onSelect.subscribe(data => {
                if (data) {
                    this.IsLoad = true;
                    this.projectService
                        .addLanguagesToProject(
                            this.projectId,
                            data.map(l => l.id)
                        )
                        .subscribe(
                            project => {
                                if (project) {
                                    let stringsCount;
                                    if (this.langs.length > 0) {
                                        stringsCount = this.langs[0]
                                            .complexStringsCount;
                                    } else {
                                    }

                                    Array.prototype.push.apply(
                                        this.langs,
                                        data
                                            .map(function(language: Language) {
                                                return {
                                                    id: language.id,
                                                    name: language.name,
                                                    code: language.code,
                                                    translatedStringsCount: 0,
                                                    complexStringsCount: stringsCount,
                                                    progress: 0
                                                };
                                            })
                                            .filter(function(language) {
                                                let l = thisLangs.find(
                                                    t => t.id === language.id
                                                );
                                                if (l)
                                                    return language.id !== l.id;
                                                return true;
                                            })
                                    );
                                    this.langs.sort(this.compareProgress);
                                    this.IsLoad = false;
                                } else {
                                    this.snotifyService.error(
                                        "An error occurred while adding languages to project, please try again",
                                        "Error!"
                                    );
                                }
                            },
                            err => {
                                this.snotifyService.error(
                                    "An error occurred while adding languages to project, please try again",
                                    "Error!"
                                );
                                console.log("err", err);
                            }
                        );
                } else {
                    this.snotifyService.error(
                        "An error occurred while adding languages to project, please try again",
                        "Error!"
                    );
                }
            });

            dialogRef.afterClosed().subscribe(() => {
                dialogRef.componentInstance.onSelect.unsubscribe();
            });
        });
    }

    updateStringsCount(projectLanguageId: number) {
        this.projectService
            .getProjectLanguageStatistic(this.projectId, projectLanguageId)
            .subscribe(language => {
                if (language) {
                    for (let i = 0; i < this.langs.length; i++)
                        this.langs[i].complexStringsCount =
                            language.complexStringsCount;
                }
            });
    }

    onDeleteLanguage(languageId: number) {
        if (
            this.langs.filter(l => l.id === languageId)[0]
                .translatedStringsCount > 0
        ) {
            const dialogRef = this.dialog.open(DeleteProjectLanguageComponent, {
                data: {
                    languageName: this.langs.filter(l => l.id === languageId)[0]
                        .name,
                    translationsCount: this.langs.filter(
                        l => l.id === languageId
                    )[0].translatedStringsCount
                }
            });

            dialogRef.componentInstance.onConfirmDelete.subscribe(data => {
                if (data && data.state) {
                    this.snotifyService.info(
                        data.message,
                        "Deletion confirmed."
                    );
                    this.deleteLanguage(languageId);
                } else {
                    this.snotifyService.error(data.message, "Error!");
                }
            });

            dialogRef.afterClosed().subscribe(() => {
                dialogRef.componentInstance.onConfirmDelete.unsubscribe();
            });
        } else {
            this.deleteLanguage(languageId);
        }
    }

    deleteLanguage(languageId: number) {
        debugger;
        this.IsLoading[languageId] = true;
        this.projectService
            .deleteProjectLanguage(this.projectId, languageId)
            .subscribe(
                () => {
                    this.IsLoading[languageId] = false;

                    this.langs = this.langs.filter(l => l.id != languageId);
                    this.langs.sort(this.compareProgress);
                    setTimeout(() => {
                        this.snotifyService.success(
                            "Language removed",
                            "Success!"
                        );
                    }, 100);
                },
                err => {
                    this.IsLoading[languageId] = false;
                    this.snotifyService.error(
                        "Language wasn`t removed",
                        "Error!"
                    );
                }
            );
    }

    compareProgress(a: LanguageStatistic, b: LanguageStatistic) {
        if (a.progress < b.progress) return -1;
        if (a.progress > b.progress) return 1;
        return 0;
    }
}

// subscribeProjectChanges(){

//     this.connection.send("joinProjectGroup", `${this.projectId}`)

//     this.connection.on("languageAdded", (languagesIds: Array<number>) =>
//       {
//         console.log(languagesIds);
//         this.snotifyService.info(languagesIds.join(", ") + " added" , "Language added")
//         this.IsLoad = true;
// // ==============================================================================
// // ==============> Загрузить с сервера только те языки которые были добавленны
//         this.projectService.getProjectLanguagesStatistic(this.projectId)
//         .subscribe(langs => {
//           this.IsLoad = false;
//           this.langs = langs;
//           this.langs.sort(this.compareProgress);
//         },
//         err => {
//           this.IsLoad = false;
//         });
//       });
//       this.connection.on("languageDeleted", (languageId: number) =>
//       {
//         this.langs = this.langs.filter(l => l.id != languageId);
//         this.snotifyService.info(`lang with id =${languageId} removed`  , "Language removed")
//       });

//       this.connection.on("stringTranslated", (complexStringId: number, languageId: number) =>
//       {
//         this.projectService.getProjectLanguageStatistic(this.projectId, languageId)
//           .subscribe(lang => {
//             let targetId = this.langs.findIndex(l => l.id === languageId);
//             this.langs[targetId] = lang;
//           });
//       });
//   }
