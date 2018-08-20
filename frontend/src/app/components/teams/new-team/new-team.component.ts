import { Component, OnInit, Output, ViewChild, EventEmitter } from '@angular/core';
import { SnotifyService } from 'ng-snotify';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { TeamService } from '../../../services/teams.service';
import { ContainerComponent, DraggableComponent, IDropResult } from 'ngx-smooth-dnd';
import { applyDrag, generateItems } from '../../../models';
import { Translator } from '../../../models/Translator';
import { Router } from '@angular/router';


@Component({
  selector: 'app-new-team',
  templateUrl: './new-team.component.html',
  styleUrls: ['./new-team.component.sass']
})
export class NewTeamComponent implements OnInit {

  IsLoad: boolean = true;
  managerId: number = 1;
  allTranslators: Translator[] = [];
  teamTranslators: Translator[] = [];
  // displayedColumns = ['id', 'name', 'rating', 'language', 'action'];
  // dataSource: MatTableDataSource<any>;


  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  items = generateItems(50, i => ({ data: 'Draggable ' + i }))

  onDrop(dropResult: IDropResult) {
    // update item list according to the @dropResult
    this.items = applyDrag(this.items, dropResult);
  }

  constructor(
    private router: Router,
    private teamService: TeamService,
    private snotifyService: SnotifyService
  ) {


  }

  ngOnInit() {
    this.getAllTranslators();
  }

  getAllTranslators() {
    this.teamService.getAllTranslators()
      .subscribe((translators: Translator[]) => {
       
        this.IsLoad = false;
        if (translators && translators.length > 0)
          this.allTranslators = translators;
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

  addTranslator(translator: Translator) {
    
    this.teamTranslators.push(translator);
    this.allTranslators = this.allTranslators.filter(t => t.id != translator.id);
  }

  removeTranslator(translator: Translator) {
    
    this.allTranslators.push(translator);
    this.teamTranslators = this.teamTranslators.filter(t => t.id != translator.id);
  }

  formTeam() {
    if (this.teamTranslators && this.teamTranslators.length > 0) {
      this.teamService.formTeam(this.teamTranslators.map(t => t.id))
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
}








