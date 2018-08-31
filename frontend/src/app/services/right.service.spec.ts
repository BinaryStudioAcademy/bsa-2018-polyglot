import { TestBed, inject } from '@angular/core/testing';

import { RightService } from './right.service';

describe('RightService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RightService]
    });
  });

  it('should be created', inject([RightService], (service: RightService) => {
    expect(service).toBeTruthy();
  }));
});
