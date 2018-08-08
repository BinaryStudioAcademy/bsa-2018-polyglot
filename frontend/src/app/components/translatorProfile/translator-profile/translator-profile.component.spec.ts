import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TranslatorProfileComponent } from './translator-profile.component';

describe('TranslatorProfileComponent', () => {
  let component: TranslatorProfileComponent;
  let fixture: ComponentFixture<TranslatorProfileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TranslatorProfileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TranslatorProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
