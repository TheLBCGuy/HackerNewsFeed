import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { StoryModel, GetStoryResponse } from '../abstractions';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NewsfeedService {
  constructor(private http: HttpClient) {}
  url: string = '/api/story';
  searchUrl: string = this.url + '/search';
  list: StoryModel[] = [];
  listCount: number = 0;

  getPagination(pageIndex: number, pageSize: number): Observable<GetStoryResponse> {
    const params = new HttpParams()
      .set('pageIndex', pageIndex.toString())
      .set('pageSize', pageSize.toString());
    return this.http.get<GetStoryResponse>(this.url, { params });
  }

  getSearchPagination(searchTerm: string, pageIndex: number, pageSize: number): Observable<GetStoryResponse> {
    const params = new HttpParams()
      .set('searchTerm', searchTerm.toString())
      .set('pageIndex', pageIndex.toString())
      .set('pageSize', pageSize.toString());
    return this.http.get<GetStoryResponse>(this.searchUrl, { params });
  }
}
