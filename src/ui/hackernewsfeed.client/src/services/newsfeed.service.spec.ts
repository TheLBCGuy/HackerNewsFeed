import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { NewsfeedService } from './newsfeed.service';
import { mockResult } from '../test.variables';

describe('NewsfeedService', () => {
  let service: NewsfeedService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ]
    });
    service = TestBed.inject(NewsfeedService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should return stories', () => {
    service.getPagination(0, 10).subscribe(result => {
      console.log(result);
      expect(result).toBeTruthy;
      expect(result.stories).toBeTruthy();
      expect(result.stories.length).toEqual(2);
      expect(result.total).toEqual(2);
    });

    const req = httpMock.expectOne('/api/story?pageIndex=0&pageSize=10');
    expect(req.request.method).toBe('GET');
    req.flush(mockResult);
  });

  afterEach(() => {
    httpMock.verify();
  });
});
