import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Project } from '../../models';

@Component({
  selector: 'app-workspace',
  templateUrl: './workspace.component.html',
  styleUrls: ['./workspace.component.sass']
})
export class WorkspaceComponent implements OnInit, OnDestroy {

  public project: Project;
  public keys: any[];
  public searchQuery: string;

  private routeSub: Subscription;

  constructor(
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.searchQuery = '';

    this.routeSub = this.activatedRoute.params.subscribe((params) => {
      //making api call using service service.get(params.projectId); ....

      console.log(params.projectId);

      this.project = MOCK_PROJECT(params.projectId);
      this.keys = MOCK_KEYS;
    });
  }

  onAdvanceSearchClick() {

  }

  onAddNewStringClick() {

  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }

}

let MOCK_KEYS = [
  {
    id: 1,
    name: 'HELLO',
    originalValue: 'hello',
    tags: ['it', 'header', 'app', 'workspace', 'lection1']
  },
  {
    id: 2,
    name: 'CANCEL',
    originalValue: 'cancel'
  },
  {
    id: 3,
    name: 'CONFIRM',
    originalValue: 'confirm'
  },
  {
    id: 4,
    name: 'HELLO',
    originalValue: 'hello',
    tags: ['it', 'header', 'app', 'workspace', 'lection1']
  },
  {
    id: 5,
    name: 'CANCEL',
    originalValue: 'cancel'
  },
  {
    id: 6,
    name: 'CONFIRM',
    originalValue: 'confirm'
  }
];

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
});