import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ComplexStringService } from '../../../services/complex-string.service';
import { MatDialog } from '@angular/material';
import { ImgDialogComponent } from '../../../dialogs/img-dialog/img-dialog.component';
import { ConfirmDialogComponent } from '../../../dialogs/confirm-dialog/confirm-dialog.component';
import {SnotifyService, SnotifyPosition, SnotifyToastConfig} from 'ng-snotify';

@Component({
  selector: 'app-workspace-key',
  templateUrl: './key.component.html',
  styleUrls: ['./key.component.sass']
})
export class KeyComponent implements OnInit {

  @Input() public key: any;
  @Input() public connection: any;
  @Output() idEvent = new EventEmitter<number>();
  description: string = "Are you sure you want to remove the string?";
  btnOkText: string = "Delete";
  btnCancelText: string = "Cancel";
  answer: boolean;

  constructor(private dataProvider: ComplexStringService,
              public dialog: MatDialog,
              private snotifyService: SnotifyService) {
                //
                //let a = this.connection;
               }

  ngOnInit() {
  }

  onDeleteString() {
    //
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '500px',
      data: {description: this.description, btnOkText: this.btnOkText, btnCancelText: this.btnCancelText, answer: this.answer}
    });
    dialogRef.afterClosed().subscribe(result => {
      if (dialogRef.componentInstance.data.answer){
        this.dataProvider.delete(this.key.id)
        .subscribe(
          (response => {
            this.snotifyService.success("String deleted", "Success!");
            //
            //if(this.connection)
            //  this.connection.send("complexStringDeleted", this.key.projectId, this.key.id);
          }),
          err => {
            this.snotifyService.error("String wasn`t deleted", "Error!");
          });
          this.idEvent.emit(this.key.id);
        }
      }
    );
  }

  onPictureIconClick(key: any){
    let dialogRef = this.dialog.open(ImgDialogComponent, {
      data: {
        imageUri: key.pictureLink
      }
    });
  }
}
