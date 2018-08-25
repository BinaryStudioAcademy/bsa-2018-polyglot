/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { GlossaryService } from './glossary.service';

describe('Service: Glossary', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GlossaryService]
    });
  });

  it('should ...', inject([GlossaryService], (service: GlossaryService) => {
    expect(service).toBeTruthy();
  }));
});
