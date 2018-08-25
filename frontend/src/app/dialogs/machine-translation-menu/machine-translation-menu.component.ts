import { Component, OnInit, Input, EventEmitter, Output, OnChanges, SimpleChanges } from '@angular/core';
import { TranslationService } from '../../services/translation.service';

@Component({
    selector: 'app-machine-translation-menu',
    templateUrl: './machine-translation-menu.component.html',
    styleUrls: ['./machine-translation-menu.component.sass']
})
export class MachineTranslationMenuComponent implements OnInit,OnChanges{

    @Input() data : any;
    @Input() keyId : any;
    @Input() IsLoadingSpinner : boolean;
    @Output() selectTranslationEvent = new EventEmitter<any>();

    public Translation : any;

    constructor() { }

    ngOnInit() {
        this.Translation = this.data;
        
      }

      ngOnChanges(changes:SimpleChanges){
        console.log(changes);
        if(changes['data']){
           this.Translation = changes.data.currentValue;
           this.IsLoadingSpinner = false;
        }
      }
    
      onSelect(): void {
        this.selectTranslationEvent.emit({translation : this.Translation,keyId : this.keyId});
      }

}
