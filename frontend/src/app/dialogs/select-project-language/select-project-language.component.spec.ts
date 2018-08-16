import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectProjectLanguageComponent } from './select-project-language.component';

describe('SelectProjectLanguageComponent', () => {
  let component: SelectProjectLanguageComponent;
  let fixture: ComponentFixture<SelectProjectLanguageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelectProjectLanguageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectProjectLanguageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
