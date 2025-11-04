import { Component, OnInit, ViewChild } from '@angular/core';
import { NewsfeedService } from '../../services/newsfeed.service';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { StoryModel } from '../../abstractions/story-model';

@Component({
  selector: 'app-newsfeed',
  standalone: false,
  templateUrl: './newsfeed.component.html',
  styleUrl: './newsfeed.component.css'
})
export class NewsfeedComponent implements OnInit {
  totalStories = 0;
  displayedColumns: string[] = ['id', 'title', 'by'];
  stories: StoryModel[] = [];
  pageSize = 10;
  pageIndex = 0;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    public service: NewsfeedService
  ) { }

  ngOnInit(): void {
    this.loadStories();

  }

  loadStories(): void {
    this.service.getPagination(this.pageIndex, this.pageSize).subscribe({
      next: (data) => {
        console.log(data.total);
        console.log(data.stories);
        this.stories = data.stories;
        this.totalStories = data.total;
      },
      error: (err) => {
        console.error('Error fetching users:', err);
      },
    });
  }

  onPageChange(event: PageEvent): void {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadStories();
  }
}
