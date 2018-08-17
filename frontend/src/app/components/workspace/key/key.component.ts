import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ComplexStringService } from '../../../services/complex-string.service';
import { MatDialog } from '@angular/material';
import { ImgDialogComponent } from '../../../dialogs/img-dialog/img-dialog.component';
import { ActivatedRoute } from '../../../../../node_modules/@angular/router';
import { from } from 'rxjs';

@Component({
  selector: 'app-workspace-key',
  templateUrl: './key.component.html',
  styleUrls: ['./key.component.sass']
})
export class KeyComponent implements OnInit {

  @Input() public key: any;
  @Output() idEvent = new EventEmitter<number>();


  constructor(
    private dataProvider: ComplexStringService,
    public dialog: MatDialog
  ) { }

  ngOnInit() {
  }

  onDeleteString() {
    this.idEvent.emit(this.key.id);
    this.dataProvider.delete(this.key.id)
    .subscribe(() => {});
  }

  onPictureIconClick(key: any){
    let dialogRef = this.dialog.open(ImgDialogComponent, {
      data: {
        imageUri: key.pictureLink
      }
      });
  }

}
