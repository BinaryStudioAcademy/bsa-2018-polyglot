import { Component, OnInit, Input } from '@angular/core';
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
import { AppStateService } from '../../../services/app-state.service';
import { LanguageService } from '../../../services/language.service';
import { LanguageStatistic, TranslatorLanguage } from '../../../models';
import { ChooseProficiencyDialogComponent } from '../../../dialogs/choose-proficiency-dialog/choose-proficiency-dialog.component';

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
              private router: Router,
              private appStateService: AppStateService,
              private languageService: LanguageService) { }

    @Input() public userProfile : any;
    public Comments: Comment[];
    Languages: TranslatorLanguage[];
    private routeSub: Subscription;

    ngOnInit(): void {
        this.userService.getUserRatings(this.userProfile.id).subscribe(ratings => {
            this.userProfile.ratings = ratings;
            this.userService.getUserTeams(this.userProfile.id).subscribe(t => {
                this.teams = t;
            });
        });

        this.languageService.getTranslatorsLanguages(this.userProfile.id).subscribe(languages=>{
            this.Languages = languages;
        });
    }

    isOwnersProfile(){
        return this.userProfile.id == this.userService.getCurrentUser().id;
    }

    isTranslator(){
        return !this.userService.isCurrentUserManager();
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
        if(this.isOwnersProfile()){
            const dialogRef = this.dialog.open(CropperComponent, {
                data: {imageUrl: this.userProfile.avatarUrl}
            });
            dialogRef.afterClosed().subscribe(result => {
                if (dialogRef.componentInstance.cropedImageBlob){
                    let formData = new FormData();
                    formData.append("image", dialogRef.componentInstance.cropedImageBlob);
                    this.userService.updatePhoto(formData).subscribe(
                        (d) => {
                            setTimeout(() => {
                                this.snotifyService.success("Photo updated.", "Success!");
                                }, 100);
                                this.userProfile = d;
                                this.appStateService.currentDatabaseUser = d;
                            },
                            err => {
                                this.snotifyService.error("Photo failed to update!", "Error!");
                            }
                        );
                    }
                }
            );
        }
    }

    openProficiencyDialog(){
        const dialogRef = this.dialog.open(ChooseProficiencyDialogComponent, {
            data: {translatorLanguages: this.Languages}
        });       
    }

}


