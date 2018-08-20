import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TeamAssignComponent } from './team-assign.component';

describe('TeamAssignComponent', () => {
  let component: TeamAssignComponent;
  let fixture: ComponentFixture<TeamAssignComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TeamAssignComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TeamAssignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
