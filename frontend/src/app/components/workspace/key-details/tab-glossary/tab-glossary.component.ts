import { Component, OnInit, OnChanges, Input } from '@angular/core';
import { GlossaryTerm } from '../../../../models/glossaryTerm';
import { GlossaryService } from '../../../../services/glossary.service';

@Component({
  selector: 'app-tab-glossary',
  templateUrl: './tab-glossary.component.html',
  styleUrls: ['./tab-glossary.component.sass']
})
export class TabGlossaryComponent implements OnInit, OnChanges  {

  glossary: GlossaryTerm[] = [];
  // base: string;
  // translation: string;

  @Input() base: string;
  @Input() translation: string;

  constructor(private glossaryService: GlossaryService) { }

  ngOnInit() {
  }
  
  ngOnChanges(){
    debugger;
    this.glossary = [];
    if(!this.translation){
      this.translation = '';
    }
    this.glossary = this.glossaryService.fakeGlossaryParse(this.base, this.translation);
  }

}
