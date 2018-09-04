import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatSort, MatPaginator, MatTableDataSource, MatDialog } from '@angular/material';
import { FormControl, FormGroupDirective, NgForm, Validators} from '@angular/forms';
import { MatTable } from '@angular/material';
import { Translator } from '../../../models/Translator';
import { TeamService } from '../../../services/teams.service';
import { SearchService } from '../../../services/search.service';
import { ActivatedRoute } from '@angular/router';
import { Team } from '../../../models';
import { ConfirmDialogComponent } from '../../../dialogs/confirm-dialog/confirm-dialog.component';
import {SnotifyService, SnotifyPosition, SnotifyToastConfig} from 'ng-snotify';
import { Router } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { RightService } from '../../../services/right.service';
import { AppStateService } from '../../../services/app-state.service';

@Component({
  selector: 'app-team-members',
  templateUrl: './team-members.component.html',
  styleUrls: ['./team-members.component.sass']
})

export class TeamMembersComponent implements OnInit {


	@Input() teamId: number;
	teamName: string;
	teamTranslators: Translator[];
	emailToSearch: string;
	displayedColumns: string[] = ['status', 'fullName', 'email', 'rights', 'options' ];
	dataSource: MatTableDataSource<Translator> = new MatTableDataSource();
	emailFormControl = new FormControl('', [
		Validators.email,
	]);
	searchResultRecieved: boolean = false;
	ckb: boolean = false;
	public IsPagenationNeeded: boolean = true;
	public pageSize: number  = 5;
	displayNoRecords: boolean = false;
	team: Team;

	desc: string = "Are you sure you want to remove this team?";
	btnOkText: string = "Delete";
	btnCancelText: string = "Cancel";
	answer: boolean;

	descriptionLeave: string = "Are you sure you want to leave the team?";
	btnOkTextLeave: string = "Yes";
	btnCancelTextLeave: string = "No";
	answerLeave: boolean;

	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatTable) table: MatTable<any>;

	constructor(
		private teamService: TeamService,
		private snotifyService: SnotifyService,
		private stateService: AppStateService,
		private router: Router,
		private searchService: SearchService,
		private activatedRoute: ActivatedRoute,
		private dialog: MatDialog,
		private userService: UserService,
		private rightService: RightService) {
		
		
	}

	getTranslators(){
		this.teamTranslators = [];
		this.teamService.getTeam(this.teamId)
				.subscribe((data: Team) => {
					this.teamName = data.name;
					this.teamTranslators = data.teamTranslators;
					this.dataSource = new MatTableDataSource(this.teamTranslators);
					this.dataSource.sort = this.sort;
					this.ngOnChanges();
				})
	}

	ngOnInit() {
		this.dataSource.paginator = this.paginator;
		this.teamService.getTeam(this.teamId).subscribe(data => {
			this.team = data;
		})
		this.getTranslators();
	 // this.checkPaginatorNecessity();
	}

	ngOnChanges(){
		
		this.dataSource.paginator = this.paginator;
	//  this.checkPaginatorNecessity();
	}

	ngAfterViewInit() {
		// If the user changes the sort order, reset back to the first page.
		this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
	}
	
	applyFilter(filterValue: string) {
		this.dataSource.filter = filterValue.trim().toLowerCase();
		if(this.dataSource.filteredData.length==0) {
			this.displayNoRecords = true;
		} 
		else {
			this.displayNoRecords = false;
		}
	}

	searchTranslators() {
		this.getTranslators();
		this.searchService.FindTranslatorsByEmail(this.emailToSearch)
				.subscribe((data: Translator[]) => {
					this.teamTranslators = data.concat(this.teamTranslators);
					this.dataSource = new MatTableDataSource(this.teamTranslators);
					this.dataSource.paginator = this.paginator;
					this.paginator.pageIndex = 0;
				});
	}

	checkTranslatorRight(id: number, rightDefinition: number) : boolean{
		if(!this.teamTranslators)
			return false;

		let teammate = this.teamTranslators.find(t => t.userId === id);
		if(!teammate)
			return false;
			
		
			if(teammate.rights == null){
				return false;
			}
			return teammate.rights
				.find(r => r.definition == rightDefinition)
				!= null;
			
	}

	changeTranslatorRight(e, id, rightDefinition: number){
		if(e.checked){
				this.rightService.setTranslatorRight(this.teamId, id, rightDefinition).subscribe((teammate)=>{
					let teammateIndex = this.teamTranslators.findIndex(t => t.userId === id);
					this.teamTranslators[teammateIndex] = teammate;
				});
			}
		else{
				this.rightService.removeTranslatorRight(this.teamId, id, rightDefinition).subscribe((teammate)=>{
					let teammateIndex = this.teamTranslators.findIndex(t => t.userId === id);
					this.teamTranslators[teammateIndex] = teammate;});
			}
	}

	checkPaginatorNecessity(){
		if(this.teamTranslators){
			this.IsPagenationNeeded = this.teamTranslators.length > this.pageSize;

			if(this.IsPagenationNeeded){
				this.paginator.pageSize = this.pageSize;
			}
		}
		else
			this.IsPagenationNeeded = false;
	}

	delete(id: number): void {
		const dialogRef = this.dialog.open(ConfirmDialogComponent, {
			width: '500px',
			data: {description: this.desc, btnOkText: this.btnOkText, btnCancelText: this.btnCancelText, answer: this.answer}
		});
		dialogRef.afterClosed().subscribe(result => {
			if (dialogRef.componentInstance.data.answer){
				this.teamService.delete(id)
				.subscribe(
					(response => {
						this.snotifyService.success("Team was deleted", "Success!");
						setTimeout(() => (this.router.navigate(['/dashboard/teams'])), 3000);
					}),
					err => {
						this.snotifyService.error("Team wasn`t deleted", "Error!");
					});
				}
			}
		);
	}

	isMember() : boolean{
		let result = false;
		if(this.team){
			const currentUserId = this.stateService.currentDatabaseUser.id;
			this.team.teamTranslators.forEach(t => {
				t.userId == currentUserId ? result = true : result = result;
			});
		}
		return result
	}

	leaveTeam(){
		const dialogRef = this.dialog.open(ConfirmDialogComponent, {
				width: '500px',
				data: {description: this.descriptionLeave, btnOkText: this.btnOkTextLeave, btnCancelText: this.btnCancelTextLeave, answer: this.answerLeave}
		});
		dialogRef.afterClosed().subscribe(result => {
				if (dialogRef.componentInstance.data.answer){
						let translatorId = this.team.teamTranslators.find(translator => {return translator.userId === this.stateService.currentDatabaseUser.id}).id;
						this.teamService.deletedTeamTranslators([translatorId]).subscribe(
								(d) => {
										setTimeout(() => {
												this.snotifyService.success("Left team", "Success!");
												}, 100);
												this.router.navigate(['/dashboard/teams'])
										},
										err => {
												this.snotifyService.error("Team wasn`t left", "Error!");
										}
								);
						}
				}
		);
	}
}
