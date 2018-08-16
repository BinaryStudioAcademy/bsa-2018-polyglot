import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TabCommentsComponent } from './tab-comments.component';

describe('TabCommentsComponent', () => {
  let component: TabCommentsComponent;
  let fixture: ComponentFixture<TabCommentsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TabCommentsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TabCommentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
