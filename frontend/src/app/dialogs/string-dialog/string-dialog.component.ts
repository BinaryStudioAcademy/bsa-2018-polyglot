import { Component, OnInit, Input } from '@angular/core';
import { IString } from '../../models/string';
import { UploadImageComponent } from '../../components/upload-image/upload-image.component';
import { Tag } from '../../models/tag';

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
    this.str.tags = [];
    let tags: Tag[] = $event;
    for(let i = 0;i < tags.length; i++){
      this.str.tags.push(tags[i].name);
    }
  }

  getAllTags(): Tag[]{
     let tags: Tag[] = [
      {name: 'FirstTag', color: '', id: 0, projectTags:[]},
      {name: 'SecondTag', color: '', id: 0, projectTags:[]},
      {name: 'ThirdTag', color: '', id: 0, projectTags:[]}
    ];
    return tags;
  }

  constructor() { }


  ngOnInit() {
    this.str = {
      key: '',
      base: '',
      description: '',
      tags: [],
    };
    this.image = undefined;
  }

  onSubmit() {
    alert("submited(change this to push)");
    console.log(this.str);
    console.log(this.image);
  }
}
