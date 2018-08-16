import { Component, OnInit, Input } from '@angular/core';
import { UserService } from '../../../../services/user.service';
import { ComplexStringService } from '../../../../services/complex-string.service';
import { AppStateService } from '../../../../services/app-state.service';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { SnotifyService } from 'ng-snotify';

@Component({
  selector: 'app-tab-comments',
  templateUrl: './tab-comments.component.html',
  styleUrls: ['./tab-comments.component.sass']
})
export class TabCommentsComponent implements OnInit {

  @Input()  public keyDetails: any;
  commentForm = this.fb.group({
    commentBody: ['', Validators.required]
    });

  constructor(private userService: UserService,
              private fb: FormBuilder,
              private complexStringService: ComplexStringService,
              private snotifyService: SnotifyService) { }

  ngOnInit() {
    
  }

  addComment(commentBody: string){
    this.keyDetails.comments.push({userId: this.userService.getCurrrentUser().id,
                                   text: commentBody,   
                                   createdOn: Date.now});
    this.complexStringService.update(this.keyDetails, this.keyDetails.id)
      .subscribe(
        (data) => {
          this.keyDetails = data
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

  getUserNameById(id: number){
    let name: string;
    this.userService.getOne(id).subscribe(data => name = data.fullName);
    return name;
  }

}
