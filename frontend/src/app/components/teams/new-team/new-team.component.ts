import { Component, OnInit, ViewChild } from '@angular/core';
import { SnotifyService } from '../../../../../node_modules/ng-snotify';
import { Translator } from '../../../models';
import { TranslatorService } from '../../../services/translator.service';
import { MatTableDataSource, MatSort, MatPaginator  } from '@angular/material';


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

  constructor(private translatorService: TranslatorService,
    private snotifyService: SnotifyService, ) {
      
    
        
  }

  ngOnInit() {
    this.getAllTranslators();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.dataSource.filterPredicate = (data, filter) => {
      let valid = false;

      const transformedFilter = filter.trim().toLowerCase();

      Object.keys(data).map(key => {
        if (
          key === 'details' &&
          (data.details.name.toLowerCase().includes(transformedFilter) ||
          ('' + data.details.weight).toLowerCase().includes(transformedFilter))
        ) {
          valid = true;
        } else {
          if (('' + data[key]).toLowerCase().includes(transformedFilter)) {
            valid = true;
          }
        }
      });

      return valid;
    }
  
  }

  getAllTranslators(){
     this.translatorService.getAll()
      .subscribe(translators => {
        this.translators = translators;
        this.dataSource = new MatTableDataSource(this.translators);
        debugger;
        this.IsLoad = false;
      })
  }

  
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }
}








