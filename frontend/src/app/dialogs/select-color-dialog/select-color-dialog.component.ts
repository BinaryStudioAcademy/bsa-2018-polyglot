import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '../../../../node_modules/@angular/material';
import { Observable } from '../../../../node_modules/rxjs';

@Component({
  selector: 'app-select-color-dialog',
  templateUrl: './select-color-dialog.component.html',
  styleUrls: ['./select-color-dialog.component.sass']
})
export class SelectColorDialogComponent implements OnInit {

  selectedColor: string;
  selectedIndex : number;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
  public dialogRef: MatDialogRef<SelectColorDialogComponent>) { }

  ngOnInit() {
   this.selectedColor = "blue";
  }

  onAccept(){
    this.dialogRef.close(this.selectColor );
  }

  selectColor(index : any){
    this.selectedIndex = index;
    this.data = this.data;
    this.selectColor = this.data[index];
  }

}
