import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../environments/environment.development';
import { StoryModel } from '../abstractions/story-model';

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
        //console.log(res);
        this.list = res as StoryModel[];
        console.log(this.list);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
