import { Component, OnInit, Inject, Output, EventEmitter } from '@angular/core';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-team-add-member',
  templateUrl: './team-add-member.component.html',
  styleUrls: ['./team-add-member.component.sass']
})
export class TeamAddMemberComponent implements OnInit {
  @Output() onAssign = new EventEmitter<Array<number>>(true);
  translators: Array<any> = [];
  public selectedTeams: Array<any> = [];
  disabled: boolean = true;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<TeamAddMemberComponent>,
    public router: Router,
    private userService: UserService
  ) {

    if (data && data.translators) {
      this.translators = data.translators;
      this.translators.sort(this.compareId);
    }
  }

  ngOnInit() {
  }

  change(e, teams) {
    this.selectedTeams = teams.selectedOptions.selected.map(item => item.value);
    this.disabled = !this.selectedTeams.length;
  }

  assign() {

    if (this.selectedTeams.length > 0) {
      this.onAssign.emit(this.selectedTeams);
    }
    this.dialogRef.close();
  }

  redirectById(id: number) {
    this.dialogRef.close();
    if (this.userService.getCurrentUser().id == id) {
      this.router.navigate(['/profile']);
    }
    else {
      this.router.navigate(['/user', id]);
    }
  }

  getAvatarUrl(person): String {
    if (person.avatarUrl)
      return person.avatarUrl;
    else
      return "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTTsrMId-b7-CLWIw6S80BQZ6Xqd7jX0rmU9S7VSv_ngPOU7NO-6Q";
  }

  compareId(a, b) {
    if (a.id < b.id)
      return -1;
    if (a.id > b.id)
      return 1;
    return 0;
  }


}
