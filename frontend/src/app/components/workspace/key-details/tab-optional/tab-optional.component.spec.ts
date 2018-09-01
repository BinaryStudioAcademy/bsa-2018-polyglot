import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TabOptionalComponent } from './tab-optional.component';

describe('TabOptionalComponent', () => {
  let component: TabOptionalComponent;
  let fixture: ComponentFixture<TabOptionalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TabOptionalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TabOptionalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
