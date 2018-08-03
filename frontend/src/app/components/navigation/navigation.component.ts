import {ChangeDetectorRef, Component, OnDestroy} from '@angular/core';
import {MediaMatcher} from '@angular/cdk/layout';
import { UserService } from '../../services/user.service';
import { HttpClient } from '@angular/common/http';



@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.sass']
})
export class NavigationComponent implements OnDestroy {
  mobileQuery: MediaQueryList;
  private _mobileQueryListener: () => void;
  
  
  constructor(changeDetectorRef: ChangeDetectorRef, media: MediaMatcher, private UserService: UserService) {
    this.mobileQuery = media.matchMedia('(max-width: 640px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
    this.UserService.getOne(5).subscribe((data: String) => console.log(data));
    this.UserService.getList().subscribe((data: String[]) => console.log(data));
  }

ngOnDestroy(): void {
  this.mobileQuery.removeListener(this._mobileQueryListener);
}

}
