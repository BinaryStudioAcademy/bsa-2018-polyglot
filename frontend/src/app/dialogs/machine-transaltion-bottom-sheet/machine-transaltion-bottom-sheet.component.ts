import { Component, OnInit } from '@angular/core';
import { MatBottomSheetRef } from '../../../../node_modules/@angular/material';

@Component({
  selector: 'app-machine-transaltion-bottom-sheet',
  templateUrl: './machine-transaltion-bottom-sheet.component.html',
  styleUrls: ['./machine-transaltion-bottom-sheet.component.sass']
})
export class MachineTransaltionBottomSheetComponent implements OnInit {

  constructor(private bottomSheetRef: MatBottomSheetRef<MachineTransaltionBottomSheetComponent>) { }

  ngOnInit() {
  }

}
