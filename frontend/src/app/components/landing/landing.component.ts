import { Component, OnInit } from '@angular/core';
import { LoginDialogComponent } from '../../dialogs/login-dialog/login-dialog.component';
import { SignupDialogComponent } from '../../dialogs/signup-dialog/signup-dialog.component';
import { MatDialog } from '@angular/material';
import { AuthService } from '../../services/auth.service';
import { Router } from '../../../../node_modules/@angular/router';
<<<<<<< HEAD

=======
>>>>>>> 65efe20b62a8974648f1746197154dbb665f2cd4

@Component({
  selector: 'app-root',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.sass'],
  providers: [AuthService]
})
export class LandingComponent implements OnInit {
  title: string;

  constructor(
    public dialog: MatDialog,
    public router: Router
  ) {
  }

  ngOnInit() {
    document.body.classList.add('bg-image');
  }

  onSignUpClick() {
    this.dialog.open(SignupDialogComponent);
  }

  onLoginClick() {
<<<<<<< HEAD
    this.dialog.open(LoginDialogComponent).afterClosed().subscribe(() =>this.router.navigate(['/dashboard']));
      
=======
    this.dialog.open(LoginDialogComponent).afterClosed().subscribe(() =>this.router.navigate(['/dashboard']));  
>>>>>>> 65efe20b62a8974648f1746197154dbb665f2cd4
  }
}
