import { TestBed, inject } from '@angular/core/testing';

import { TranslationsService } from './translations.service';

describe('TranslationsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TranslationsService]
    });
  });

  it('should be created', inject([TranslationsService], (service: TranslationsService) => {
    expect(service).toBeTruthy();
  }));
});
