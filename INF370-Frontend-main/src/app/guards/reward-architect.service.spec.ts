import { TestBed } from '@angular/core/testing';

import { RewardArchitectService } from './reward-architect.service';

describe('RewardArchitectService', () => {
  let service: RewardArchitectService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RewardArchitectService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
