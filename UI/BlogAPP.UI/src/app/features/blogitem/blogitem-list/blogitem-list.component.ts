import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BlogItem } from '../models/blogitem.model';
import { BlogitemService } from '../services/blogitem.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-blogitem-list',
  templateUrl: './blogitem-list.component.html',
  styleUrls: ['./blogitem-list.component.css']
})
export class BlogitemListComponent implements OnInit {

  blogItems$?: Observable<BlogItem[]>;

  constructor(
    private blogItemService: BlogitemService,
    private router: Router) {

  }
  ngOnInit(): void {
    this.blogItems$ = this.blogItemService.getAllBlogItems();
  }

  deleteBlogItemById(id: string): void {
    if (id) {
      this.blogItemService.deleteBlogItem(id).subscribe({
        next: (response) => {
          console.log(response);
          //this.router.navigateByUrl('/admin/blogItems');
          window.location.reload();
        },
      });
    }
  }


}
