import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectActivitiesComponent } from './project-activities.component';

describe('ProjectActivitiesComponent', () => {
  let component: ProjectActivitiesComponent;
  let fixture: ComponentFixture<ProjectActivitiesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectActivitiesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectActivitiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
