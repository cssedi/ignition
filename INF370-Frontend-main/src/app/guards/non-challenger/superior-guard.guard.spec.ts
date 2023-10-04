import { TestBed } from '@angular/core/testing';

import { SuperiorGuardGuard } from './superior-guard.guard';

describe('SuperiorGuardGuard', () => {
  let guard: SuperiorGuardGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(SuperiorGuardGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
