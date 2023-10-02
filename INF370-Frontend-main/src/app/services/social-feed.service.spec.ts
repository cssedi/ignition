import { TestBed } from '@angular/core/testing';

import { SocialFeedService } from './social-feed.service';

describe('SocialFeedService', () => {
  let service: SocialFeedService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SocialFeedService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
