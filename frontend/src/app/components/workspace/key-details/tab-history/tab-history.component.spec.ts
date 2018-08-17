import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TabHistoryComponent } from './tab-history.component';

describe('TabHistoryComponent', () => {
  let component: TabHistoryComponent;
  let fixture: ComponentFixture<TabHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TabHistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TabHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
