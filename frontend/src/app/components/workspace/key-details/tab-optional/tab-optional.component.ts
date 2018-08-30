import { Component, OnInit } from '@angular/core';
import { ComplexStringService } from '../../../../services/complex-string.service';

@Component({
    selector: 'app-tab-optional',
    templateUrl: './tab-optional.component.html',
    styleUrls: ['./tab-optional.component.sass']
})
export class TabOptionalComponent implements OnInit {

    public optional = [];

    constructor(private dataprovider: ComplexStringService) { }

    ngOnInit() {
    }

    showOptional(stringId, translationId) {
        this.optional = [];
        if (stringId && translationId) {
            this.dataprovider.getOptionalTranslation(stringId, translationId)
                .subscribe(
                    (res) => {
                        this.optional = res;
                    });
        } else {
            return;
        }
    }

}
