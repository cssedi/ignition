import { TestBed } from '@angular/core/testing';

import { DepartementChallengeService } from './departement-challenge.service';

describe('DepartementChallengeService', () => {
  let service: DepartementChallengeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DepartementChallengeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
