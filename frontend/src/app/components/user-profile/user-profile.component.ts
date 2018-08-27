import { Component, OnInit } from '@angular/core';
import { UserProfile } from '../../models';
import { UserService } from '../../services/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-user-profile',
    templateUrl: './user-profile.component.html',
    styleUrls: ['./user-profile.component.sass']
})
export class UserProfileComponent implements OnInit {
  
    public SelectedUser: UserProfile;
    private routeSub: Subscription;
  

    constructor(private userService: UserService,
                private router: Router,
                private activatedRoute: ActivatedRoute,) { }

    ngOnInit() {
        this.activatedRoute.params.subscribe((params)=>{
            if(params.userId){
                this.userService.getOne(params.userId).subscribe((user)=>{
                    if(!user){
                        this.router.navigate(['/404']);
                    }
                    this.SelectedUser = user;
                },
                err=>{
                    this.router.navigate(['/404']);
                });
            }
            else{
                this.SelectedUser = this.userService.getCurrentUser();
            }
        });
    }

    isSelectedUserManager(){
        return this.SelectedUser.userRole == 1;
    }

}
