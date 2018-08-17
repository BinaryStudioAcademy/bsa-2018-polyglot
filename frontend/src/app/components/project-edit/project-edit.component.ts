import { Component, OnInit, ViewChild, NgZone, OnChanges, SimpleChanges, Input } from '@angular/core';
import {FormBuilder, FormGroup, Validators, FormControl} from '@angular/forms';
import { Project } from '../../models/project';
import { Language } from '../../models/language';
import { TypeTechnology } from '../../models/type-technology.enum';
import { ProjectService } from '../../services/project.service';
import { LanguageService } from '../../services/language.service';
import { Router } from '@angular/router';
import {SnotifyService, SnotifyPosition, SnotifyToastConfig} from 'ng-snotify';
import { debounce } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { ConfirmDialogComponent } from '../../dialogs/confirm-dialog/confirm-dialog.component';
import { MatDialog } from '@angular/material';


@Component({
  selector: 'app-project-edit',
  templateUrl: './project-edit.component.html',
  styleUrls: ['./project-edit.component.sass']
})
export class ProjectEditComponent implements OnInit, OnChanges {

  private routeSub: Subscription;
  projectImage: File;
  @Input() project: Project;
  projectForm: FormGroup = this.fb.group({
    name: [ '', [Validators.required, Validators.minLength(4)]],
    description: [ '', [Validators.maxLength(500)]],
    technology: [ '', [Validators.required]],
    mainLanguage: [ '', [Validators.required]],
  });
  languages: Language[];
  id: number;
  desc: string = "Are you sure you want to remove the project?";
  btnOkText: string = "Delete";
  btnCancelText: string = "Cancel";
  answer: boolean;

  constructor(private fb: FormBuilder,
              private projectService: ProjectService,
              private languageService: LanguageService,
              private router: Router,
              private snotifyService: SnotifyService,
              private activatedRoute: ActivatedRoute,
              private dialog: MatDialog) {  }

  ngOnInit() {
    
  }

  ngOnChanges(changes: SimpleChanges) {
    debugger;
    this.languageService.getAll()
      .subscribe(
      (d: Language[]) => {
        this.languages = d.map(x => Object.assign({}, x));
        debugger;

        let l = this.languages.find(x => x.id == this.project.mainLanguage.id);

        let i = this.languages.indexOf(l);

        this.projectForm.setValue({
          name: this.project.name,
          description: this.project.description,
          technology: this.project.technology,
          mainLanguage: this.languages[i]
        });
        debugger;
      },
      err => {
        console.log('err', err);
      });
   
  }

  receiveImage($event){
    this.projectImage = $event[0];
    $event.pop();
}

  saveChanges(project: Project): void {
    debugger;
    // project.createdOn = new Date(Date.now());
    let formData = new FormData();
    if(this.projectImage)
      formData.set("image", this.projectImage);

      console.log(project);
    formData.set("project", JSON.stringify(project));

    this.routeSub = this.activatedRoute.params.subscribe((params) => {
      this.id = params.projectId;
      
});

    this.projectService.update(formData, this.id)
    .subscribe(
      (d)=> {
        console.log(d);
        this.router.navigate(['../']);
        setTimeout(() => {
          this.snotifyService.success("Project edited", "Success!");
        }, 100);
        
      },
      err => {
        this.snotifyService.error("Project wasn`t edited", "Error!");
        console.log('err', err);
      }
    );
  }

  getAllTechnologies() {
    return Object.keys(TypeTechnology).filter(
      (type) => isNaN(<any>type) && type !== 'values'
    );
  }

  getLanguages() {
    return this.languages.map(l => l.name);
  }


  get name() {
    return this.projectForm.get('name');
  }

  get technology() {
    return this.projectForm.get('technology');
  }

  get mainLanguage() {
    return this.projectForm.get('mainLanguage');
  }

  get description() {
    return this.projectForm.get('description');
  }

  delete(id: number): void{
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '500px',
      data: {description: this.desc, btnOkText: this.btnOkText, btnCancelText: this.btnCancelText, answer: this.answer}
    });
    dialogRef.afterClosed().subscribe(result => {
      if (dialogRef.componentInstance.data.answer){
        debugger;
        this.projectService.delete(id)
        .subscribe(
          (response => {
            this.snotifyService.success("Project was deleted", "Success!");
            setTimeout(() => (this.router.navigate(['/'])), 3000);
          }),
          err => {
            this.snotifyService.error("Project wasn`t deleted", "Error!");
          });
        }
      }
    );
  }
}
