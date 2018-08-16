import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteProjectLanguageComponent } from './delete-project-language.component';

describe('DeleteProjectLanguageComponent', () => {
  let component: DeleteProjectLanguageComponent;
  let fixture: ComponentFixture<DeleteProjectLanguageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeleteProjectLanguageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteProjectLanguageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
