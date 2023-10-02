import { TestBed } from '@angular/core/testing';

import { HelpDashService } from './help-dash.service';

describe('HelpDashService', () => {
  let service: HelpDashService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HelpDashService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
