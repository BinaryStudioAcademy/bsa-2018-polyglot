import { Component, OnInit, OnDestroy, DoCheck } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Project, Language } from '../../models';
import { ProjectService } from '../../services/project.service';
import { MatDialog } from '@angular/material';
import { StringDialogComponent } from '../../dialogs/string-dialog/string-dialog.component';
import { SnotifyService } from 'ng-snotify';
import { FormControl } from '../../../../node_modules/@angular/forms';
import { AppStateService } from '../../services/app-state.service';
import { WorkspaceState } from '../../models/workspace-state';

@Component({
    selector: 'app-workspace',
    templateUrl: './workspace.component.html',
    styleUrls: ['./workspace.component.sass']
})

export class WorkspaceComponent implements OnInit, OnDestroy, DoCheck {
    public project: Project;
    public keys: any[];
    public searchQuery: string;
    public selectedKey: any;
    public isEmpty
    public currentPath;
    public basicPath;

    private routeSub: Subscription;

    options = new FormControl();

    filterOptions: string[] = [
        'Translated', 'Untranslated', 'Human Translation', 'Machine Translation', 'With Tags'
    ]

    constructor(
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private dataProvider: ProjectService,
        private dialog: MatDialog,
        private projectService: ProjectService,
        private snotifyService: SnotifyService,
        private appState: AppStateService
    ) { }

    description: string = "Are you sure you want to remove the project?";
    btnOkText: string = "Delete";
    btnCancelText: string = "Cancel";
    answer: boolean;

    ngOnInit() {
        this.searchQuery = '';
        console.log("q");
        this.routeSub = this.activatedRoute.params.subscribe((params) => {
            //making api call using service service.get(params.projectId); ..
            this.getProjById(params.projectId);
        });
    }
    onAdvanceSearchClick() {

    }

    ngDoCheck() {

        if (this.project && this.keys && this.router.url == `/workspace/${this.project.id}` && this.keys.length != 0) {
            this.router.navigate(['/'])
        }
    }

    onAddNewStringClick() {
        let dialogRef = this.dialog.open(StringDialogComponent, {
            data: {
                projectId: this.project.id
            }
        });
        dialogRef.componentInstance.onAddString.subscribe((result) => {
            if (result)
                this.keys.push(result);
            this.selectedKey = result;
            let keyId = this.keys[0].id;
            this.router.navigate([this.currentPath, keyId]);
            this.isEmpty = false;
        })
        dialogRef.afterClosed().subscribe(() => {
            dialogRef.componentInstance.onAddString.unsubscribe();
        });
    }

    onSelect(key: any) {
        this.selectedKey = key;
    }

    ngOnDestroy() {
        this.routeSub.unsubscribe();
    }

    getProjById(id: number) {
        this.dataProvider.getById(id).subscribe(proj => {
            this.project = proj;
            this.projectService.getProjectLanguages(this.project.id).subscribe(
                (d: Language[]) => {
                    const workspaceState = {
                        projectId: this.project.id,
                        languages: d
                    };
                    this.appState.setWorkspaceState = workspaceState;
                    this.basicPath = 'workspace/' + this.project.id;
                    this.currentPath = 'workspace/' + this.project.id + '/key';
                    this.dataProvider.getProjectStrings(this.project.id)
                        .subscribe((data: any) => {
                            if (data) {
                                this.onSelect(data[0]);
                                this.keys = data;
                                this.isEmpty = this.keys.length == 0 ? true : false;
                                let keyId: number;
                                if (!this.isEmpty) {
                                    keyId = this.keys[0].id;
                                    this.router.navigate([this.currentPath, keyId]);
                                }
                            }
                        });
                },
                err => {
                    console.log('err', err);
                }
            );
        });
    }

    receiveId($event) {
        let temp = this.keys.findIndex(x => x.id === $event);
        if (this.selectedKey.id == this.keys[temp].id)
            this.selectedKey = this.keys[temp - 1] ? this.keys[temp - 1] : this.keys[temp + 1]

        this.keys.splice(temp, 1);

        if (this.keys.length > 0) {
            this.router.navigate([this.currentPath, this.selectedKey.id]);
        } else {
            this.router.navigate([this.basicPath]);
        }
    }
    OnSelectOption() {
        //If the filters сontradict each other
        this.ContradictoryСhoise(["Translated", "Untranslated"])
        this.ContradictoryСhoise(["Human Translation", "Machine Translation"])

        this.dataProvider.getProjectStringsByFilter(this.project.id, this.options.value)
            .subscribe(res => {
                this.keys = res;
            })
        console.log(this.options.value);
    }

    ContradictoryСhoise(options: string[]) {
        if (this.options.value.includes(options[0]) && this.options.value.includes(options[1])) {
            options.forEach(element => {
                let index = this.options.value.indexOf(element);
                this.options.value.splice(index, 1)
            });
        }
    }

}



/*
let MOCK_PROJECT = (id: number): Project => ({
  id : id,
  name: 'Binary Studio Academy Project',
  description: 'Academy for young and motivated studens! Lorem ipsum dolor sit, amet consectetur adipisicing elit. Magnam distinctio repudiandae quas fugit ad quaerat impedit ipsum!  Rem quo, impedit eum adipisci, molestiae cum omnis vitae nisi minima tenetur itaque!',
  technology: 'AngularJS, Node.js',
  imageUrl: 'https://d3ot0t2g92r1ra.cloudfront.net/img/logo@3x_optimized.svg',
  createdOn: new Date(),
  manager: <any>{

  },
  mainLanguage: <any>{

  },
  teams: [],
  translations: [
    { id: 1, tanslationKey: 'Hello' },
    { id: 2, tanslationKey: 'Cancel' },
    { id: 3, tanslationKey: 'Confirm' },
    { id: 4, tanslationKey: 'Delete' }
  ],
  projectLanguageses: [],
  projectGlossaries: [],
  projectTags: []
});*/
