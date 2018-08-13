import { Component, OnInit, Input } from '@angular/core';
import { IString } from '../../models/string';
import { Tag } from '../../models/tag';
import { MAT_DIALOG_DATA } from '@angular/material';
import { Inject } from '@angular/core';
import { ComplexStringService } from '../../services/complex-string.service';
import { MatDialogRef } from '@angular/material';
import { SnotifyService, SnotifyPosition, SnotifyToastConfig } from 'ng-snotify';

@Component({
  selector: 'app-string-dialog',
  templateUrl: './string-dialog.component.html',
  styleUrls: ['./string-dialog.component.sass']
})

export class StringDialogComponent implements OnInit {

  public str: IString;
  public image: File;

  public projectId: number;

  receiveImage($event) {
    this.image = $event[0];
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

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private complexStringService: ComplexStringService,
    public dialogRef: MatDialogRef<StringDialogComponent>,
    private snotifyService: SnotifyService) { }


  ngOnInit() {
    this.str = {
      id: 0,
      key: '',
      base: '',
      description: '',
      tags: [],
      projectId: this.data.projectId
    };
    this.image = undefined;
    console.log(this.str);
  }

  onSubmit(){
    this.complexStringService.create(this.str)
      .subscribe(
        (d) => {
          console.log(d);
          this.snotifyService.success("ComplexString created", "Success!");
          this.dialogRef.close();         
        },
        err => {
          console.log('err', err);
          this.snotifyService.success("ComplexString wasn`t created", "Error!");
          this.dialogRef.close();     
        });
  }
}


