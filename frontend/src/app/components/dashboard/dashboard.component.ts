import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppStateService } from '../../services/app-state.service';
import { Role } from '../../models';

@Component({
	selector: 'app-dashboard',
	templateUrl: './dashboard.component.html',
	styleUrls: ['./dashboard.component.sass']
})
export class DashboardComponent {
	routeLinks: any[];
	activeLinkIndex = 0;

	constructor(private router: Router, private stateService: AppStateService) {
		this.routeLinks = [
			{
				label: 'Projects',
				link: 'projects',

			}, {
				label: 'Teams',
				link: 'teams',

			}
		];
		if(this.stateService.currentDatabaseUser.userRole === Role.Manager){
			this.routeLinks.push(
				{
					label: 'Glossaries',
					link: 'glossaries',
				}
			)
		}

	}
}

