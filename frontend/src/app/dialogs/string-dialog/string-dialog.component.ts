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
  public projectTags: string[];
  public image: File;
  public tags: Array<String>;

  receiveImage($event){
    this.image = $event;
  }

  constructor() { }


  ngOnInit() {
    this.str = {
      key: '',
      base: '',
      description: '',
      tags: [''],
    };

    // get tag list from server
    this.projectTags = ['sometag1', 'tag2', 'sometag3', 'tag4', 'sometag5'];
  }

  onSubmit(str: IString) {

    // get only checked items from projectTags in str.tags

    console.log(str);
    // POST method
  }
}
