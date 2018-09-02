import { Component, OnInit, Input, EventEmitter, Output, ViewChild, ElementRef } from '@angular/core';
import { ProjecttranslatorsService } from '../../services/projecttranslators.service';
import { UserProfilePrev } from '../../models/user/user-profile-prev';
import { Observable } from 'rxjs';
import { FormControl } from '@angular/forms';
import { startWith, map } from 'rxjs/operators';
import { MatAutocompleteSelectedEvent, MatChipInputEvent } from '@angular/material';
import { ENTER, COMMA } from '@angular/cdk/keycodes';

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
    visible = true;
    selectable = true;
    removable = true;
    addOnBlur = false;
    separatorKeysCodes: number[] = [ENTER, COMMA];
    fruitCtrl = new FormControl();
    filteredFruits: Observable<UserProfilePrev[]>;
    fruits: string[] = [];
    allFruits: UserProfilePrev[] = [{fullName:'Nata', id: 5, avatarUrl: ''}, {fullName:'Nina', id: 3, avatarUrl: ''}, {fullName:'Nina', id: 3, avatarUrl: ''}];

    @ViewChild('fruitInput') fruitInput: ElementRef<HTMLInputElement>;

    constructor() {

    }

    ngOnInit() {
        this.filteredFruits = this.fruitCtrl.valueChanges.pipe(
          startWith(null),
          map((fruit: UserProfilePrev | null) => fruit ? this._filter(fruit) : this.allFruits.slice()));
    }

    add(event: MatChipInputEvent): void {
      debugger
      const input = event.input;
      const value = event.value;

      // Add our fruit
      if ((value || '').trim()) {
        this.fruits[0] = value;
      }

      // Reset the input value
      if (input) {
        input.value = '';
      }

      this.fruitCtrl.setValue(null);
    }

    remove(fruit: string): void {
      const index = this.fruits.indexOf(fruit);

      if (index >= 0) {
        this.fruits.splice(index, 1);
      }
    }

    selected(event: MatAutocompleteSelectedEvent): void {
      this.fruits[0] = event.option.viewValue;
      this.fruitInput.nativeElement.value = '';
      this.fruitCtrl.setValue(null);
    }

    private _filter(value: UserProfilePrev): UserProfilePrev[] {
      const filterValue = value.fullName.toLowerCase();

      return this.allFruits.filter(fruit => fruit.fullName.toLowerCase().indexOf(filterValue) === 0);
    }

    onSave() {
      let user: UserProfilePrev;
      for (var i = 0; i < this.allFruits.length; i++) {
        if(this.allFruits[i].fullName === this.fruits[0])
        {
          user = this.allFruits[i];
          break;
        }
      }
      if(!user)
        {
          user = null;
        }
        console.log(user);
    }
    // myControl = new FormControl();
    // filteredOptions: Observable<UserProfilePrev[]>;

    // ngOnInit() {
    //   this.filteredOptions = this.myControl.valueChanges
    //     .pipe(
    //       startWith<string | UserProfilePrev>(''),
    //       map(value => typeof value === 'string' ? value : value.fullName),
    //       map(fullName => fullName ? this._filter(fullName) : this.options.slice())
    //     );
    // }

    // displayFn(user?: UserProfilePrev): string | undefined {
    //   return user ? user.fullName : undefined;
    // }

    // private _filter(name: string): UserProfilePrev[] {
    //   const filterValue = name.toLowerCase();
    //   return this.options.filter(option => option.fullName.toLowerCase().indexOf(filterValue) === 0);
    // }

    //onSave() {
        //this.chooseUserEvent.emit( { user: this.myControl.value, translationId: this.translationId, langId: this.langId});
    //}
 }
