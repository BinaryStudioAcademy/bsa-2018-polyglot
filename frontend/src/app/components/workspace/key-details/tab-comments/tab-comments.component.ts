import { Component, OnInit, Input, SimpleChanges, ElementRef, ViewChild } from '@angular/core';
import { UserService } from '../../../../services/user.service';
import { ComplexStringService } from '../../../../services/complex-string.service';
import { AppStateService } from '../../../../services/app-state.service';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { SnotifyService } from 'ng-snotify';
import { IString } from '../../../../models/string';
import { ImgDialogComponent } from '../../../../dialogs/img-dialog/img-dialog.component';
import { MatDialog } from '@angular/material';

@Component({
  selector: 'app-tab-comments',
  templateUrl: './tab-comments.component.html',
  styleUrls: ['./tab-comments.component.sass']
})
export class TabCommentsComponent implements OnInit {

  @Input()  public keyDetails: IString;
  commentForm = this.fb.group({
    commentBody: ['', ]
    });


  body: string;

    @ViewChild('textarea') textarea: ElementRef;

  constructor(private userService: UserService,
              private fb: FormBuilder,
              private complexStringService: ComplexStringService,
              private dialog: MatDialog,
              private snotifyService: SnotifyService) { }

  ngOnInit() {
  }


  ngOnChanges(changes: SimpleChanges) {
    this.commentForm.reset();
  }

  onImageClick(avatarUrl: string){
    if(avatarUrl){
    let dialogRef = this.dialog.open(ImgDialogComponent, {
      data: {
        imageUri: avatarUrl
      }
      });
    }
  }

  addComment(commentBody: string){
    this.keyDetails.comments.unshift({user: this.userService.getCurrrentUser(),
                                   text: commentBody,   
                                   createdOn: new Date(Date.now())});

    this.complexStringService.update(this.keyDetails, this.keyDetails.id)
      .subscribe(
        (data) => {
          this.keyDetails = data;
          this.commentForm.reset();
            if(data){
              this.snotifyService.success("Comment added", "Success!");
            }
            else{
              this.snotifyService.error("Comment wasn't add", "Error!");
            }
        },
        err => {
          this.snotifyService.error("Comment added", "Error!");
        });
  }
}
