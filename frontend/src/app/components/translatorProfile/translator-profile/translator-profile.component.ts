import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { CropperComponent } from '../../../dialogs/cropper-dialog/cropper.component';
import { UserService } from '../../../services/user.service';
import { UserProfile } from '../../../models/user-profile';
import { Team } from '../../../models/team';
import { TeamService } from '../../../services/teams.service';
import {SnotifyService, SnotifyPosition, SnotifyToastConfig} from 'ng-snotify';
import { ConfirmDialogComponent } from '../../../dialogs/confirm-dialog/confirm-dialog.component';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-translator-profile',
  templateUrl: './translator-profile.component.html',
  styleUrls: ['./translator-profile.component.sass']
})
export class TranslatorProfileComponent implements OnInit{

    teams : Team[];
    description: string = "Are you sure you want to leave the team?";
    btnOkText: string = "Yes";
    btnCancelText: string = "No";
    answer: boolean;

    constructor(private userService: UserService,
              public dialog: MatDialog,
              private teamService: TeamService,
              private snotifyService: SnotifyService,
              private activatedRoute: ActivatedRoute,
              private router: Router) { }

    public userProfile : any;
    public Comments: Comment[];
    Languages: Language[];
    private routeSub: Subscription;

    ngOnInit(): void {

            this.routeSub = this.activatedRoute.params.subscribe((params) => {
                this.userService.getOne(params.translatorId).subscribe((user)=>{
                    if(user.userRole == 1){
                        this.router.navigate(['/404']);
                    }
                    this.userProfile = user;
                });
            });
            this.userService.getUserRatings(this.userProfile.id).subscribe(ratings => {
            this.userProfile.ratings = ratings;
            this.userService.getUserTeams(this.userProfile.id).subscribe(t => {
                this.teams = t;
              });
        });
    
        this.Comments = [
            { CreatedBy : "Petrov Ivan",Body : "Comment body with text",CreatedOn : "12.12.2018", Rating : 4.9 ,
            Avatar : "http://static-29.sinclairstoryline.com/resources/media/2d9080f1-46ec-47b0-3874-d5190c1b02e7-2d9080f146ec47b03874d5190c1b02e7rendition_1_scottthuman5x7bluegradient.jpg?1519078303490"},
            { CreatedBy : "Savinov Ivan",Body : "Comment body with text",CreatedOn : "08.11.2018", Rating : 7.9 , 
            Avatar : "http://static-29.sinclairstoryline.com/resources/media/2d9080f1-46ec-47b0-3874-d5190c1b02e7-2d9080f146ec47b03874d5190c1b02e7rendition_1_scottthuman5x7bluegradient.jpg?1519078303490"}
        ];

        this.Languages = [
            {Name : "French",Proficiency : 47},
            {Name : "Spanish",Proficiency : 77},
            {Name : "English",Proficiency : 97},
            {Name : "OtherLang",Proficiency : 50},
            {Name : "OtherLang",Proficiency : 50},
            {Name : "OtherLang",Proficiency : 50},
            {Name : "OtherLang",Proficiency : 50}
          ];      
    }

    isOwnersProfile(){
        return this.userProfile.id == this.userService.getCurrentUser().id;
    }
    
    leaveTeam(team: Team)
    {
        const dialogRef = this.dialog.open(ConfirmDialogComponent, {
            width: '500px',
            data: {description: this.description, btnOkText: this.btnOkText, btnCancelText: this.btnCancelText, answer: this.answer}
        });
        dialogRef.afterClosed().subscribe(result => {
            if (dialogRef.componentInstance.data.answer){
                let translatorId = team.teamTranslators.find(translator => {return translator.userId === this.userProfile.id}).id;
                this.teamService.deletedTeamTranslators([translatorId]).subscribe(
                    (d) => {
                        setTimeout(() => {
                            this.snotifyService.success("Left team", "Success!");
                            }, 100);
                            this.teams = this.teams.filter(t => t.id !== team.id);
                        },
                        err => {
                            this.snotifyService.error("Team wasn`t left", "Error!");
                        }
                    );
                }
            }
        );
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

export interface Language{
    Name : string,
    Proficiency : number
  }