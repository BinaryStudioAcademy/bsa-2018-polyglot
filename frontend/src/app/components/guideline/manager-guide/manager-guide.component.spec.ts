import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagerGuideComponent } from './manager-guide.component';

describe('ManagerGuideComponent', () => {
  let component: ManagerGuideComponent;
  let fixture: ComponentFixture<ManagerGuideComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManagerGuideComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManagerGuideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
