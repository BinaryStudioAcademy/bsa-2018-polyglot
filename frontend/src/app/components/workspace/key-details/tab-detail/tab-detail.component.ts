import { Component, OnInit, Input } from '@angular/core';
import { MatDialog } from '@angular/material';
import { ImgDialogComponent } from '../../../../dialogs/img-dialog/img-dialog.component';
import { IString } from '../../../../models/string';
import { UserService } from '../../../../services/user.service';
import { ActivatedRoute } from '@angular/router';
import { UserProfile } from '../../../../models';

@Component({
    selector: 'app-tab-detail',
    templateUrl: './tab-detail.component.html',
    styleUrls: ['./tab-detail.component.sass']
})
export class TabDetailComponent implements OnInit {

    @Input() public keyDetails: IString;
    @Input() public isLoad: boolean;

    private user: UserProfile;

    constructor(public dialog: MatDialog,
        private userService: UserService,
        private route: ActivatedRoute) { }

    ngOnInit() {
        this.user = { fullName: '', avatarUrl: '' };
        this.route.params.subscribe(
            value => {
                this.userService.getOne(this.keyDetails.createdBy).subscribe(
                    (user: UserProfile) => {
                        this.user = user;
                    },
                    err => {
                        this.user = { fullName: '', avatarUrl: '' };
                    }
                );
            }
        );


    }

    onImageClick(keyDetails) {
        if (keyDetails.pictureLink) {
            let dialogRef = this.dialog.open(ImgDialogComponent, {
                data: {
                    imageUri: keyDetails.pictureLink
                }
            });
        }
    }

}
