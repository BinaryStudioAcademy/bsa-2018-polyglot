import { Validator, NG_VALIDATORS, AbstractControl } from '@angular/forms';
import { Directive, Input } from '@angular/core';


@Directive({
    selector: '[appNoWhiteSpace]',
    providers: [{
        provide: NG_VALIDATORS,
        useExisting: NoWhiteSpaceDirective,
        multi: true
    }]
})
export class NoWhiteSpaceDirective implements Validator {

    validate(control: AbstractControl): { [key: string]: any } {
        
        let isWhitespace = (control.value || '').trim().length === 0;
        let isValid = !isWhitespace;
        return isValid ? null : { 'WhiteSpace' : true };
    }

}
