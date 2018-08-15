import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-tab-detail',
  templateUrl: './tab-detail.component.html',
  styleUrls: ['./tab-detail.component.sass']
})
export class TabDetailComponent implements OnInit {

  @Input()  public keyDetails: any;

  constructor() { }

  ngOnInit() {
  }

}
