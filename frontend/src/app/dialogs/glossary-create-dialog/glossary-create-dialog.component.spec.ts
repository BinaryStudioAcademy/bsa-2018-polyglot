import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GlossaryCreateDialogComponent } from './glossary-create-dialog.component';

describe('GlossaryCreateDialogComponent', () => {
  let component: GlossaryCreateDialogComponent;
  let fixture: ComponentFixture<GlossaryCreateDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GlossaryCreateDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GlossaryCreateDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
