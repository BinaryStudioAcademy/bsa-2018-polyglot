import { Component, OnInit, Input, OnDestroy, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatTableDataSource, MatPaginator } from '@angular/material';
import { ProjectService } from '../../../services/project.service';
import { IString } from '../../../models/string';

@Component({
  selector: 'app-workspace-key-details',
  templateUrl: './key-details.component.html',
  styleUrls: ['./key-details.component.sass']
})
export class KeyDetailsComponent implements OnInit, OnDestroy {

  @Input()  public keyDetails: any; 
  public translationsDataSource: MatTableDataSource<any>; 
  public IsEdit : boolean = false;
  public IsPagenationNeeded: boolean = true;
  public pageSize: number  = 5;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    private activatedRoute: ActivatedRoute,
    private dataProvider: ProjectService
  ) { }

  ngOnChanges(){


    if(this.keyDetails && this.keyDetails.translations){
      debugger;
      this.IsPagenationNeeded = this.keyDetails.translations.length > this.pageSize;
      this.translationsDataSource = new MatTableDataSource(this.keyDetails.translations);
      
      if(this.IsPagenationNeeded){
        this.paginator.pageSize = this.pageSize;
        this.translationsDataSource.paginator = this.paginator;
      }

    }
    else
      this.IsPagenationNeeded = false;
  }

  ngOnInit() {
    
  }

  updateTable() {
    this.translationsDataSource.data = this.keyDetails.translations;
  }

  ngOnDestroy() {
  }

  toggle(){
    this.IsEdit = !this.IsEdit;
  }

}