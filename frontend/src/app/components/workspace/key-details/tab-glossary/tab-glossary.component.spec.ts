import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TabGlossaryComponent } from './tab-glossary.component';

describe('TabGlossaryComponent', () => {
  let component: TabGlossaryComponent;
  let fixture: ComponentFixture<TabGlossaryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TabGlossaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TabGlossaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
