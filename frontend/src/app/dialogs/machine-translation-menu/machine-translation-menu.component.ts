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
    @Output() selectTranslationEvent = new EventEmitter<any>();

    public Translation : any = " ";
    public isLoading : boolean;

    constructor() { }

    ngOnInit() {
      this.isLoading = true;
        if(this.Translation === this.data){
        this.isLoading = false;
        }
      }

      ngOnChanges(changes:SimpleChanges){
        if(changes['data']){
           this.Translation = changes.data.currentValue;
           this.isLoading = false;
        }
      }
    
      onSelect(): void {
        this.selectTranslationEvent.emit({translation : this.Translation,keyId : this.keyId});
      }

}
