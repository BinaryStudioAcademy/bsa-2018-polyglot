import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ComplexStringService } from '../../services/complex-string.service';
import { SnotifyService } from 'ng-snotify';
import { IString } from '../../models/string';

@Component({
	selector: 'app-edit-string-dialog',
	templateUrl: './edit-string-dialog.component.html',
	styleUrls: ['./edit-string-dialog.component.sass']
})
export class EditStringDialogComponent implements OnInit {

	public str: IString;

	constructor(    
		@Inject(MAT_DIALOG_DATA) public data: any,
		private complexStringService: ComplexStringService,
		public dialogRef: MatDialogRef<EditStringDialogComponent>,
		private snotifyService: SnotifyService) { }

	ngOnInit() {
		this.str = this.data.string
	}

	onSubmit(){

		this.complexStringService.update(this.str, this.str.id)
		    .subscribe(
			(d) => {
				this.snotifyService.success("ComplexString was created", "Success!");
				this.dialogRef.close();
			},
			err => {
			  console.log('err', err);
			  this.snotifyService.error("ComplexString wasn`t created", "Error!");
			  this.dialogRef.close();
			});
	}
	
}
