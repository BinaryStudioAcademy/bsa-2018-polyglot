import { Component, OnInit, Input } from '@angular/core';
import { UserService } from '../../../../services/user.service';
import { ActivatedRoute } from '../../../../../../node_modules/@angular/router';
import { ComplexStringService } from '../../../../services/complex-string.service';

@Component({
  selector: 'app-tab-history',
  templateUrl: './tab-history.component.html',
  styleUrls: ['./tab-history.component.sass']
})

export class TabHistoryComponent implements OnInit {

  public keyDetails: any;
  public translationDetails: any;
  private history: Array<any>;
  private users = {};
  private keyId: number;

  constructor(
    private dataProvider: ComplexStringService,
    private userService: UserService,
    private route: ActivatedRoute
  ) {

  }

  ngOnInit() {
    this.route.params.subscribe(
      value => {
        this.keyId = value.keyId;
        this.dataProvider.getById(value.keyId).subscribe(
          (data: any) => {
            this.keyDetails = data;
            for (let i = 0; i < data.translations.length; i++) {
              if (!this.users[data.translations[i].userId]) {
                this.userService.getOne(data.translations[i].userId).subscribe(
                  (user) => this.users[data.translations[i].userId] = user
                );
              }
              for (let j = 0; j < data.translations[i].history.length; j++) {
                if (!this.users[data.translations[i].history[j].userId]) {
                  this.userService.getOne(data.translations[i].history[j].userId).subscribe(
                    (user) => this.users[data.translations[i].history[j].userId] = user
                  );
                }
              }
            }
          }
        );
      }
    );
  }

  showHistory(index) {
    this.history = new Array<any>();

    this.dataProvider.getById(this.keyId).subscribe(
      (result) => {
        this.keyDetails = result;

        this.translationDetails = this.keyDetails.translations[index];

        if (this.translationDetails) {
          if (this.translationDetails.history.length === 0) {
            this.history.unshift({
              user: this.users[this.translationDetails.userId].fullName,
              avatarUrl: this.users[this.translationDetails.userId].avatarUrl,
              action: 'translated',
              from: this.keyDetails.base,
              to: this.translationDetails.translationValue,
              when: this.translationDetails.createdOn
            });
          } else {
            this.history.unshift({
              user: this.users[this.translationDetails.history[0].userId].fullName,
              avatarUrl: this.users[this.translationDetails.history[0].userId].avatarUrl,
              action: 'translated',
              from: this.keyDetails.base,
              to: this.translationDetails.history[0].translationValue, 
              when: this.translationDetails.history[0].createdOn
            });
            for (let i = 1; i < this.translationDetails.history.length; i++) {
              this.history.unshift({
                user: this.users[this.translationDetails.history[i].userId].fullName,
                avatarUrl: this.users[this.translationDetails.history[i].userId].avatarUrl,
                action: 'changed',
                from: this.translationDetails.history[i-1].translationValue,
                to: this.translationDetails.history[i].translationValue,
                when: this.translationDetails.history[i].createdOn
              });
            }
            this.history.unshift({
              user: this.users[this.translationDetails.userId].fullName,
              avatarUrl: this.users[this.translationDetails.userId].avatarUrl,
              action: 'changed', 
              from: this.translationDetails.history[this.translationDetails.history.length - 1].translationValue,
              to: this.translationDetails.translationValue,
              when: this.translationDetails.createdOn
            });
          }
        }
      }
    );
  }
}
