import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TranslationInputComponent } from './translation-input.component';

describe('TranslationInputComponent', () => {
  let component: TranslationInputComponent;
  let fixture: ComponentFixture<TranslationInputComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TranslationInputComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TranslationInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
