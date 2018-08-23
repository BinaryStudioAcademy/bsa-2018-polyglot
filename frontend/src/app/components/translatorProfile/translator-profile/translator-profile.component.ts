import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { CropperComponent } from '../../../dialogs/cropper-dialog/cropper.component';
import { UserService } from '../../../services/user.service';
import { UserProfile } from '../../../models/user-profile';

@Component({
  selector: 'app-translator-profile',
  templateUrl: './translator-profile.component.html',
  styleUrls: ['./translator-profile.component.sass']
})
export class TranslatorProfileComponent implements OnInit{

    constructor(private userService: UserService,
              public dialog: MatDialog) { }

    public userProfile : any;
    public Comments: Comment[];

    ngOnInit(): void {
            this.userProfile = this.userService.getCurrrentUser();
            this.userService.getUserRatings(this.userProfile.id).subscribe(ratings => {
            this.userProfile.ratings = ratings;
        });
    
        this.Comments = [
            { CreatedBy : "Petrov Ivan",Body : "Comment body with text",CreatedOn : "12.12.2018", Rating : 4.9 ,
            Avatar : "http://static-29.sinclairstoryline.com/resources/media/2d9080f1-46ec-47b0-3874-d5190c1b02e7-2d9080f146ec47b03874d5190c1b02e7rendition_1_scottthuman5x7bluegradient.jpg?1519078303490"},
            { CreatedBy : "Savinov Ivan",Body : "Comment body with text",CreatedOn : "08.11.2018", Rating : 7.9 , 
            Avatar : "http://static-29.sinclairstoryline.com/resources/media/2d9080f1-46ec-47b0-3874-d5190c1b02e7-2d9080f146ec47b03874d5190c1b02e7rendition_1_scottthuman5x7bluegradient.jpg?1519078303490"}
        ];
    }

    calculateAvarageRating(){
        if(!this.userProfile.ratings.length) {
            return 0;
        }

        let rating: number = 0;
        for(let i = 0;i < this.userProfile.ratings.length;i++){
        rating += this.userProfile.ratings[i].rate;
        }
        return (rating / this.userProfile.ratings.length).toFixed(1);
    }

    editPhoto(){
        this.dialog.open(CropperComponent, {
            data: {imageUrl: this.userProfile.avatarUrl}
        });
    }

}

export interface Comment{
    CreatedBy : string,
    Avatar : string ,
    Body : string,
    CreatedOn : string,
    Rating : number
}