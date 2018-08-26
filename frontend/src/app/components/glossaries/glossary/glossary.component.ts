import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { GlossaryService } from '../../../services/glossary.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Glossary } from '../../../models';
import { GlossaryString } from '../../../models/glossary-string';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-glossary',
  templateUrl: './glossary.component.html',
  styleUrls: ['./glossary.component.sass']
})
export class GlossaryComponent implements OnInit {

  @ViewChild('readOnlyTemplate') readOnlyTemplate: TemplateRef<any>;
  @ViewChild('editTemplate') editTemplate: TemplateRef<any>;

  private routeSub: Subscription;
  public glossary : Glossary;
  public strings : GlossaryString[] = [];
  public editingString : GlossaryString;
  public isNewRecord: boolean;
  displayedColumns: string[] = ['termText','explanationText', 'edit_btn', 'remove_btn'];
  dataSource : MatTableDataSource<GlossaryString>;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router : Router,
    private dataProvider: GlossaryService,
  ) { }

  ngOnInit() {
    this.routeSub = this.activatedRoute.params.subscribe((params) => {
      this.dataProvider.getById(params.glossaryId).subscribe((data : Glossary) => {
        this.glossary = data;
        this.strings = this.glossary.glossaryStrings;
        this.dataSource = new MatTableDataSource(this.strings);
      });
    });
  }

  ngOnChanges(){
    if(this.glossary){
      this.strings = this.glossary.glossaryStrings;
      this.dataSource = new MatTableDataSource(this.strings);
    }
  }

  onCreate(){

  }

  onDelete(Item : GlossaryString){

  }

  onEdit(Item : GlossaryString){

  }

}
