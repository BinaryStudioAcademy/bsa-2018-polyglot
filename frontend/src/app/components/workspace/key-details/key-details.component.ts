import { Component, OnInit, Input, OnDestroy, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { MatTableDataSource, MatPaginator } from '@angular/material';
import { ProjectService } from '../../../services/project.service';

@Component({
  selector: 'app-workspace-key-details',
  templateUrl: './key-details.component.html',
  styleUrls: ['./key-details.component.sass']
})
export class KeyDetailsComponent implements OnInit, OnDestroy {

  //private routeSub: Subscription;
  @Input()  public keyDetails: any; 
  public translationsDataSource: MatTableDataSource<any>; 
  public IsEdit : boolean = false;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    private activatedRoute: ActivatedRoute,
    private dataProvider: ProjectService
  ) { debugger;}

  ngOnChanges(){
    debugger;

    if(this.keyDetails){
      this.translationsDataSource = new MatTableDataSource(this.keyDetails.translations);
      this.translationsDataSource.paginator = this.paginator;

     // this.routeSub = this.activatedRoute.params.subscribe((params) => {
     //   this.updateTable();
     //   console.log(params.keyId);
     // });
    }
  }

  ngOnInit() {
    
  }

  updateTable() {
  //  console.log(this.keyDetails);
    this.translationsDataSource.data = this.keyDetails.translations;
  }

  ngOnDestroy() {
    //this.routeSub.unsubscribe();
  }

  toggle(){
    this.IsEdit = !this.IsEdit;
  //  console.log(this.IsEdit);
  }

}

const MOCK_KEY_DETAILS = {
  projectId: 1,
  language: 'ua',
  originalValue: 'Привіт',
  description: 'Вітання, що відображається на головній сторінці',
  screenshotLink: 'http://i.i.ua/cards/pic/5/0/206905.jpg',
  translations: [
    {
      language: 'en',
      translatedValue: 'Hello',
      userId: 2,
      createdOn: new Date(2018, 1, 1),
      history: [
        {
          translatedValue: 'Good afternoon',
          userId: 2,
          CreatedOn: new Date(2018, 1, 1)
        },
        {
          translatedValue: 'Hello',
          userId: 2,
          CreatedOn: new Date(2018, 1, 1)
        }
      ],
      optionalTranslations: [
        {
          translatedValue: 'Privet',
          userId: 2,
          CreatedOn: new Date(2018, 1, 1)
        }
      ]
    },
    {
      language: 'de',
      translatedValue: 'Hallo',
      userId: 1,
      CreatedOn: new Date(2018, 1, 1),
      history: [{
        translatedValue: 'Guten Morgen',
        userId: 2,
        CreatedOn: new Date(2018, 1, 1)
      },
      {
        translatedValue: 'Wie geht\'s?',
        userId: 2,
        CreatedOn: new Date(2018, 1, 1)
      }],
      optionalTranslations: []
    },
    {
      language: 'ru',
      translatedValue: 'Привет',
      userId: 4,
      CreatedOn: new Date(2018, 1, 1),
      history: [],
      optionalTranslations: []
    }
  ],

  comments: [
    {
      userId: 4,
      text: 'cool',
      CreatedOn: new Date(2018, 1, 1)
    },
    {
      userId: 6,
      text: 'You should add more traslations',
      CreatedOn: new Date(2018, 1, 1)
    }],

  tags: ['sometag', 'greeting', 'smth']
}
