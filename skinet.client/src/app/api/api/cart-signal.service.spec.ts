import { TestBed } from '@angular/core/testing';

import { CartSignalService } from './cart-signal.service';

describe('CartSignalService', () => {
  let service: CartSignalService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CartSignalService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
