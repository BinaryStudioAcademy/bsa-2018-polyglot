import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MachineTransaltionBottomSheetComponent } from './machine-transaltion-bottom-sheet.component';

describe('MachineTransaltionBottomSheetComponent', () => {
  let component: MachineTransaltionBottomSheetComponent;
  let fixture: ComponentFixture<MachineTransaltionBottomSheetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MachineTransaltionBottomSheetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MachineTransaltionBottomSheetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
