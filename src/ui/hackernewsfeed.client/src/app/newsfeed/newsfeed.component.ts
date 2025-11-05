import { Component, OnInit, ViewChild, signal } from '@angular/core';
import { NewsfeedService } from '../../services/newsfeed.service';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { StoryModel } from '../../abstractions';

@Component({
  selector: 'app-newsfeed',
  standalone: false,
  templateUrl: './newsfeed.component.html',
  styleUrl: './newsfeed.component.css'
})
export class NewsfeedComponent implements OnInit {
  
  stories: StoryModel[] = [];
  totalStories = signal<number>(0);
  pageSize = signal<number>(15);
  pageIndex = signal<number>(0);
  searchTerm = signal<string>("");
  star_score_minimum: number = 500;

  customPaginatorStyle = 'custom-paginator-style';

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    public service: NewsfeedService
  ) { }

  ngOnInit(): void {
    this.loadStories();
  }

  loadStories(): void {
    this.service.getPagination(this.pageIndex(), this.pageSize()).subscribe({
      next: (data) => {
        console.log(data.total);
        console.log(data.stories);
        this.stories = data.stories;
        this.totalStories.set(data.total);
      },
      error: (err) => {
        console.error('Error fetching users:', err);
      },
    });
  }

  searchStories(): void {
    console.log("search button clicked");
    console.log("searchTerm is " + this.searchTerm());
    this.service.getSearchPagination(this.searchTerm(), this.pageIndex(), this.pageSize()).subscribe({
      next: (data) => {
        console.log(data.total);
        console.log(data.stories);
        this.stories = data.stories;
        this.totalStories.set(data.total);
      },
      error: (err) => {
        console.error('Error fetching users:', err);
      },
    });
  }

  clearSearch() {
    this.searchTerm.set("");
    this.pageIndex.set(0);
    console.log("pageIndex is " + this.pageIndex().toString());
    console.log("clear button clicked");
    this.loadStories();
  }

  searchButtonClicked() {
    if (this.searchTerm() == "") return;

    this.prepareForNewSearch();
  }

  searchFieldKeyUp(event: KeyboardEvent): void {
    if (event.key === 'Enter') {
      this.prepareForNewSearch();
    }
    console.log(this.searchTerm());
  }

  showGradeStar(story: StoryModel) {
    if (story.score == null) return false;
    return story.score >= this.star_score_minimum;
  }

  prepareForNewSearch() {
    this.pageIndex.set(0);
    console.log("pageIndex is " + this.pageIndex().toString());
    this.searchStories();
  }

  onPageChange(event: PageEvent): void {
    this.pageIndex.set(event.pageIndex);
    this.pageSize.set(event.pageSize);
    if (this.searchTerm() != "") {
      this.searchStories();
    }
    else {
      this.loadStories();
    }
  }
}
