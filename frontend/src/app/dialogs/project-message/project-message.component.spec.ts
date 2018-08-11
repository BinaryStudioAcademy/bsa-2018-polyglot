import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectMessageComponent } from './project-message.component';

describe('ProjectMessageComponent', () => {
  let component: ProjectMessageComponent;
  let fixture: ComponentFixture<ProjectMessageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectMessageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectMessageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
