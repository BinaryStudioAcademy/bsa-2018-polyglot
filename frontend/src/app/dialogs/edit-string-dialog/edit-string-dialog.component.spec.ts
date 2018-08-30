import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditStringDialogComponent } from './edit-string-dialog.component';

describe('EditStringDialogComponent', () => {
  let component: EditStringDialogComponent;
  let fixture: ComponentFixture<EditStringDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditStringDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditStringDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
