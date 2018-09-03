import { Component, OnInit, Output } from '@angular/core';
import { IString } from '../../models/string';
import { Tag } from '../../models/tag';
import { MAT_DIALOG_DATA } from '@angular/material';
import { Inject } from '@angular/core';
import { ComplexStringService } from '../../services/complex-string.service';
import { MatDialogRef } from '@angular/material';
import { SnotifyService, SnotifyPosition, SnotifyToastConfig } from 'ng-snotify';
import { EventEmitter } from '@angular/core';
import { AngularFireAuth } from 'angularfire2/auth';
import { TagService } from '../../services/tag.service';

@Component({
  selector: 'app-string-dialog',
  templateUrl: './string-dialog.component.html',
  styleUrls: ['./string-dialog.component.sass']
})

export class StringDialogComponent implements OnInit {

  @Output() onAddString = new EventEmitter<IString>(true);
  public str: IString;
  public image: File;
  public Tags;

  public projectId: number;

  receiveImage($event) {
    this.image = $event[0];
  }

  receiveTags($event) {
    this.str.tags = [];
    let tags: Tag[] = $event;
    for (let i = 0; i < tags.length; i++) {
      this.str.tags.push(tags[i]);
    }
    console.log($event);
  }

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private complexStringService: ComplexStringService,
    public dialogRef: MatDialogRef<StringDialogComponent>,
    private tagService : TagService,
    private snotifyService: SnotifyService) { }


  ngOnInit() {
    this.str = {
      id: 0,
      key: '',
      base: '',
      description: '',
      tags: [],
      projectId: this.data.projectId,
      translations: [],
      comments: [],
      createdBy: 0,
      createdOn: new Date()
    };
    this.image = undefined;
  }

  onSubmit(){
    this.tagService.addTagsToProject(this.str.tags,this.data.projectId).subscribe(res => {
    this.str.tags = res;
    let formData = new FormData();
    if(this.image)
      formData.append("image", this.image);
    formData.append("str", JSON.stringify(this.str));
    this.complexStringService.create(formData)
      .subscribe(
        (d) => {
          if(d)
          {
            this.onAddString.emit(d);
            this.snotifyService.success("ComplexString created", "Success!");
            this.dialogRef.close();
          }
          else
          {
            this.snotifyService.success("ComplexString wasn`t created", "Error!");
            this.dialogRef.close();
          }

        },
        err => {
          console.log('err', err);
          this.snotifyService.success("ComplexString wasn`t created", "Error!");
          this.dialogRef.close();
        });
      });
  }
}


