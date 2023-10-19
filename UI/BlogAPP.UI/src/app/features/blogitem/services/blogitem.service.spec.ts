import { TestBed } from '@angular/core/testing';

import { BlogitemService } from './blogitem.service';

describe('BlogitemService', () => {
  let service: BlogitemService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BlogitemService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
