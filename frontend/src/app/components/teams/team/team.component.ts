import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatSort, MatPaginator, MatTableDataSource } from '@angular/material';
import { FormControl, FormGroupDirective, NgForm, Validators} from '@angular/forms';
import { MatTable } from '@angular/material';
import { Teammate } from '../../../models/teammate';
import { TeamService } from '../../../services/teams.service';
import { SearchService } from '../../../services/search.service';
import { ActivatedRoute } from '@angular/router';


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
  dataSource: MatTableDataSource<Teammate> = new MatTableDataSource();
  emailFormControl = new FormControl('', [
    Validators.email,
  ]);
  searchResultRecieved: boolean = false;
  ckb: boolean = false;
  public IsPagenationNeeded: boolean = true;
  public pageSize: number  = 5;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatTable) table: MatTable<any>;

  constructor(
    private teamService: TeamService, 
    private searchService: SearchService,
    private activatedRoute: ActivatedRoute) {
    this.id = Number(this.activatedRoute.snapshot.paramMap.get('id'));
    this.getTranslators();
    
  }

  getTranslators(){
    this.teammates = [];
    this.teamService.getAllTeammates(this.id)
        .subscribe((data: Teammate[]) => {
          this.teammates = data;
          this.dataSource = new MatTableDataSource(this.teammates);
          this.dataSource.sort = this.sort;
          this.ngOnChanges();
        })
  }

  ngOnInit() {
    
    this.dataSource.paginator = this.paginator;
   // this.checkPaginatorNecessity();
  }

  ngOnChanges(){
    
    this.dataSource.paginator = this.paginator;
  //  this.checkPaginatorNecessity();
  }

  ngAfterViewInit() {
    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
  }
  
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  searchTranslators() {
    this.getTranslators();
    this.searchService.FindTranslatorsByEmail(this.emailToSearch)
        .subscribe((data: Teammate[]) => {
          this.teammates = data.concat(this.teammates);
          this.dataSource = new MatTableDataSource(this.teammates);
          this.dataSource.paginator = this.paginator;
          this.paginator.pageIndex = 0;
        });
  }

  checkTranslatorRight(id: number, rightName: string) : boolean{
    if(!this.teammates)
      return false;

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

  checkPaginatorNecessity(){
    if(this.teammates){
      this.IsPagenationNeeded = this.teammates.length > this.pageSize;

      if(this.IsPagenationNeeded){
        this.paginator.pageSize = this.pageSize;
      }
    }
    else
      this.IsPagenationNeeded = false;
  }
}