import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SaveStringConfirmComponent } from './save-string-confirm.component';

describe('SaveStringConfirmComponent', () => {
  let component: SaveStringConfirmComponent;
  let fixture: ComponentFixture<SaveStringConfirmComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SaveStringConfirmComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SaveStringConfirmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
