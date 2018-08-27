import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GlossaryEditDialogComponent } from './glossary-edit-dialog.component';

describe('GlossaryEditDialogComponent', () => {
  let component: GlossaryEditDialogComponent;
  let fixture: ComponentFixture<GlossaryEditDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GlossaryEditDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GlossaryEditDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
