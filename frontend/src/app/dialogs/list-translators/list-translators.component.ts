import { Component, OnInit, Input, EventEmitter, Output, ViewChild, ElementRef } from '@angular/core';
import { ProjecttranslatorsService } from '../../services/projecttranslators.service';
import { UserProfilePrev } from '../../models/user/user-profile-prev';
import { Observable } from 'rxjs';
import { FormControl } from '@angular/forms';
import { startWith, map } from 'rxjs/operators';
import { MatAutocompleteSelectedEvent, MatChipInputEvent } from '@angular/material';
import { ENTER, COMMA } from '@angular/cdk/keycodes';
import { AppStateService } from '../../services/app-state.service';
import { Role } from '../../models';

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
    translatorCtrl = new FormControl();
    filteredtranslators: Observable<UserProfilePrev[]>;
    translators: string[] = [];

    @ViewChild('translatorInput') translatorInput: ElementRef<HTMLInputElement>;

    constructor(private stateService: AppStateService) {

    }

    ngOnInit() {
        this.filteredtranslators = this.translatorCtrl.valueChanges.pipe(
            startWith(null),
            map((translator: UserProfilePrev | null) => translator ? this._filter(translator) : this.options.slice()));
    }

    add(event: MatChipInputEvent): void {
        const input = event.input;
        const value = event.value;

        if ((value || '').trim()) {
            this.translators[0] = value;
        }

        if (input) {
            input.value = '';
        }

        this.translatorCtrl.setValue(null);
    }

    remove(translator: string): void {
        const index = this.translators.indexOf(translator);

        if (index >= 0) {
            this.translators.splice(index, 1);
        }
    }

    selected(event: MatAutocompleteSelectedEvent): void {
        this.translators[0] = event.option.viewValue;
        this.translatorInput.nativeElement.value = '';
        this.translatorCtrl.setValue(null);
    }

    private _filter(value: UserProfilePrev): UserProfilePrev[] {
        const filterValue = value.fullName.toLowerCase();

        return this.options.filter(translator => translator.fullName.toLowerCase().indexOf(filterValue) === 0);
    }

    onSave() {
        let user: UserProfilePrev;
        for (var i = 0; i < this.options.length; i++) {
            if (this.options[i].fullName === this.translators[0]) {
                user = this.options[i];
                break;
            }
        }
        if (!user) {
            user = null;
        }
        this.chooseUserEvent.emit({ user: user, translationId: this.translationId, langId: this.langId });
    }

    isTranslator(){
        this.stateService.currentDatabaseUser.userRole === Role.Translator
    }

    isEnoughRigts(){
        //ToDo: Add rights when it will implemented.
        return !this.isTranslator();
    }

    onSelfAssign(){
        this.chooseUserEvent.emit({ user: this.stateService.currentDatabaseUser, translationId: this.translationId, langId: this.langId })
    }

    onClose() {

    }
}
