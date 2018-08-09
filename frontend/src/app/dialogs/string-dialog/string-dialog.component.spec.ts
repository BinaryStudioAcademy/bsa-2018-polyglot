import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StringDialogComponent } from './string-dialog.component';

describe('StringDialogComponent', () => {
  let component: StringDialogComponent;
  let fixture: ComponentFixture<StringDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StringDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StringDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
