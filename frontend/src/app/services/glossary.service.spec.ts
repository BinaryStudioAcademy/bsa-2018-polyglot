import { TestBed, inject } from '@angular/core/testing';

import { GlossaryService } from './glossary.service';

describe('GlossaryService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GlossaryService]
    });
  });

  it('should be created', inject([GlossaryService], (service: GlossaryService) => {
    expect(service).toBeTruthy();
  }));
});
