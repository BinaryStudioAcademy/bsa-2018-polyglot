import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { GlossaryService } from '../../services/glossary.service';
import { SnotifyService } from 'ng-snotify';
import { Glossary } from '../../models';
import { MatDialog } from '@angular/material';
import { GlossaryCreateDialogComponent } from '../../dialogs/glossary-create-dialog/glossary-create-dialog.component';
import { GlossaryEditDialogComponent } from '../../dialogs/glossary-edit-dialog/glossary-edit-dialog.component';

@Component({
  selector: 'app-glossaries',
  templateUrl: './glossaries.component.html',
  styleUrls: ['./glossaries.component.sass']
})
export class GlossariesComponent implements OnInit {
  displayedColumns: string[] = ['name','originLanguage', 'glossary_link','edit_btn', 'remove_btn'];
  dataSource : MatTableDataSource<Glossary>;
  glossaries : Glossary[];

  
  constructor(private glossaryService: GlossaryService,
    public dialog: MatDialog,
    private snotifyService: SnotifyService) { }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  ngOnInit() {
    this.glossaryService.getAll().subscribe((data : Glossary[]) =>{
      this.glossaries = data;
      this.dataSource = new MatTableDataSource(this.glossaries);
    })
  }

  ngOnChanges(){
    if(this.glossaries){
      this.dataSource = new MatTableDataSource(this.glossaries); 
    }
  }

  onCreate(){
    this.dialog.open(GlossaryCreateDialogComponent).afterClosed().subscribe(() =>{
      this.glossaryService.getAll().subscribe((data : Glossary[]) =>{
        this.glossaries = data;
      })
    });

  }

  onEdit(Item : Glossary){
    this.dialog.open(GlossaryEditDialogComponent, {
      data: Item,
    }).afterClosed().subscribe(() =>{
      this.glossaryService.getAll().subscribe((data : Glossary[]) =>{
        this.glossaries = data;
      })
    });

  }


}
