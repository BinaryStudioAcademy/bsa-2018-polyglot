import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '../../../../../node_modules/@angular/forms';
import { ProjectService } from '../../../services/project.service';
import { LanguageService } from '../../../services/language.service';
import { Router } from '../../../../../node_modules/@angular/router';
import { SnotifyService } from '../../../../../node_modules/ng-snotify';
import { Language, Project, Team } from '../../../models';
import { TypeTechnology } from '../../../models/type-technology.enum';
import { TeamService } from '../../../services/teams.service';

@Component({
  selector: 'app-new-team',
  templateUrl: './new-team.component.html',
  styleUrls: ['./new-team.component.sass']
})
export class NewTeamComponent implements OnInit {

  constructor(private fb: FormBuilder, private teamService: TeamService,
    private router: Router,
    private snotifyService: SnotifyService,) {

  }

  ngOnInit() {
   
  }

  receiveImage($event){
      this.teamImage = $event[0];
  }

  teamImage: File;
  team: Team;
  teamForm: FormGroup = this.fb.group({
    name: [ '', [Validators.required, Validators.minLength(4)]],
     
    
  });
 

  get name() {
    return this.teamForm.get('name');
  }


}
