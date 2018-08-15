import { Component, OnInit, Input } from '@angular/core';
import { IString } from '../../../../models/string';
import { MatDialog } from '@angular/material';
import { ImgDialogComponent } from '../../../../dialogs/img-dialog/img-dialog.component';

@Component({
  selector: 'app-key-detail',
  templateUrl: './key-detail.component.html',
  styleUrls: ['./key-detail.component.sass']
})
export class KeyDetailComponent implements OnInit {

  @Input()  public keyDetails: any;

  constructor(public dialog: MatDialog) { }

  ngOnInit() {
  }

  onImageClick(keyDetails){
    if(keyDetails.pictureLink){
    let dialogRef = this.dialog.open(ImgDialogComponent, {
      data: {
        imageUri: keyDetails.pictureLink
      }
      });
    }
  }

}
