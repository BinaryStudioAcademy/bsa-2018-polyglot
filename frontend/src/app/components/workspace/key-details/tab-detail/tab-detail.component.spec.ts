import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TabDetailComponent } from './tab-detail.component';

describe('TabDetailComponent', () => {
  let component: TabDetailComponent;
  let fixture: ComponentFixture<TabDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TabDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TabDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
