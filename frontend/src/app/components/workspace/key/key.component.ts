import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ComplexStringService } from '../../../services/complex-string.service';

@Component({
  selector: 'app-workspace-key',
  templateUrl: './key.component.html',
  styleUrls: ['./key.component.sass']
})
export class KeyComponent implements OnInit {

  @Input() public key: any;
  /*
  @Output() idEvent = new EventEmitter<number>();
  */
  constructor(private dataProvider: ComplexStringService) { }

  ngOnInit() {
  }

  onDeleteString() {
    this.dataProvider.delete(this.key.id)
    .subscribe(() => {});
    /*
    this.idEvent.emit(this.key.id);
    */
  }

}
