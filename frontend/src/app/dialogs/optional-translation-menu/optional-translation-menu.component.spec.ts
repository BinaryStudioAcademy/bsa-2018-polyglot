import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OptionalTranslationMenuComponent } from './optional-translation-menu.component';

describe('OptionalTranslationMenuComponent', () => {
  let component: OptionalTranslationMenuComponent;
  let fixture: ComponentFixture<OptionalTranslationMenuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OptionalTranslationMenuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OptionalTranslationMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
