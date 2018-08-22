import { Component, OnInit, OnDestroy, DoCheck } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Project, UserProfile } from '../../models';
import { ProjectService } from '../../services/project.service';
import { MatDialog } from '@angular/material';
import { StringDialogComponent } from '../../dialogs/string-dialog/string-dialog.component';
import { SnotifyService, SnotifyPosition, SnotifyToastConfig} from 'ng-snotify';
import * as signalR from "@aspnet/signalr";
import { environment } from '../../../environments/environment';
import { UserService } from '../../services/user.service';
import { FormControl } from '../../../../node_modules/@angular/forms';
import { ComplexStringService } from '../../services/complex-string.service';


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
  public isEmpty;
  public currentPath;
  public connection;
  private user: UserProfile;
  private basicPath;
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
    private snotifyService: SnotifyService,
    private userService: UserService,
    private complexStringService: ComplexStringService
   ) {
     this.user = userService.getCurrrentUser();
     debugger;
   }

   description: string = "Are you sure you want to remove the project?";
   btnOkText: string = "Delete";
   btnCancelText: string = "Cancel";
   answer: boolean;

  

  ngOnInit() {
    debugger;
    
    this.connection = new signalR.HubConnectionBuilder()
    .withUrl(`${environment.apiUrl}/workspaceHub/`).build();
    this.connection.start().catch(err => console.log("ERROR " + err));

    this.searchQuery = '';
    console.log("q");
    this.routeSub = this.activatedRoute.params.subscribe((params) => {
      //making api call using service service.get(params.projectId); ..
      this.getProjById(params.projectId);
      this.basicPath = 'workspace/'+ params.projectId;
      this.currentPath = this.basicPath +'/key'; 
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
          {
            debugger;
            this.connection.send("newComplexString", this.project.id,  result.id);
            this.keys.push(result);
            this.selectedKey = result;
            let keyId = this.keys[0].id;   
            this.router.navigate([this.currentPath, keyId]);
            this.isEmpty = false;
          }
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
    this.connection.send("leaveProjectGroup", `${this.project.id}`);
    this.connection.stop();
  }

  getProjById(id: number){
    this.dataProvider.getById(id).subscribe(proj =>{
      this.project = proj;
      this.subscribeProjectChanges();
    });
  }

  subscribeProjectChanges(){

    this.connection.send("joinProjectGroup", `${this.project.id}`)

    this.connection.on("stringDeleted", (deletedStringId: number) => {
      if(deletedStringId)
        {
          debugger;
          this.snotifyService.info(`Key ${deletedStringId} deleted`, "String deleted")
          this.receiveId(deletedStringId);
        }
    });

    this.connection.on("stringAdded", (newStringId: number) => {
      if(!this.keys.find(s => s.id == newStringId))
        {
          if(!this.keys.find(s => s.id == newStringId))
        {
          this.complexStringService.getById(newStringId)
          .subscribe((newStr) => 
          {
            if(newStr){
              this.snotifyService.info(`New key added`, "String added")
              this.keys.push(newStr);
            }
            
          })
        }
          
        }
    });

    this.connection.on("stringTranslated", (message: string) => 
      {
        this.snotifyService.info(message , "Translated")
      });

      this.connection.on("languageAdded", (languagesIds: Array<number>) => 
      {
        console.log(languagesIds);
        this.snotifyService.info(languagesIds.join(", ") , "Language added")
        
      });
      this.connection.on("languageDeleted", (languageId: number) => 
      {
        this.snotifyService.info(`lang with id =${languageId} removed`  , "Language removed")
      });

      this.connection.on("newTranslation", (message: string) => 
      {
        this.snotifyService.info(message , "Translated")
      });
    
  }

  receiveId($event) {
    debugger;
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