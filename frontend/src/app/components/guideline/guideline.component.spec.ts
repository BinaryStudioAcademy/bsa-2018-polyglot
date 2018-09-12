import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GuidelineComponent } from './guideline.component';

describe('GuidelineComponent', () => {
  let component: GuidelineComponent;
  let fixture: ComponentFixture<GuidelineComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GuidelineComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GuidelineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
