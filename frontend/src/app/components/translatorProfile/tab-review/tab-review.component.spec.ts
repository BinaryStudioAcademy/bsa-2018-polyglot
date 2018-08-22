import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TabReviewComponent } from './tab-review.component';

describe('TabReviewComponent', () => {
  let component: TabReviewComponent;
  let fixture: ComponentFixture<TabReviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TabReviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TabReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
