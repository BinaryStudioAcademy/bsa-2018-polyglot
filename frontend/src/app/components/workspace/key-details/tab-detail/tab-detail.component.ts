import { Component, OnInit, Input } from '@angular/core';
import { MatDialog } from '@angular/material';
import { ImgDialogComponent } from '../../../../dialogs/img-dialog/img-dialog.component';
import { IString } from '../../../../models/string';
import { UserService } from '../../../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserProfile } from '../../../../models';
import { EditStringDialogComponent } from '../../../../dialogs/edit-string-dialog/edit-string-dialog.component';
import { ComplexStringService } from '../../../../services/complex-string.service';

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
		private activatedRouter: ActivatedRoute,
		private router: Router,
		private stringService: ComplexStringService) { }

	ngOnInit() {
		this.user = { fullName: '', avatarUrl: '' };
		this.activatedRouter.params.subscribe(
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

	redirectById(id: number){
		if(this.userService.getCurrentUser().id == id){
		  this.router.navigate(['/profile']);
		}
		else {
			this.router.navigate(['/user', id]);
		}
	}


}
