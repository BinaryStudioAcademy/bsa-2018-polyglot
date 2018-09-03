import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectColorDialogComponent } from './select-color-dialog.component';

describe('SelectColorDialogComponent', () => {
  let component: SelectColorDialogComponent;
  let fixture: ComponentFixture<SelectColorDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelectColorDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectColorDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
