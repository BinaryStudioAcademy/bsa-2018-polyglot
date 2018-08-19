import { Component, OnInit, Input } from '@angular/core';
import { IString } from '../../../../models/string';
import { UserService } from '../../../../services/user.service';

@Component({
  selector: 'app-tab-history',
  templateUrl: './tab-history.component.html',
  styleUrls: ['./tab-history.component.sass']
})
export class TabHistoryComponent implements OnInit {

  // @Input()  public keyDetails: any;
  public keyDetails: any;
  private translationDetails: any;
  private history: any[] = new Array<any>();


  constructor(private userService: UserService) { 
    this.keyDetails =   {
      id: 1,
      key: 'title',
      projectId: 3,
      language: 'English',
      originalValue: 'Operation Valkyrie',
      description: 'file title',
      pictureLink: 'sss',
      translations: [
        {
          language: 'Ukrainian',
          translationValue: 'Операція: "ВАЛЬКІРІЯ"',
          userId: 1,
          createdOn: '2018-08-22',
          history: [
            {
              translationValue: 'Операція Валькірія',
              userId: 2,
              createdOn: '2018-08-17'
            },
            {
              translationValue: 'Операція "Валькірія"',
              userId: 3,
              createdOn: '2018-08-18'
            },
            {
              translationValue: 'Операція: "Валькірія"',
              userId: 3,
              createdOn: '2018-08-19'
            }
          ]
        }
      ],
      comments: [],
      tags: []
    };
  }

  ngOnInit() {
    // for test
    this.translationDetails = this.keyDetails.translations[0];

    if (this.translationDetails.history.length == 0) {
      this.userService.getOne(this.translationDetails.userId).subscribe(
        (user) => {
          this.history.push({
            user: user.fullName,
            avatarUrl: user.avatarUrl,
            action: 'translated',
            from: this.keyDetails.originalValue,
            to: this.translationDetails.translationValue,
            when: this.translationDetails.createdOn
          });
        }
      );
    } else {
      this.userService.getOne(this.translationDetails.history[0].userId).subscribe(
        (user) => {
          this.history.push({
            user: user.fullName,
            avatarUrl: user.avatarUrl,
            action: 'translated',
            from: this.keyDetails.originalValue,
            to: this.translationDetails.history[0].translationValue, 
            when: this.translationDetails.history[0].createdOn
          });
        }
      );
      for (let i = 1; i < this.translationDetails.history.length; i++) {
        this.userService.getOne(this.translationDetails.history[i].userId).subscribe(
          (user) => {
            this.history.push({
              user: user.fullName,
              avatarUrl: user.avatarUrl,
              action: 'changed',
              from: this.translationDetails.history[i-1].translationValue,
              to: this.translationDetails.history[i].translationValue,
              when: this.translationDetails.history[i].createdOn
            });
          }
        ); 
      }
      this.userService.getOne(this.translationDetails.userId).subscribe(
        (user) => {
          this.history.push({
            user: user.fullName,
            avatarUrl: user.avatarUrl,
            action: 'changed', 
            from: this.translationDetails.history[this.translationDetails.history.length - 1].translationValue,
            to: this.translationDetails.translationValue,
            when: this.translationDetails.createdOn
          });
        }
      );
    }
    console.log(this.history);
  }


}
