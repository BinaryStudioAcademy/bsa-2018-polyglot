import { Component, OnInit, Input } from '@angular/core';
import { IString } from '../../models/string';
import { Tag } from '../../models/tag';
import {MAT_DIALOG_DATA} from '@angular/material';
import { Inject } from '@angular/core';
import { ComplexStringService } from '../../services/complex-string.service';

@Component({
  selector: 'app-string-dialog',
  templateUrl: './string-dialog.component.html',
  styleUrls: ['./string-dialog.component.sass']
})

export class StringDialogComponent implements OnInit {

  public str: IString;
  public image: File;

  public projectId: number;

  receiveImage($event){
    this.image = $event;
  }

  receiveTags($event) {
    this.str.tags = [];
    let tags: Tag[] = $event;
    for (let i = 0; i < tags.length; i++) {
      this.str.tags.push(tags[i].name);
    }
  }

  getAllTags(): Tag[] {
    let tags: Tag[] = [
      { name: 'FirstTag', color: '', id: 0, projectTags: [] },
      { name: 'SecondTag', color: '', id: 0, projectTags: [] },
      { name: 'ThirdTag', color: '', id: 0, projectTags: [] }
    ];
    return tags;
  }

    constructor(@Inject(MAT_DIALOG_DATA) public data: any, private complexStringService: ComplexStringService) { }


  ngOnInit() {
    this.str = {
      id: 7,
      key: '',
      base: '',
      projectId: 0,
      description: '',
      tags: [],
      projectId: this.data.projectId
    };
    this.image = undefined;
    console.log(this.str);
  }

  onSubmit(str: IString): void {
    this.complexStringService.create(this.str)
      .subscribe(
        (d) => {
          console.log(d);
        },
        err => {
          console.log('err', err);
        });
  }
}


