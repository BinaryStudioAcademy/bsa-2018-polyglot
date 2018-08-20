import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Project } from '../../models';
import { ProjectService } from '../../services/project.service';
import { MatDialog } from '@angular/material';
import { StringDialogComponent } from '../../dialogs/string-dialog/string-dialog.component';
import {SnotifyService, SnotifyPosition, SnotifyToastConfig} from 'ng-snotify';
import { FormControl } from '../../../../node_modules/@angular/forms';


@Component({
  selector: 'app-workspace',
  templateUrl: './workspace.component.html',
  styleUrls: ['./workspace.component.sass']
})
export class WorkspaceComponent implements OnInit, OnDestroy{

  public project: Project;
  public keys: any[];
  public searchQuery: string;
  public selectedKey: any;
  public isEmpty
  public currentPath;
  
  private routeSub: Subscription;

  options = new FormControl();

  filterOptions : string [] = [
    'Translated', 'Untranslated' , 'Human Translation' , 'Machine Transaltion' , 'With Tags'
  ]

  constructor(
    private activatedRoute: ActivatedRoute,
    private router : Router,
    private dataProvider: ProjectService,
    private dialog: MatDialog,
    private projectService: ProjectService,
    private snotifyService: SnotifyService
   ) {}

   description: string = "Are you sure you want to remove the project?";
   btnOkText: string = "Delete";
   btnCancelText: string = "Cancel";
   answer: boolean;
 
  ngOnInit() {
    this.searchQuery = '';

    this.routeSub = this.activatedRoute.params.subscribe((params) => {
      //making api call using service service.get(params.projectId); ..
      this.getProjById(params.projectId);
      this.currentPath = 'workspace/'+ params.projectId +'/key'; 
      this.dataProvider.getProjectStrings(params.projectId)
      .subscribe((data: any) => {
        if(data)
        {
          this.onSelect(data[0]);
          this.keys = data;
          this.isEmpty = this.keys.length == 0 ? true : false;
          let keyId: number;
          if(!this.isEmpty) {
            keyId = this.keys[0].id;
            this.router.navigate([this.currentPath, keyId]);
          }
        }
      });
    });
  }
  onAdvanceSearchClick() {

  } 
   
  onAddNewStringClick() {
    let dialogRef = this.dialog.open(StringDialogComponent, {
      data: {
        projectId: this.project.id
      }
      });
      dialogRef.componentInstance.onAddString.subscribe((result) => {
        if(result)
          this.keys.push(result);
          this.selectedKey = result;
          let keyId = this.keys[0].id;   
          this.router.navigate([this.currentPath, keyId]);
          this.isEmpty = false;
      })
      dialogRef.afterClosed().subscribe(()=>{
        dialogRef.componentInstance.onAddString.unsubscribe();
      });
  }

  onSelect(key: any){
    this.selectedKey = key;
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }

  getProjById(id: number){
    this.dataProvider.getById(id).subscribe(proj =>{
      this.project = proj;
    });
  }

  receiveId($event) {
    let temp = this.keys.findIndex( x => x.id === $event);
    if(this.selectedKey.id == this.keys[temp].id)
      this.selectedKey = this.keys[temp-1] ? this.keys[temp-1] : this.keys[temp+1]

    this.keys.splice(temp, 1);
    
    this.router.navigate([this.currentPath, this.selectedKey.id]);
  }

  OnSelectOption(){
    //let opt = "?options[]=one&options[]=two"
    this.dataProvider.getProjectStringsByFilter(this.project.id,this.options.value)
    .subscribe(res => {
      this.keys = res;
    })
    console.log(this.options.value);
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