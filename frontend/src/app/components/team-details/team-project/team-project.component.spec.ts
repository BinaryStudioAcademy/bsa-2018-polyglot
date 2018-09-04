import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TeamProjectComponent } from './team-project.component';

describe('TeamProjectComponent', () => {
  let component: TeamProjectComponent;
  let fixture: ComponentFixture<TeamProjectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TeamProjectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TeamProjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
