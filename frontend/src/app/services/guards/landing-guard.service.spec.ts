import { TestBed, inject } from '@angular/core/testing';

import { LandingGuardService } from './landing-guard.service';

describe('LandingGuardService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LandingGuardService]
    });
  });

  it('should be created', inject([LandingGuardService], (service: LandingGuardService) => {
    expect(service).toBeTruthy();
  }));
});
