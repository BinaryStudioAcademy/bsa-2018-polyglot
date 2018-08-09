import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-workspace-key',
  templateUrl: './key.component.html',
  styleUrls: ['./key.component.sass']
})
export class KeyComponent implements OnInit {

  @Input() public key: any; // for now only. when i dont understand schema

  constructor() { }

  ngOnInit() {
  }

}
