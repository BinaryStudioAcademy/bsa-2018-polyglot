import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MachineTranslationMenuComponent } from './machine-translation-menu.component';

describe('MachineTranslationMenuComponent', () => {
  let component: MachineTranslationMenuComponent;
  let fixture: ComponentFixture<MachineTranslationMenuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MachineTranslationMenuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MachineTranslationMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
