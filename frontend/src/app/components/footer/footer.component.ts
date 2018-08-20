import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { AppStateService } from '../../services/app-state.service';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.sass']
})
export class FooterComponent implements OnInit {

  constructor(private authService: AuthService, private appState: AppStateService) { }

  ngOnInit() {
  }

  onLogoutClick() {
    this.authService.logout();
  }

  isLoggedIn() {
    return this.appState.LoginStatus;
  }

}
