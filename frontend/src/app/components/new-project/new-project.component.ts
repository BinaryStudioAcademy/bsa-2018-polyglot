import { Component, OnInit, ViewChild, NgZone } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Project } from '../../models/project';
import { Language } from '../../models/language';
import { TypeTechnology } from '../../models/type-technology.enum';
import { ProjectService } from '../../services/project.service';
import { LanguageService } from '../../services/language.service';
import { Router } from '@angular/router';
import { SnotifyService, SnotifyPosition, SnotifyToastConfig } from 'ng-snotify';
import { debounce } from 'rxjs/operators';
import { MatDialog } from '@angular/material';
import { SelectProjectLanguageComponent } from '../../dialogs/select-project-language/select-project-language.component';
import { CompressFileService } from '../../services/compress-file.service';

@Component({
    selector: 'app-new-project',
    templateUrl: './new-project.component.html',
    styleUrls: ['./new-project.component.sass']
})

export class NewProjectComponent implements OnInit {

    projectImage: File;
    project: Project;
    projectForm: FormGroup = this.fb.group({
        name: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(25)]],
        description: ['', [Validators.maxLength(500)]],
        technology: ['', [Validators.required]],
        mainLanguage: ['', [Validators.required]],
    });
    languages: Language[];

    constructor(private fb: FormBuilder, private projectService: ProjectService,
        private languageService: LanguageService,
        private router: Router,
        private snotifyService: SnotifyService,
        public dialog: MatDialog,
        private compressService: CompressFileService) {

    }

    ngOnInit() {
        this.languageService.getAll()
            .subscribe(
                (d: Language[]) => {
                    this.languages = d.map(x => Object.assign({}, x));
                },
                err => {
                    console.log('err', err);
                });
    }

    receiveImage($event) {
        this.compressService.compress($event[0], { quality: 0.6 }).then((result) => {
            this.projectImage = new File([result], $event[0].name)
        });
    }

    saveChanges(project: Project): void {
        let langsToSelect = this.languages.filter((lang) => {
            return lang.id !== project.mainLanguage.id;
        });


        let dialogRef = this.dialog.open(SelectProjectLanguageComponent, {
            data: {
                langs: langsToSelect
            }
        });

        dialogRef.componentInstance.onSelect.subscribe(data => {
            if (data) {
                project.projectLanguageses = data;
                project.createdOn = new Date(Date.now());
                let formData = new FormData();
                if (this.projectImage) {
                    formData.append("image", this.projectImage);
                }

                formData.append("project", JSON.stringify(project));

                this.projectService.create(formData)
                    .subscribe(
                        (d) => {
                            this.router.navigate(['../']);
                            setTimeout(() => {
                                this.snotifyService.success("Project created", "Success!");
                            }, 100);

                        },
                        err => {
                            this.snotifyService.error("Project wasn`t created", "Error!");
                            console.log('err', err);
                        });
            }
            else {
                //this.snotifyService.error("Youe have to choose 1 project language", "Error!");   why its not working??
            }
        });


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
}