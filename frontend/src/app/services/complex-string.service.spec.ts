import { TestBed, inject } from '@angular/core/testing';

import { ComplexStringService } from './complex-string.service';

describe('Services\complexStringService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ComplexStringService]
    });
  });

  it('should be created', inject([ComplexStringService], (service: ComplexStringService) => {
    expect(service).toBeTruthy();
  }));
});
