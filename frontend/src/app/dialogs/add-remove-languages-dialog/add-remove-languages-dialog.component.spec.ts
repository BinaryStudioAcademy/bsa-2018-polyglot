import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRemoveLanguagesDialogComponent } from './add-remove-languages-dialog.component';

describe('AddRemoveLanguagesDialogComponent', () => {
  let component: AddRemoveLanguagesDialogComponent;
  let fixture: ComponentFixture<AddRemoveLanguagesDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddRemoveLanguagesDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddRemoveLanguagesDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
