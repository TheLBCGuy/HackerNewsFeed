import { Component, OnInit } from '@angular/core';
import { NewsfeedService } from '../../services/newsfeed.service';

@Component({
  selector: 'app-newsfeed',
  standalone: false,
  templateUrl: './newsfeed.component.html',
  styleUrl: './newsfeed.component.css'
})
export class NewsfeedComponent implements OnInit {
  constructor(
    public service: NewsfeedService
  ) {}

  ngOnInit(): void {
    this.service.retrieveList();
  }
}
