import { Component, OnInit, Input, SimpleChanges, ElementRef, ViewChild } from '@angular/core';
import { UserService } from '../../../../services/user.service';
import { ComplexStringService } from '../../../../services/complex-string.service';
import { AppStateService } from '../../../../services/app-state.service';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { SnotifyService } from 'ng-snotify';
import { IString } from '../../../../models/string';
import { ImgDialogComponent } from '../../../../dialogs/img-dialog/img-dialog.component';
import { MatDialog } from '@angular/material';
import { CommentsService } from '../../../../services/comments.service';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-tab-comments',
  templateUrl: './tab-comments.component.html',
  styleUrls: ['./tab-comments.component.sass']
})
export class TabCommentsComponent implements OnInit {

  comments: Comment[];
  routeSub: Subscription;
  keyId: number;

  commentForm = this.fb.group({
    commentBody: ['', Validators.required]
    });

    @ViewChild('textarea') textarea: ElementRef;

  constructor(private userService: UserService,
              private fb: FormBuilder,
              private commentService: CommentsService,
              private dialog: MatDialog,
              private snotifyService: SnotifyService,
              private activatedRoute: ActivatedRoute) { }


  ngOnInit() {
    this.routeSub = this.activatedRoute.params.subscribe((params) => {
      
      this.keyId = params.keyId;
      this.getComments().subscribe(comments=>{
        this.comments = comments;
      });
    });
  }

  getComments(){
    return this.commentService.getCommentsByStringId(this.keyId)
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

  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }

  addComment(commentBody: string){
    this.comments.unshift({user: this.userService.getCurrrentUser(),
                                   text: commentBody,   
                                   createdOn: new Date(Date.now())});

    this.commentService.updateStringComments(this.comments, this.keyId)
      .subscribe(
        (comments) => {
            if(comments){
              this.snotifyService.success("Comment added", "Success!");
              this.comments = comments;
              this.commentForm.reset();
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
