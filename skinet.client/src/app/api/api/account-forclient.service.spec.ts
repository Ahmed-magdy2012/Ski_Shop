import { TestBed } from '@angular/core/testing';

import { AccountForclientService } from './account-forclient.service';

describe('AccountForclientService', () => {
  let service: AccountForclientService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AccountForclientService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
