import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GlossaryStringDialogComponent } from './glossary-string-dialog.component';

describe('GlossaryStringDialogComponent', () => {
  let component: GlossaryStringDialogComponent;
  let fixture: ComponentFixture<GlossaryStringDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GlossaryStringDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GlossaryStringDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
