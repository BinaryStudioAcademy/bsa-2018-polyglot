import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListTranslatorsComponent } from './list-translators.component';

describe('ListTranslatorsComponent', () => {
  let component: ListTranslatorsComponent;
  let fixture: ComponentFixture<ListTranslatorsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListTranslatorsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListTranslatorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
