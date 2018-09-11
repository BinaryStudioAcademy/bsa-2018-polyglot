import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { GlossaryService } from '../../services/glossary.service';
import { SnotifyService } from 'ng-snotify';
import { Glossary } from '../../models';
import { MatDialog } from '@angular/material';
import { GlossaryCreateDialogComponent } from '../../dialogs/glossary-create-dialog/glossary-create-dialog.component';
import { GlossaryEditDialogComponent } from '../../dialogs/glossary-edit-dialog/glossary-edit-dialog.component';
import { ConfirmDialogComponent } from '../../dialogs/confirm-dialog/confirm-dialog.component';

@Component({
    selector: 'app-glossaries',
    templateUrl: './glossaries.component.html',
    styleUrls: ['./glossaries.component.sass']
})
export class GlossariesComponent implements OnInit {
    displayedColumns: string[] = ['name', 'originLanguage', 'glossary_link', 'edit_btn', 'remove_btn'];
    dataSource: MatTableDataSource<Glossary>;
    glossaries: Glossary[];
    desc: string = "Are you sure you want to remove the glossary?";
    btnOkText: string = "Delete";
    btnCancelText: string = "Cancel";
    answer: boolean;


    constructor(private glossaryService: GlossaryService,
        public dialog: MatDialog,
        private snotifyService: SnotifyService) { }

    applyFilter(filterValue: string) {
        this.dataSource.filter = filterValue.trim().toLowerCase();
    }

    ngOnInit() {
        this.glossaryService.getAll().subscribe((data: Glossary[]) => {
            this.glossaries = data;
            this.dataSource = new MatTableDataSource(this.glossaries);
        })
    }

    ngOnChanges() {
        if (this.glossaries) {
            this.dataSource = new MatTableDataSource(this.glossaries);
        }
    }

    onCreate() {
        this.dialog.open(GlossaryCreateDialogComponent)
        .afterClosed()
        .subscribe(() => {
            this.glossaryService.getAll().subscribe((data: Glossary[]) => {
                this.glossaries = data;
            });
        });

    }

    onEdit(Item: Glossary) {
        this.dialog.open(GlossaryEditDialogComponent, {
            data: Item,
        }).afterClosed().subscribe(() => {
            this.glossaryService.getAll().subscribe((data: Glossary[]) => {
                this.glossaries = data;
            });
        });
    }

    onDelete(id) {

        const dialogRef = this.dialog.open(ConfirmDialogComponent, {
            width: '500px',
            data: {description: this.desc, btnOkText: this.btnOkText, btnCancelText: this.btnCancelText, answer: this.answer}
          });

        dialogRef.afterClosed().subscribe(result => {
            if(dialogRef.componentInstance.data.answer) {
                this.glossaryService.delete(id).subscribe(
                    (d) => {
                        this.snotifyService.success("Glossary deleted", "Success!");
                        this.glossaryService.getAll().subscribe((data: Glossary[]) => {
                            this.glossaries = data;
                        });
                    },
                    err => {
                        console.log('err', err);
                        this.snotifyService.error("Glossary wasn`t deleted", "Error!");
                    });
            }
        });
    }


}
