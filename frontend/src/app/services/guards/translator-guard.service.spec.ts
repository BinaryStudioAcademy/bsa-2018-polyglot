import { TestBed, inject } from '@angular/core/testing';

import { TranslatorGuardService } from './translator-guard.service';

describe('TranslatorGuardService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TranslatorGuardService]
    });
  });

  it('should be created', inject([TranslatorGuardService], (service: TranslatorGuardService) => {
    expect(service).toBeTruthy();
  }));
});
