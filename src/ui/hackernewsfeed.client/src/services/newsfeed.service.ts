import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../environments/environment.development';
import { StoryModel } from '../abstractions';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NewsfeedService {
  constructor(private http: HttpClient) {}
  url: string = environment.apiBaseUrl + '/story';
  searchUrl: string = environment.apiBaseUrl + '/story/search';
  list: StoryModel[] = [];
  listCount: number = 0;

  refreshList() {
    this.http.get(this.url).subscribe({
      next: (res) => {
        this.list = res as StoryModel[];
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  getPagination(pageIndex: number, pageSize: number): Observable<{ stories: StoryModel[]; total: number }> {
    const params = new HttpParams()
      .set('pageIndex', pageIndex.toString())
      .set('pageSize', pageSize.toString());
    return this.http.get<{ stories: StoryModel[]; total: number }>(this.url, { params });
  }

  getSearchPagination(searchTerm: string, pageIndex: number, pageSize: number): Observable<{ stories: StoryModel[]; total: number }> {
    const params = new HttpParams()
      .set('searchTerm', searchTerm.toString())
      .set('pageIndex', pageIndex.toString())
      .set('pageSize', pageSize.toString());
    return this.http.get<{ stories: StoryModel[]; total: number }>(this.searchUrl, { params });
  }
}
