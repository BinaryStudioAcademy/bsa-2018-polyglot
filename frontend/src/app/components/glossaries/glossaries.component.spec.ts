import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GlossariesComponent } from './glossaries.component';

describe('GlossariesComponent', () => {
  let component: GlossariesComponent;
  let fixture: ComponentFixture<GlossariesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GlossariesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GlossariesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
