import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { MatDialogRef } from '@angular/material';
import { SnotifyService } from 'ng-snotify';

@Component({
  selector: 'app-forgot-password-dialog',
  templateUrl: './forgot-password-dialog.component.html',
  styleUrls: ['./forgot-password-dialog.component.sass']
})
export class ForgotPasswordDialogComponent implements OnInit {

  userEmail: string;

  constructor(
    private authService: AuthService, 
    public dialogRef: MatDialogRef<ForgotPasswordDialogComponent>,
    private snotify: SnotifyService) {
   }

  ngOnInit() {
    this.userEmail = '';
  }

  onResetPasswordFormSubmit() {
    this.authService.sendResetPasswordConfirmation(this.userEmail);
    this.snotify.info(`Email confirmation was send to ${this.userEmail}`, {
      timeout: 10000,
      showProgressBar: true,
      closeOnClick: false,
      pauseOnHover: false        
    });
    setTimeout(
      () => this.dialogRef.close(), 
      10000
    );
  }
}
