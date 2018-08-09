import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatSort, MatPaginator, MatTableDataSource } from '@angular/material';
import { FormControl, FormGroupDirective, NgForm, Validators} from '@angular/forms';
import { MatTable } from '@angular/material';
import { ErrorStateMatcher} from '@angular/material/core';
import { Observable, of } from 'rxjs';
import { Sort} from '@angular/material';
import { Teammate } from '../../../models/teammate';
import { SearchService } from '../../../services/search.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router, ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
  styleUrls: ['./team.component.sass']
})
export class TeamComponent implements OnInit {

  @Input() id: number;
  teammates: Teammate[];
  emailToSearch: string;
  displayedColumns: string[] = ['status', 'fullName', 'email', 'rights', 'options' ];
  dataSource: MatTableDataSource<Teammate>;
  emailFormControl = new FormControl('', [
    Validators.email,
  ]);
  searchResultRecieved: boolean = false;
  ckb: boolean = false;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatTable) table: MatTable<any>;

  constructor(private searchService: SearchService, private activatedRoute: ActivatedRoute) {
    this.id = Number(this.activatedRoute.snapshot.paramMap.get('id'));
    this.getTranslators();
  }

  getTranslators(){
    this.teammates = [];
    this.searchService.GetTranslatorsByTeam(this.id)
        .subscribe((data: Teammate[]) => {
          this.teammates = data;
          this.dataSource = new MatTableDataSource(this.teammates);
        })
  }

  ngOnInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }
  ngAfterViewInit() {
    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
  }
  
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  searchTranslators($event) {
    let email: string = $event;
    console.log(email);
    this.getTranslators();
    this.searchService.FindTranslatorsByEmail(email)
        .subscribe((data: Teammate[]) => {
          this.teammates = data.concat(this.teammates);
          this.dataSource = new MatTableDataSource(this.teammates);
          this.dataSource.paginator = this.paginator;
          this.paginator.pageIndex = 0;
        });
  }

  checkTranslatorRight(id: number, rightName: string) : boolean{
    let teammate = this.teammates.find(t => t.id === id);
    if(!teammate)
      return false;
      
    
      if(teammate.rights == null){
        return false;
      }
      return teammate.rights
        .find(r => r.definition.trim().toLowerCase() === rightName.trim().toLowerCase())
        != null;
      
  }

  changeTranslatorRight(e, id){
    if(e.checked)
      {
        // add right
      }
    else
      {
        //remove right
      }
  }
}