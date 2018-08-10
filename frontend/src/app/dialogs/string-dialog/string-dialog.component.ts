import { Component, OnInit, Input } from '@angular/core';
import { IString } from '../../models/string';
import { UploadImageComponent } from '../../components/upload-image/upload-image.component';
import { Tag } from '../../models/tag';
import { ComplexStringService } from '../../services/complex-string.service';

@Component({
  selector: 'app-string-dialog',
  templateUrl: './string-dialog.component.html',
  styleUrls: ['./string-dialog.component.sass']
})

export class StringDialogComponent implements OnInit {

  public str: IString;
  public image: File;

  constructor(private complexStringService: ComplexStringService) { }

  receiveImage($event) {
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

  ngOnInit() {
    this.str = {
      id: 7,
      key: '',
      base: '',
      projectId: 0,
      description: '',
      tags: [],
    };
    this.image = undefined;
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


