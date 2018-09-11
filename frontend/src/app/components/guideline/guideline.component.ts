import { Component, OnInit } from '@angular/core';
import { AppStateService } from '../../services/app-state.service';
import { UserProfile } from '../../models';

@Component({
    selector: 'app-guideline',
    templateUrl: './guideline.component.html',
    styleUrls: ['./guideline.component.sass']
})
export class GuidelineComponent implements OnInit {

    user: UserProfile;

    constructor(private appState: AppStateService) { }

    ngOnInit() {
        this.user = this.appState.currentDatabaseUser;
    }
}
