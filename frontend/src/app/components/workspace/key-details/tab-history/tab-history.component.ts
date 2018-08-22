import { Component, OnInit, Input } from '@angular/core';
import { IString } from '../../../../models/string';
import { UserService } from '../../../../services/user.service';
import { AppStateService } from '../../../../services/app-state.service';
import { DatePipe} from '@angular/common';

@Component({
  selector: 'app-tab-history',
  templateUrl: './tab-history.component.html',
  styleUrls: ['./tab-history.component.sass']
})
export class TabHistoryComponent implements OnInit {

  @Input() public keyDetails: any;
  public translationDetails: any;
  private history: Array<any>;
  private users = {};


  constructor(private userService: UserService, private appState: AppStateService) { 
  }

  showHistory(index) {
    this.history = new Array<any>();

    this.translationDetails = this.keyDetails.translations[index];

    if (this.translationDetails.history.length == 0) {
      if (!this.users[this.translationDetails.userId]) {
        this.userService.getOne(this.translationDetails.userId).subscribe(
          (user) => {
            this.users[this.translationDetails.userId] = user;
            this.history.push({
              user: this.users[this.translationDetails.userId].fullName,
              avatarUrl: this.users[this.translationDetails.userId].avatarUrl,
              action: 'translated',
              from: this.keyDetails.base,
              to: this.translationDetails.translationValue,
              when: this.translationDetails.createdOn
            });
          }
        );
      } else {
        this.history.push({
          user: this.users[this.translationDetails.userId].fullName,
          avatarUrl: this.users[this.translationDetails.userId].avatarUrl,
          action: 'translated',
          from: this.keyDetails.base,
          to: this.translationDetails.translationValue,
          when: this.translationDetails.createdOn
        });
      }
    } else {
      if (!this.users[this.translationDetails.history[0].userId]) {
        this.userService.getOne(this.translationDetails.history[0].userId).subscribe(
          (user) => {
            this.users[this.translationDetails.history[0].userId] = user;
            this.history.push({
              user: this.users[this.translationDetails.history[0].userId].fullName,
              avatarUrl: this.users[this.translationDetails.history[0].userId].avatarUrl,
              action: 'translated',
              from: this.keyDetails.base,
              to: this.translationDetails.history[0].translationValue, 
              when: this.translationDetails.history[0].createdOn
            });
          }
        );
      } else {
        this.history.push({
          user: this.users[this.translationDetails.history[0].userId].fullName,
          avatarUrl: this.users[this.translationDetails.history[0].userId].avatarUrl,
          action: 'translated',
          from: this.keyDetails.base,
          to: this.translationDetails.history[0].translationValue, 
          when: this.translationDetails.history[0].createdOn
        });
      }
      for (let i = 1; i < this.translationDetails.history.length; i++) {
        if (!this.users[this.translationDetails.history[i].userId]) {
          this.userService.getOne(this.translationDetails.history[i].userId).subscribe(
            (user) => {
              this.users[this.translationDetails.history[i].userId] = user;
              this.history.push({
                user: this.users[this.translationDetails.history[i].userId].fullName,
                avatarUrl: this.users[this.translationDetails.history[i].userId].avatarUrl,
                action: 'changed',
                from: this.translationDetails.history[i-1].translationValue,
                to: this.translationDetails.history[i].translationValue,
                when: this.translationDetails.history[i].createdOn
              });
            }
          );
        } else {
          this.history.push({
            user: this.users[this.translationDetails.history[i].userId].fullName,
            avatarUrl: this.users[this.translationDetails.history[i].userId].avatarUrl,
            action: 'changed',
            from: this.translationDetails.history[i-1].translationValue,
            to: this.translationDetails.history[i].translationValue,
            when: this.translationDetails.history[i].createdOn
          });
        }
      }
      if (!this.users[this.translationDetails.userId]) {
        this.userService.getOne(this.translationDetails.userId).subscribe(
          (user) => {
            this.users[this.translationDetails.userId] = user;
            this.history.push({
              user: this.users[this.translationDetails.userId].fullName,
              avatarUrl: this.users[this.translationDetails.userId].avatarUrl,
              action: 'changed', 
              from: this.translationDetails.history[this.translationDetails.history.length - 1].translationValue,
              to: this.translationDetails.translationValue,
              when: this.translationDetails.createdOn
            });
          }
        );
      } else {
        this.history.push({
          user: this.users[this.translationDetails.userId].fullName,
          avatarUrl: this.users[this.translationDetails.userId].avatarUrl,
          action: 'changed', 
          from: this.translationDetails.history[this.translationDetails.history.length - 1].translationValue,
          to: this.translationDetails.translationValue,
          when: this.translationDetails.createdOn
        });
      }
      this.history.sort(function(a,b) {return (a.when > b.when) ? -1 : ((b.when > a.when) ? 1 : 0);} ); 
    }
  }
  
  ngOnInit() {
  }
}
