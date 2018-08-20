import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectTeamComponent } from './project-team.component';

describe('ProjectTeamComponent', () => {
  let component: ProjectTeamComponent;
  let fixture: ComponentFixture<ProjectTeamComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectTeamComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectTeamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
