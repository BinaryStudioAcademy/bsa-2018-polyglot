import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ProjectService } from '../../services/project.service';
import { Project } from '../../models';
import { MatDialog } from '@angular/material';
import { SnotifyService } from 'ng-snotify';
import { ConfirmDialogComponent } from '../../dialogs/confirm-dialog/confirm-dialog.component';
import { AppStateService } from '../../services/app-state.service';
import { RightService } from '../../services/right.service';
import { RightDefinition } from '../../models/rightDefinition';

@Component({
    selector: 'app-project-details',
    templateUrl: './project-details.component.html',
    styleUrls: ['./project-details.component.sass']
})
export class ProjectDetailsComponent implements OnInit {

    private routeSub: Subscription;
    public project: Project;
    checkIfUserCanAddLanguage: boolean = false;
    checkIfUserCanAddKey: boolean = false;
    checkIfUserManager: boolean = false;
    rights: RightDefinition[];

    constructor(
        private activatedRoute: ActivatedRoute,
        private dataProvider: ProjectService,
        private appState: AppStateService,
        private rightService: RightService
    ) { }

    ngOnInit() {
        this.routeSub = this.activatedRoute.params.subscribe((params) => {
            this.getProjById(params.projectId);

            this.rightService.getUserRightsInProject(params.projectId)
                .subscribe((rights) => {
                    this.rights = rights;

                    this.checkIfUserCanAddKey = (this.appState.currentDatabaseUser.userRole === 1) || this.rights.includes(RightDefinition.AddNewKey);
                    this.checkIfUserCanAddLanguage = (this.appState.currentDatabaseUser.userRole === 1) || this.rights.includes(RightDefinition.AddNewLanguage);
                    this.checkIfUserManager = this.appState.currentDatabaseUser.userRole === 1;
                });

        });
    }

    getProjById(id: number) {
        this.dataProvider.getById(id).subscribe(proj => {
            this.project = proj;
        });
    }


    


}
