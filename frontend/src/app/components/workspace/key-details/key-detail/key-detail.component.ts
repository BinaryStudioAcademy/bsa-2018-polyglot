import { Component, OnInit, Input } from '@angular/core';
import { IString } from '../../../../models/string';

@Component({
  selector: 'app-key-detail',
  templateUrl: './key-detail.component.html',
  styleUrls: ['./key-detail.component.sass']
})
export class KeyDetailComponent implements OnInit {

  @Input()  public keyDetails: any;

  constructor() { }

  ngOnInit() {
    console.log(this.keyDetails);
  }

}
