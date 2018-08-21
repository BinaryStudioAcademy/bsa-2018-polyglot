import { TestBed, inject } from '@angular/core/testing';

import { LandingGuard } from './landing-guard.service';

describe('LandingGuardService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LandingGuard]
    });
  });

  it('should be created', inject([LandingGuard], (service: LandingGuard) => {
    expect(service).toBeTruthy();
  }));
});
