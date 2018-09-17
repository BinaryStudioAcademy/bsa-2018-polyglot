import { Component, OnInit, Inject, Output, EventEmitter } from '@angular/core';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { Language, generateItems, applyDrag } from '../../models';
import { Proficiency } from '../../models/proficiency';
import { TeamService } from '../../services/teams.service';
import { SnotifyService } from 'ng-snotify';
import { SelectProjectLanguageComponent } from '../select-project-language/select-project-language.component';
import { Translator } from '../../models/Translator';
import { LanguageService } from '../../services/language.service';
import { IDropResult } from 'ngx-smooth-dnd';

@Component({
  selector: 'app-team-add-member',
  templateUrl: './team-add-member.component.html',
  styleUrls: ['./team-add-member.component.sass']
})
export class TeamAddMemberComponent implements OnInit {
  @Output() onAssign = new EventEmitter<Array<number>>(true);
  translators: Array<any> = [];
  public selectedTeams: Array<any> = [];
  disabled: boolean = true;
  selected = 'Elementary';
  searchQuery: string;
  languages: Language[];
  proficiencyTypes = Proficiency;
  selectedLanguages: Language[];
  allTranslators: Translator[] = [];
  teamTranslators: Translator[] = [];
  translatorsRefresh: Translator[] = [];
  IsLoad: boolean = true;
  items = generateItems(50, i => ({ data: 'Draggable ' + i }))

  onDrop(dropResult: IDropResult) {
    // update item list according to the @dropResult
    this.items = applyDrag(this.items, dropResult);
  }

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<TeamAddMemberComponent>,
    public router: Router,
    private userService: UserService,
    private teamService: TeamService,
    private snotifyService: SnotifyService,
    private dialog: MatDialog,
    private languageService: LanguageService,
  ) {

    if (data && data.translators) {
      this.translators = data.translators;
      this.translators.sort(this.compareId);
    }
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
          this.translatorsRefresh = this.allTranslators;
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
    this.selectedTeams = teams.selectedOptions.selected.map(item => item.value);
    this.disabled = !this.selectedTeams.length;
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    // this.dataSource.filter = filterValue;
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

  assign() {

    if (this.selectedTeams.length > 0) {
      this.onAssign.emit(this.selectedTeams);
    }
    this.dialogRef.close();
  }

  redirectById(id: number) {
    this.dialogRef.close();
    if (this.userService.getCurrentUser().id == id) {
      this.router.navigate(['/profile']);
    }
    else {
      this.router.navigate(['/user', id]);
    }
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
    if (!proficiency) {
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
    this.allTranslators = this.translatorsRefresh;
  }

  getAvatarUrl(person): String {
    if (person.avatarUrl)
      return person.avatarUrl;
    else
      return "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTTsrMId-b7-CLWIw6S80BQZ6Xqd7jX0rmU9S7VSv_ngPOU7NO-6Q";
  }

  compareId(a, b) {
    if (a.id < b.id)
      return -1;
    if (a.id > b.id)
      return 1;
    return 0;
  }


}
