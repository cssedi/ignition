import { TestBed } from '@angular/core/testing';

import { ArchitectGuard } from './architect.guard';

describe('ArchitectGuard', () => {
  let guard: ArchitectGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(ArchitectGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
