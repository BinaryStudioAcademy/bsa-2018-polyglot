import { Component, OnInit } from '@angular/core';
import { UserProfile } from '../../models';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.sass']
})
export class UserProfileComponent implements OnInit {
  currentUser: UserProfile;

  constructor(private userService: UserService,
              private router: Router) { }

  ngOnInit() {
    this.currentUser = this.userService.getCurrentUser();
    if(this.currentUser.userRole == 0){
      this.router.navigate(['/translator', this.currentUser.id]);
    }
  }

}
