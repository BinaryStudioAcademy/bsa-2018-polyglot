import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChooseProficiencyDialogComponent } from './choose-proficiency-dialog.component';

describe('ChooseProficiencyDialogComponent', () => {
  let component: ChooseProficiencyDialogComponent;
  let fixture: ComponentFixture<ChooseProficiencyDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChooseProficiencyDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChooseProficiencyDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
