import { Component, OnInit, ViewChild } from '@angular/core';
import { SnotifyService } from 'ng-snotify';
import { MatTableDataSource, MatSort, MatPaginator  } from '@angular/material';
import { TeamService } from '../../../services/teams.service';


@Component({
  selector: 'app-new-team',
  templateUrl: './new-team.component.html',
  styleUrls: ['./new-team.component.sass']
})
export class NewTeamComponent implements OnInit {

  IsLoad: boolean = true;
  managerId: number = 1;
  translators: any;
  displayedColumns = ['id', 'name', 'rating', 'language', 'action'];
  dataSource: MatTableDataSource<any>;
  

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private teamService: TeamService,
    private snotifyService: SnotifyService 
  ) {
        
        
  }

  ngOnInit() {
    this.getAllTranslators();
  }

  getAllTranslators(){
     this.teamService.getAllTeams()
      .subscribe(translators => {
        this.translators = translators;
        debugger;
        this.dataSource = new MatTableDataSource(this.translators);
        this.dataSource.filterPredicate = (data, filter: string)  => {
          const accumulator = (currentTerm, key) => {
            return key === 'userProfile' ? currentTerm + data.userProfile.fullName : currentTerm + data[key];
          };
          const dataStr = Object.keys(data).reduce(accumulator, '').toLowerCase();
          // Transform the filter by converting it to lowercase and removing whitespace.
          const transformedFilter = filter.trim().toLowerCase();
          return dataStr.indexOf(transformedFilter) !== -1;
        };
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
        debugger;
        this.IsLoad = false;
      })
  }
  
  getRating(id:string){
    var rating = 0;
    for(let i = 0; i < this.translators[id].ratings.count(); i++) {
      rating += this.translators[id].rating[i].rate
    }
    return rating/this.translators[id].ratings.count();
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;


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








