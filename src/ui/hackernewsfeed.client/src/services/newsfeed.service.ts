import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../environments/environment.development';
import { StoryModel } from '../abstractions/story-model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NewsfeedService {
  constructor(private http: HttpClient) {}
  url: string = environment.apiBaseUrl + '/Story';
  list: StoryModel[] = [];

  refreshList() {
    this.http.get(this.url, {
      headers: { 'Access-Control-Allow-Origin': 'True' }
    }).subscribe({
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
}
