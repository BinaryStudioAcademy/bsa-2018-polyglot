import { Component, OnInit, Inject } from '@angular/core';
import { Glossary, Language } from '../../models';
import { GlossaryService } from '../../services/glossary.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { SnotifyService } from 'ng-snotify';
import { LanguageService } from '../../services/language.service';

@Component({
	selector: 'app-glossary-edit-dialog',
	templateUrl: './glossary-edit-dialog.component.html',
	styleUrls: ['./glossary-edit-dialog.component.sass']
})
export class GlossaryEditDialogComponent implements OnInit {


	public glossary: Glossary;
	languages: Language[];

	constructor(
		@Inject(MAT_DIALOG_DATA) public data: Glossary,
		private glossaryService: GlossaryService,
		public dialogRef: MatDialogRef<GlossaryEditDialogComponent>,
		private snotifyService: SnotifyService,
		private languageService: LanguageService) { }


	ngOnInit() {
		this.languageService.getAll()
		.subscribe(
		(d: Language[])=> {
			this.languages = d.map(x => Object.assign({}, x));
		},
		err => {
			console.log('err', err);
		}
	);  
		this.glossary = this.data;
	}

	onSubmit(){

		this.glossaryService.update(this.glossary, this.glossary.id)
			.subscribe(
				(d) => {
					this.snotifyService.success("Glossary edited", "Success!");
					this.dialogRef.close();   
				},
				err => {
					console.log('err', err);
					this.snotifyService.error("Glossary wasn`t edited", "Error!");
					this.dialogRef.close();     
				});
	}

	onDelete(){
		this.glossaryService.delete(this.glossary.id).subscribe(
			(d) => {
				this.snotifyService.success("Glossary deleted", "Success!");
				this.dialogRef.close(); 
						
			},
			err => {
				console.log('err', err);
				this.snotifyService.error("Glossary wasn`t deleted", "Error!");
				this.dialogRef.close();     
			});
	}
	
		compareFn(l1: Language, l2: Language): boolean {
			return l1 && l2 ? l1.id === l2.id : l1 === l2;
	}
}
