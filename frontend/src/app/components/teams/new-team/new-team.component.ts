import { Component, OnInit, Output, ViewChild, EventEmitter } from '@angular/core';
import { SnotifyService } from 'ng-snotify';
import { MatTableDataSource, MatSort, MatPaginator, MatDialog } from '@angular/material';
import { TeamService } from '../../../services/teams.service';
import { ContainerComponent, DraggableComponent, IDropResult } from 'ngx-smooth-dnd';
import { applyDrag, generateItems, Language } from '../../../models';
import { Translator } from '../../../models/Translator';
import { Router } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { NotificationService } from '../../../services/notification.service';
import { OptionDefinition } from '../../../models/optionDefinition';
import { Proficiency } from '../../../models/proficiency';
import { LanguageService } from '../../../services/language.service';
import { SelectProjectLanguageComponent } from '../../../dialogs/select-project-language/select-project-language.component';
import { TranslatorSearchByNamePipe } from '../../../pipes/translator-search-by-name.pipe';


@Component({
    selector: 'app-new-team',
    templateUrl: './new-team.component.html',
    styleUrls: ['./new-team.component.sass']
})
export class NewTeamComponent implements OnInit {
    IsLoad: boolean = true;
    allTranslators: Translator[] = [];
    teamTranslators: Translator[] = [];
    public defaultAvatar: String = "/assets/images/anonymus.jpg"
    public name: string;
    // displayedColumns = ['id', 'name', 'rating', 'language', 'action'];
    // dataSource: MatTableDataSource<any>;

    public selectedTranslators: Array<any> = [];
    disabled: boolean = true;
    selected = 'Beginner';

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;

    items = generateItems(50, i => ({ data: 'Draggable ' + i }))
    searchQuery: string;
    languages: Language[];
    proficiencyTypes = Proficiency;
    selectedLanguages: Language[];

    onDrop(dropResult: IDropResult) {
        // update item list according to the @dropResult
        this.items = applyDrag(this.items, dropResult);
    }

    constructor(
        private router: Router,
        private teamService: TeamService,
        private snotifyService: SnotifyService,
        private userService: UserService,
        private notificationService: NotificationService,
        private languageService: LanguageService,
        private dialog: MatDialog,
    ) {


    }

    ngOnInit() {
        this.getAllTranslators();
        this.languageService.getAll()
            .subscribe(
                (d: Language[]) => {
                    this.languages = d.map(x => Object.assign({}, x));
                },
                err => {
                    this.snotifyService.error("Languages wasn`t loaded", "Error!");
                });
    }

    getAllTranslators() {
        this.teamService.getAllTranslators()
            .subscribe((translators: Translator[]) => {

                this.IsLoad = false;
                if (translators && translators.length > 0) {
                    this.allTranslators = translators;
                }
                else {
                    this.allTranslators = [];
                    this.snotifyService.info("No translators found!", "Ooops!")
                }
            },
                err => {
                    this.allTranslators = [];
                    this.snotifyService.error("An error occurred while loading translators, please try again later!", "Error!")
                    this.IsLoad = false;
                });
    }


    change(e, teams) {
        this.selectedTranslators = teams.selectedOptions.selected.map(item => item.value);
        this.disabled = !this.selectedTranslators.length;
    }


    createTeam() {
        if (this.selectedTranslators && this.selectedTranslators.length > 0) {
            this.teamService.formTeam(this.selectedTranslators.map(t => t.userId), this.name)
                .subscribe((team) => {
                    if (team) {
                        this.router.navigate(['dashboard/teams']);
                        setTimeout(() => {
                            this.snotifyService.success("Team " + team.id + " successfully created!", "Success!");
                        }, 200);
                    }
                    else
                        this.snotifyService.error("An error occurred, team not created, please try again later!", "Error!")
                },
                    err => {
                        this.snotifyService.error("An error occurred, team not created, please try again later!", "Error!")
                    })
        }
    }

    applyFilter(filterValue: string) {
        filterValue = filterValue.trim(); // Remove whitespace
        filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
        // this.dataSource.filter = filterValue;
    }
    getAvatarUrl(person): String {

        if (person.avatarUrl !== " ")
            return person.avatarUrl;
        else
            return this.defaultAvatar;
    }
    nestedFilterCheck(search, data, key) {
        if (typeof data[key] === 'object') {
            for (const k in data[key]) {
                if (data[key][k] !== null) {
                    search = this.nestedFilterCheck(search, data[key], k);
                }
            }
        } else {
            search += data[key];
        }
        return search;
    }

    getStringProficiency(prof: Proficiency) {
        if (Proficiency[prof] === "UpperIntermediate") {
            return "Upper Intermediate"
        }
        return Proficiency[prof];
    }

    selectLanguages(): void {
        let dialogRef = this.dialog.open(SelectProjectLanguageComponent, {
            data: {
                langs: this.languages
            }
        });

        dialogRef.componentInstance.onSelect.subscribe(data => {
            if (data) {
                this.selectedLanguages = data.map(x => Object.assign({}, x));
            }
        });
    }

    profTypes(): Array<string> {
        var keys = Object.keys(this.proficiencyTypes);
        return keys.slice(keys.length / 2);
    }

    onFilterApply(proficiency: string) {
        if(!proficiency) {
            proficiency = "Beginner";
        }
        this.teamService.getFilteredTranslators(this.proficiencyTypes[proficiency], this.selectedLanguages)
            .subscribe(
                (d: Translator[]) => {
                    this.allTranslators = d.map(x => Object.assign({}, x));
                },
                err => {
                    this.snotifyService.error("Translators wasn`t loaded", "Error!");
                });
    }

    clearFilter() {
        this.selectedLanguages = [];
        this.selected = "Beginner";
    }
}








