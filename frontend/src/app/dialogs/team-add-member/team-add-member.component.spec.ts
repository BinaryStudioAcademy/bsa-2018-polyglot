import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TeamAddMemberComponent } from './team-add-member.component';

describe('TeamAddMemberComponent', () => {
  let component: TeamAddMemberComponent;
  let fixture: ComponentFixture<TeamAddMemberComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TeamAddMemberComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TeamAddMemberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
