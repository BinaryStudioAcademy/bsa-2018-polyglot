import { Component, OnInit, Inject } from '@angular/core';
import { MatBottomSheetRef, MAT_BOTTOM_SHEET_DATA } from '../../../../node_modules/@angular/material';

@Component({
  selector: 'app-machine-transaltion-bottom-sheet',
  templateUrl: './machine-transaltion-bottom-sheet.component.html',
  styleUrls: ['./machine-transaltion-bottom-sheet.component.sass']
})
export class MachineTransaltionBottomSheetComponent implements OnInit {

  constructor(
    @Inject(MAT_BOTTOM_SHEET_DATA) public data: any,
    private bottomSheetRef: MatBottomSheetRef<MachineTransaltionBottomSheetComponent>) { }

  ngOnInit() {
  }
  onNoClick(): void {
    this.bottomSheetRef.dismiss(this.data);
  }
}
