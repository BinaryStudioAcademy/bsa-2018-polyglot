import { Component, OnInit, Input } from '@angular/core';
import { IString } from '../../models/string';
import { UploadImageComponent } from '../../components/upload-image/upload-image.component';

@Component({
  selector: 'app-string-dialog',
  templateUrl: './string-dialog.component.html',
  styleUrls: ['./string-dialog.component.sass']
})
export class StringDialogComponent implements OnInit {

  public str: IString;
  public image: File;

  receiveImage($event){
    this.image = $event;
  }

  receiveTags($event){
    this.str.tags = $event;
  }

  constructor() { }


  ngOnInit() {
    this.str = {
      key: '',
      base: '',
      description: '',
      tags: [''],
    };

  }

  onSubmit() {
    console.log(this.str);
    console.log(this.image);
    // POST method
  }
}
