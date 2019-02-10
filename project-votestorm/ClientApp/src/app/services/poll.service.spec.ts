import { TestBed } from '@angular/core/testing';

import { PollService } from './poll.service';
import {HttpClientTestingModule} from '@angular/common/http/testing';

describe('PollService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule
    ]
  }));

  it('should be created', () => {
    const service: PollService = TestBed.get(PollService);
    expect(service).toBeTruthy();
  });
});
