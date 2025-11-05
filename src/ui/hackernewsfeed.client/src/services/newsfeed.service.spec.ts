import { TestBed } from '@angular/core/testing';
import { NewsfeedService } from './newsfeed.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { StoryModel } from '../abstractions';

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
    let story1: StoryModel = {
      id: 1,
      title: 'Title 1',
      url: 'www.story1.com'
    };
    let story2: StoryModel = {
      id: 2,
      title: 'Title 2',
      url: 'www.story2.com'
    };
    const mockResult = {
      stories: [
        story1,
        story2
      ],
      total: 2
    };

    service.getPagination(0, 10).subscribe(result => {
      console.log(result);
      expect(result).toBeTruthy;
      expect(result.stories).toBeTruthy();
      expect(result.stories.length).toEqual(2);
      expect(result.total).toEqual(2);
    });

    const req = httpMock.expectOne('http://localhost:5014/api/story?pageIndex=0&pageSize=10');
    expect(req.request.method).toBe('GET');
    req.flush(mockResult);
  });
});
