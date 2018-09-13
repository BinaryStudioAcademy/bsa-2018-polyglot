import { Component, OnInit } from '@angular/core';
import { ComplexStringService } from '../../../../services/complex-string.service';

@Component({
    selector: 'app-tab-optional',
    templateUrl: './tab-optional.component.html',
    styleUrls: ['./tab-optional.component.sass']
})
export class TabOptionalComponent implements OnInit {

    public optional = [];
    public IsLoaded: boolean = false;
    public translationSelected: boolean = false;


    constructor(private dataprovider: ComplexStringService) { }

    ngOnInit() {
        
    }

    showOptional(stringId, translationId) {
        this.translationSelected = true;
        this.optional = [];
        this.IsLoaded = false;
        if (stringId && translationId) {
            this.dataprovider.getOptionalTranslation(stringId, translationId)
                .subscribe(
                    (res) => {
                        this.optional = res;
                        this.IsLoaded = true;
                        
                    });
        } else {
            this.IsLoaded = true;
            return;
        }
    }
    public hideOptional() {
        this.translationSelected = false;
      }
}
