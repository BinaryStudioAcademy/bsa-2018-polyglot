import { Component, OnInit, Input, ViewChild, ElementRef} from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { UserProfile } from '../../../models/user-profile';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-tab-review',
  templateUrl: './tab-review.component.html',
  styleUrls: ['./tab-review.component.sass']
})
export class TabReviewComponent implements OnInit {

  @Input()  public userProfile: UserProfile;
  reviewForm = this.fb.group({
    reviewBody: ['', Validators.required],
    rating: ['', Validators.required]
    });

    @ViewChild('textarea') textarea: ElementRef;

  constructor(private fb: FormBuilder,
    private userService: UserService,) { }

  ngOnInit() {
  }

  addReview(reviewBody: string, rating: number){
    this.userProfile.ratings.unshift({
      id: undefined,
      rate: rating,
      comment: reviewBody,
      createdBy: this.userService.getCurrrentUser(),
      createdAt: new Date(Date.now())});

      this.userService.update(this.userProfile.id, this.userProfile).subscribe(
        (data) => {
          this.userProfile = data;
          this.reviewForm.reset();
            if(data){
              console.log("added");
              //this.snotifyService.success("Comment added", "Success!");
            }
            else{
              console.log("not added");
              //this.snotifyService.error("Comment wasn't add", "Error!");
            }
        },
        err => {
          console.log("error", err);
          //this.snotifyService.error("Comment added", "Error!");
        });;
  }
}
