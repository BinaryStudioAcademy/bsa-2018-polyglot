import { Component, OnInit, Input, ViewChild, ElementRef} from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { UserProfile } from '../../../models/user-profile';
import { UserService } from '../../../services/user.service';
import { RatingService } from '../../../services/rating.service';
import { Rating } from '../../../models';
import { StarRatingColor } from '../star-rating/star-rating.component';
import { SnotifyService } from 'ng-snotify';

@Component({
  selector: 'app-tab-review',
  templateUrl: './tab-review.component.html',
  styleUrls: ['./tab-review.component.sass']
})
export class TabReviewComponent implements OnInit {
  rating:number = 1;
  starCount:number = 5;
  starColor:StarRatingColor = StarRatingColor.primary;
  starColorP:StarRatingColor = StarRatingColor.accent;
  starColorW:StarRatingColor = StarRatingColor.warn;

  @Input()  public userProfile: UserProfile;
  reviewForm = this.fb.group({
    reviewBody: ['']
    });

    @ViewChild('textArea') textArea: ElementRef;

  constructor(private fb: FormBuilder,
    private userService: UserService,
    private snotifyService: SnotifyService,
    private ratingsService: RatingService) { }

  ngOnInit() {
  }

  onRatingChanged(rating){
    this.rating = rating;
  }
  
  addReview(reviewBody: string, rating: number){
    const ratingObj = {
      comment: reviewBody,
      rate: rating,
      createdById: this.userService.getCurrentUser().id,
      userId: this.userProfile.id
    };

    this.ratingsService.create(ratingObj).subscribe(
      (data) => {
        this.reviewForm.reset();
        this.userProfile.ratings.unshift(data);
        if (data) {
          this.snotifyService.success("review added", "Success!");
      }
      else {
          this.snotifyService.error("review wasn't add", "Error!");
      }
  },
  err => {
      this.snotifyService.error("review wasn't add", "Error!");
  });
  }
}
