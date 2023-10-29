import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BlogItem } from '../models/blogitem.model';
import { BlogitemService } from '../services/blogitem.service';

@Component({
  selector: 'app-blogitem-list',
  templateUrl: './blogitem-list.component.html',
  styleUrls: ['./blogitem-list.component.css']
})
export class BlogitemListComponent implements OnInit {

  blogItems$?: Observable<BlogItem[]>;

  constructor(private blogItemService: BlogitemService) {

  }
  ngOnInit(): void {
    this.blogItems$ = this.blogItemService.getAllBlogItems();
  }

}
