import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { GlossaryService } from '../../../services/glossary.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Glossary } from '../../../models';
import { GlossaryString } from '../../../models/glossary-string';
import { MatTableDataSource, MatDialog } from '@angular/material';
import { SnotifyService } from 'ng-snotify';
import { GlossaryStringDialogComponent } from '../../../dialogs/glossary-string-dialog/glossary-string-dialog.component';
import { ConfirmDialogComponent } from '../../../dialogs/confirm-dialog/confirm-dialog.component';

@Component({
    selector: 'app-glossary',
    templateUrl: './glossary.component.html',
    styleUrls: ['./glossary.component.sass']
})
export class GlossaryComponent implements OnInit {

    @ViewChild('readOnlyTemplate') readOnlyTemplate: TemplateRef<any>;
    @ViewChild('editTemplate') editTemplate: TemplateRef<any>;

    private routeSub: Subscription;
    public glossary: Glossary;
    public strings: GlossaryString[] = [];
    public editingString: GlossaryString;
    public isNewRecord: boolean;
    displayedColumns: string[] = ['termText', 'explanationText', 'edit_btn', 'remove_btn'];
    dataSource: MatTableDataSource<GlossaryString>;
    desc: string = "Are you sure you want to remove the term?";
    btnOkText: string = "Delete";
    btnCancelText: string = "Cancel";
    answer: boolean;

    constructor(
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private dataProvider: GlossaryService,
        private glossaryService: GlossaryService,
        public dialog: MatDialog,
        private snotifyService: SnotifyService
    ) { }

    ngOnInit() {
        this.refreshData();
    }

    refreshData() {
        this.routeSub = this.activatedRoute.params.subscribe((params) => {
            this.dataProvider.getById(params.glossaryId).subscribe((data: Glossary) => {
                this.glossary = data;
                this.strings = this.glossary.glossaryStrings;
                this.dataSource = new MatTableDataSource(this.strings);
            });
        });
    }

    ngOnChanges() {
        if (this.glossary) {
            this.strings = this.glossary.glossaryStrings;
            this.dataSource = new MatTableDataSource(this.strings);
        }
    }

    onCreate() {
        this.dialog.open(GlossaryStringDialogComponent, {
            data: { string: new GlossaryString(), glossaryId: this.glossary.id }
        }).afterClosed().subscribe(() => {
            this.refreshData();
        });
    }

    onDelete(Item: GlossaryString): void{
        const dialogRef = this.dialog.open(ConfirmDialogComponent, {
          width: '500px',
          data: {description: this.desc, btnOkText: this.btnOkText, btnCancelText: this.btnCancelText, answer: this.answer}
        });
        dialogRef.afterClosed().subscribe(result => {
          if (dialogRef.componentInstance.data.answer){
            this.glossaryService.deleteString(this.glossary.id, Item.id).subscribe(
              (d) => {
                  if (d) {
                      this.snotifyService.success("Glossary deleted", "Success!");
                      this.refreshData();
                  }
                  else {
                      this.snotifyService.error("Glossary wasn`t deleted", "Error!");
                      this.refreshData();
                  }
    
              },
              err => {
                  console.log('err', err);
                  this.snotifyService.error("Glossary wasn`t deleted", "Error!");
                  this.refreshData();
              });
    
            }
          }
        );
      }

    onEdit(Item: GlossaryString) {
        this.dialog.open(GlossaryStringDialogComponent, {
            data: { string: Item, glossaryId: this.glossary.id }
        }).afterClosed().subscribe(() => {
            this.refreshData();
        });
    }

}
