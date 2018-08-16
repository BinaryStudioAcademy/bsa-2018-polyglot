import { Component, OnInit, Input, SimpleChanges, ElementRef, ViewChild } from '@angular/core';
import { UserService } from '../../../../services/user.service';
import { ComplexStringService } from '../../../../services/complex-string.service';
import { AppStateService } from '../../../../services/app-state.service';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { SnotifyService } from 'ng-snotify';
import { IString } from '../../../../models/string';

@Component({
  selector: 'app-tab-comments',
  templateUrl: './tab-comments.component.html',
  styleUrls: ['./tab-comments.component.sass']
})
export class TabCommentsComponent implements OnInit {

  @Input()  public keyDetails: IString;
  comments: any[] = new Array<any>();
  commentForm = this.fb.group({
    commentBody: ['', Validators.required]
    });

    @ViewChild('textarea') textarea: ElementRef;

  constructor(private userService: UserService,
              private fb: FormBuilder,
              private complexStringService: ComplexStringService,
              private snotifyService: SnotifyService) { }

  ngOnInit() {
  }


  ngOnChanges(changes: SimpleChanges) {
    this.commentForm.reset();
  }

  addComment(commentBody: string){
    this.keyDetails.comments.push({user: this.userService.getCurrrentUser(),
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
