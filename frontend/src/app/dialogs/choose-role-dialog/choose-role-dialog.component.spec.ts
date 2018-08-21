import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChooseRoleDialogComponent } from './choose-role-dialog.component';

describe('ChooseRoleDialogComponent', () => {
  let component: ChooseRoleDialogComponent;
  let fixture: ComponentFixture<ChooseRoleDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChooseRoleDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChooseRoleDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
