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
  isEmailSend: boolean;

  constructor(
    private authService: AuthService, 
    public dialogRef: MatDialogRef<ForgotPasswordDialogComponent>,
    private snotify: SnotifyService) {
   }

  ngOnInit() {
    this.userEmail = '';
    this.isEmailSend = false;
  }

  onResetPasswordFormSubmit(form) {
    if (form.valid) {
      if (!this.isEmailSend) {
        this.authService.sendResetPasswordConfirmation(this.userEmail);
        this.isEmailSend = true;
        this.snotify.clear();
        this.snotify.info(`Email confirmation was send to ${this.userEmail}`, {
          timeout: 15000,
          showProgressBar: true,
          closeOnClick: false,
          pauseOnHover: false        
        });
        setTimeout(
          () => this.dialogRef.close(), 
          15000
        );
      } else {
        this.snotify.clear();
        this.snotify.warning(`Email confirmation was already send to ${this.userEmail}. Check your email`, {
          timeout: 15000,
          showProgressBar: true,
          closeOnClick: false,
          pauseOnHover: false,
          buttons: [
            {text: 'Resend', action: () => {
              this.authService.sendResetPasswordConfirmation(this.userEmail);
              this.snotify.clear();
              this.snotify.info(`Email confirmation was send to ${this.userEmail}`, {
                timeout: 15000,
                showProgressBar: true,
                closeOnClick: false,
                pauseOnHover: false        
              });
              setTimeout(
                () => this.dialogRef.close(), 
                15000
              );
            }}
          ]    
        });
        setTimeout(
          () => this.dialogRef.close(), 
          15000
        );
      }
    }
  }
}
