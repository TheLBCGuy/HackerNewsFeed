import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { of } from 'rxjs';
import { By } from '@angular/platform-browser';

import { NewsfeedService } from '../../services/newsfeed.service';
import { NewsfeedComponent } from './newsfeed.component';
import { StoryModel } from '../../abstractions';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { MatPaginatorModule } from '@angular/material/paginator';

describe('NewsfeedComponent', () => {
  let component: NewsfeedComponent;
  let fixture: ComponentFixture<NewsfeedComponent>;

  beforeEach(async () => {
    const storyServiceSpy = jasmine.createSpyObj<NewsfeedService>(['getPagination']);

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
    const mockData = {
      stories: [
        story1,
        story2
      ],
      total: 2
    };
    storyServiceSpy.getPagination.and.callFake(function () {
      return of(mockData)
    });

    await TestBed.configureTestingModule({
      declarations: [NewsfeedComponent],
      imports: [HttpClientModule, MatPaginatorModule],
      providers: [
        {
          provide: NewsfeedService,
          useValue: storyServiceSpy
        }
      ],
      schemas: [NO_ERRORS_SCHEMA]         // need this to ignore unknown elements like input's [(NgModel)] (Angular 19 thing?)
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewsfeedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the app', fakeAsync(() => {
    component.ngOnInit();
    //tick(1000);
    tick();
    fixture.detectChanges();
    expect(component).toBeTruthy();
    //console.log(fixture.debugElement);
    console.log(fixture.debugElement.query(By.css('h4')));
    let anchors = fixture.debugElement.queryAll(By.css('h4'));
    console.log(anchors);
    //let anchors = fixture.debugElement.query(By.css('a'));
    //expect(anchors.
    //expect().toBeTruthy();
    expect(anchors[0].nativeNode.textContent).toEqual("Title 1");
    expect(anchors[1].nativeNode.textContent).toEqual("Title 2");

  }));

});
