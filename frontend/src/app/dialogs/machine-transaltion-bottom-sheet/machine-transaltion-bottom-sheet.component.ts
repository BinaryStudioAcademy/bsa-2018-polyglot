import { Component, OnInit, Inject, OnChanges, AfterContentInit, ChangeDetectorRef } from '@angular/core';
import { MatBottomSheetRef, MAT_BOTTOM_SHEET_DATA } from '../../../../node_modules/@angular/material';
import { TranslationService } from '../../services/translation.service';

@Component({
  selector: 'app-machine-transaltion-bottom-sheet',
  templateUrl: './machine-transaltion-bottom-sheet.component.html',
  styleUrls: ['./machine-transaltion-bottom-sheet.component.sass']
})
export class MachineTransaltionBottomSheetComponent implements OnInit{

  public Translation : string;
  public isLoading: boolean;

  constructor(
    @Inject(MAT_BOTTOM_SHEET_DATA) public data: any,
    private bottomSheetRef: MatBottomSheetRef<MachineTransaltionBottomSheetComponent>,
    private service : TranslationService,private readonly _changeDetectorRef: ChangeDetectorRef) { 
    }

  ngOnInit() {
    this.isLoading = true;
    this.service.getTransation({ q : this.data.text, target : this.data.target}).subscribe((res : any) =>{
      this.Translation = res[0].translatedText;
      this.isLoading = false;
      this._changeDetectorRef.detectChanges();
    })
  }

  onNoClick(): void {
    this.bottomSheetRef.dismiss();
  }

  onYesClick(): void {
    this.bottomSheetRef.dismiss(this.Translation);
  }
}
