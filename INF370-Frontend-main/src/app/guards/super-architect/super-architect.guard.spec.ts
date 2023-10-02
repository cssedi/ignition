import { TestBed } from '@angular/core/testing';

import { SuperArchitectGuard } from './super-architect.guard';

describe('SuperArchitectGuard', () => {
  let guard: SuperArchitectGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(SuperArchitectGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
