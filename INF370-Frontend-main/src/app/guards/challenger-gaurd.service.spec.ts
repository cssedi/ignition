import { TestBed } from '@angular/core/testing';

import { ChallengerGaurdService } from './challenger-gaurd.service';

describe('ChallengerGaurdService', () => {
  let service: ChallengerGaurdService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ChallengerGaurdService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
