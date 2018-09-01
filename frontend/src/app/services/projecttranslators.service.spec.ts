import { TestBed, inject } from '@angular/core/testing';

import { ProjecttranslatorsService } from './projecttranslators.service';

describe('ProjecttranslatorsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProjecttranslatorsService]
    });
  });

  it('should be created', inject([ProjecttranslatorsService], (service: ProjecttranslatorsService) => {
    expect(service).toBeTruthy();
  }));
});
