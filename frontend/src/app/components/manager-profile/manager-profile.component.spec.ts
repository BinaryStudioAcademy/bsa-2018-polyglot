import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagerProfileComponent } from './manager-profile.component';

describe('ManagerProfileComponent', () => {
  let component: ManagerProfileComponent;
  let fixture: ComponentFixture<ManagerProfileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManagerProfileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManagerProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
