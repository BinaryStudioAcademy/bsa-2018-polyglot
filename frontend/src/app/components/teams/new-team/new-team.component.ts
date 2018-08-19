import { Component, OnInit, ViewChild } from '@angular/core';
import { SnotifyService } from 'ng-snotify';
import { MatTableDataSource, MatSort, MatPaginator  } from '@angular/material';
import { TeamService } from '../../../services/teams.service';
import { ContainerComponent, DraggableComponent, IDropResult } from 'ngx-smooth-dnd';
import { applyDrag, generateItems } from '../../../models';
import { Translator } from '../../../models/Translator';


@Component({
  selector: 'app-new-team',
  templateUrl: './new-team.component.html',
  styleUrls: ['./new-team.component.sass']
})
export class NewTeamComponent implements OnInit {

  IsLoad: boolean = true;
  managerId: number = 1;
  allTranslators: Array<Translator> = [];
  teamTranslators: Array<Translator> = [];
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
    private teamService: TeamService,
    private snotifyService: SnotifyService 
  ) {
        
        
  }

  ngOnInit() {
    this.getAllTranslators();
  }

  getAllTranslators(){
    
  }

  addTranslator(translator: Translator){

  }

  removeTranslator(translator: Translator){
    
  }

  formTeam(){

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








