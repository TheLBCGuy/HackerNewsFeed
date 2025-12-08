import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { of } from 'rxjs';
import { By } from '@angular/platform-browser';

import { NewsfeedService } from '../../services/newsfeed.service';
import { NewsfeedComponent } from './newsfeed.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { mockResult } from '../../test.variables';

describe('NewsfeedComponent', () => {
  let component: NewsfeedComponent;
  let fixture: ComponentFixture<NewsfeedComponent>;

  beforeEach(async () => {
    const storyServiceSpy = jasmine.createSpyObj<NewsfeedService>(['getPagination']);
    storyServiceSpy.getPagination.and.callFake(function () {
      return of(mockResult)
    });

    await TestBed.configureTestingModule({
      declarations: [NewsfeedComponent],
      imports: [MatPaginatorModule],
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
    tick();
    fixture.detectChanges();
    expect(component).toBeTruthy();
    console.log(fixture.debugElement.query(By.css('h4')));
    let anchors = fixture.debugElement.queryAll(By.css('h4'));
    console.log(anchors);
    expect(anchors[0].nativeNode.textContent).toEqual("Title 1");
    expect(anchors[1].nativeNode.textContent).toEqual("Title 2");
  }));
});
