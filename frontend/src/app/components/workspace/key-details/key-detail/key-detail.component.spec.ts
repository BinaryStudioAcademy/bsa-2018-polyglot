import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KeyDetailComponent } from './key-detail.component';

describe('KeyDetailComponent', () => {
  let component: KeyDetailComponent;
  let fixture: ComponentFixture<KeyDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KeyDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KeyDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
