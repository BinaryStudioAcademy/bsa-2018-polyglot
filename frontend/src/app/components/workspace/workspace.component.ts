import { Component, OnInit, OnDestroy, DoCheck } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Project } from '../../models';
import { ProjectService } from '../../services/project.service';
import { MatDialog} from '@angular/material';
import { StringDialogComponent } from '../../dialogs/string-dialog/string-dialog.component';
import {SnotifyService} from 'ng-snotify';
import { FormControl } from '../../../../node_modules/@angular/forms';


@Component({
  selector: 'app-workspace',
  templateUrl: './workspace.component.html',
  styleUrls: ['./workspace.component.sass']
})
export class WorkspaceComponent implements OnInit, OnDestroy, DoCheck{

  public project: Project;
  public keys: any[];
  public searchQuery: string;
  public selectedKey: any;
  public currentPath;
  public basicPath;
  private currentPage = 0;
  private elementsOnPage = 7; 
  
  private routeSub: Subscription;

  options = new FormControl();

  filterOptions : string [] = [
    'Translated', 'Untranslated' , 'Human Translation' , 'Machine Translation' , 'With Tags'
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
      this.basicPath = 'workspace/'+ params.projectId;
      this.currentPath = 'workspace/'+ params.projectId +'/key'; 
      this.dataProvider.getProjectStringsWithPagination(params.projectId, this.elementsOnPage, 0)
      .subscribe((data: any) => {
        if(data)
        {
        this.keys = data.complexStrings;
        this.onSelect(this.keys[0]);
        let keyId: number;
        if(this.keys.length !== 0) {
            keyId = this.keys[0].id;
            this.router.navigate([this.currentPath, keyId]);
          }
      }});
    this.currentPage ++;
    });
  }
  
  onAdvanceSearchClick() {

  }
  
  ngDoCheck(){

    if(this.project && this.keys && this.router.url == `/workspace/${this.project.id}` && this.keys.length != 0){
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
        if(result)
          this.keys.push(result);
          this.selectedKey = result;
          let keyId = this.keys[0].id;   
          this.router.navigate([this.currentPath, keyId]);          
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
    
    if (this.keys.length > 0) {
      this.router.navigate([this.currentPath, this.selectedKey.id]);
    } else {
      this.router.navigate([this.basicPath]);
    }
  }
  OnSelectOption(){
    //If the filters сontradict each other
    this.ContradictoryСhoise(["Translated", "Untranslated"])
    this.ContradictoryСhoise(["Human Translation", "Machine Translation"])

    this.dataProvider.getProjectStringsByFilter(this.project.id,this.options.value)
    .subscribe(res => {
      this.keys = res;
    })
    console.log(this.options.value);
  }
  
  
  public onScrollUp(): void {
    this.getKeys(
      this.currentPage,
      (keys) => {
        this.keys = keys.concat(this.keys);
      });
  }

  public onScrollDown(): void {
    this.getKeys(
      this.currentPage,
      (keys) => {
        this.keys = this.keys.concat(keys);
      });
  }

  getKeys(page: number = 1, saveResultsCallback: (keys) => void){
    return this.dataProvider. getProjectStringsWithPagination(this.project.id, this.elementsOnPage ,this.currentPage)
    .subscribe((keys: any) => {
       this.currentPage++;
       saveResultsCallback(keys.complexStrings);
      
    });
      
 }


  ContradictoryСhoise(options : string[]){
    if(this.options.value.includes(options[0]) && this.options.value.includes(options[1]))
    {
      options.forEach(element => {
        let index = this.options.value.indexOf(element);
        this.options.value.splice(index,1)
      });
    }
  }

}

