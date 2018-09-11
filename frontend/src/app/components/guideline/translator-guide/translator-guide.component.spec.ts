import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TranslatorGuideComponent } from './translator-guide.component';

describe('TranslatorGuideComponent', () => {
  let component: TranslatorGuideComponent;
  let fixture: ComponentFixture<TranslatorGuideComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TranslatorGuideComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TranslatorGuideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
