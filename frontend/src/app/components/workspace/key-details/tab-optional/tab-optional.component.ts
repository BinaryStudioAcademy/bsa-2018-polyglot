import { Component, OnInit } from '@angular/core';
import { ComplexStringService } from '../../../../services/complex-string.service';

@Component({
  selector: 'app-tab-optional',
  templateUrl: './tab-optional.component.html',
  styleUrls: ['./tab-optional.component.sass']
})
export class TabOptionalComponent implements OnInit {

  constructor(private dataprovider: ComplexStringService) { }

  ngOnInit() {
    debugger;
    this.dataprovider.getOptionalTranslation(2014, '9fc94416-36a7-480f-b087-6c891f4d06ba')
    .subscribe((res) => {debugger;});
  }

}
