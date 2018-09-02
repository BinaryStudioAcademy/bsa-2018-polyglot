import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { ProjecttranslatorsService } from '../../services/projecttranslators.service';
import { UserProfilePrev } from '../../models/user/user-profile-prev';
import { Observable } from 'rxjs';
import { FormControl } from '@angular/forms';
import { startWith, map } from 'rxjs/operators';

@Component({
    selector: 'app-list-translators',
    templateUrl: './list-translators.component.html',
    styleUrls: ['./list-translators.component.sass']
})
export class ListTranslatorsComponent implements OnInit {

    @Input() projectId: number;
    @Input() options: UserProfilePrev[] = [];
    @Input() translationId: number;
    @Input() langId: number;
    @Output() chooseUserEvent = new EventEmitter<any>();

    myControl = new FormControl();
    filteredOptions: Observable<UserProfilePrev[]>;

    ngOnInit() {
      this.filteredOptions = this.myControl.valueChanges
        .pipe(
          startWith<string | UserProfilePrev>(''),
          map(value => typeof value === 'string' ? value : value.fullName),
          map(fullName => fullName ? this._filter(fullName) : this.options.slice())
        );
    }

    displayFn(user?: UserProfilePrev): string | undefined {
      return user ? user.fullName : undefined;
    }

    private _filter(name: string): UserProfilePrev[] {
      const filterValue = name.toLowerCase();
      return this.options.filter(option => option.fullName.toLowerCase().indexOf(filterValue) === 0);
    }

    onSave() {
        this.chooseUserEvent.emit( { user: this.myControl.value, translationId: this.translationId, langId: this.langId});
    }
 }
