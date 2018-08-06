import { Component, OnInit } from '@angular/core';
import { DataService } from '../../services/data.service';
import { UserService } from '../../services/user.service';

import { LoginDialogComponent } from '../../dialogs/login-dialog/login-dialog.component';
import { SignupDialogComponent } from '../../dialogs/signup-dialog/signup-dialog.component';
import { MatDialog } from '@angular/material';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.sass'],
  providers: [AuthService]
})
export class LandingComponent implements OnInit {
  title: string;

  constructor(
    public dialog: MatDialog
  ) {

  }

  ngOnInit() {
    this.title = 'Polyglot';

  }
}
