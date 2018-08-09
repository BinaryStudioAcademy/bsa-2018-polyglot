import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { MatTableDataSource, MatPaginator } from '@angular/material';

@Component({
  selector: 'app-workspace-key-details',
  templateUrl: './key-details.component.html',
  styleUrls: ['./key-details.component.sass']
})
export class KeyDetailsComponent implements OnInit, OnDestroy {

  private routeSub: Subscription;
  private keyDetails: any; // DATA FROM NoSQL! Need some typing here :)
  protected translationsDataSource: MatTableDataSource<any>; // Should be KeyDetails type

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.keyDetails = {};

    this.translationsDataSource = new MatTableDataSource(this.keyDetails.translations);
    this.translationsDataSource.paginator = this.paginator;

    this.routeSub = this.activatedRoute.params.subscribe((params) => {
      //making api call using service service.get(params.keyId); ....

      this.keyDetails = MOCK_KEY_DETAILS;
      this.updateTable();
      console.log(params.keyId);
    });
  }

  updateTable() {
    this.translationsDataSource.data = this.keyDetails.translations;
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
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
