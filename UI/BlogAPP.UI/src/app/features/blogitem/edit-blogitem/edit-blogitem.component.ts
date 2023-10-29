import { Component, OnDestroy, OnInit } from '@angular/core';
import { BlogitemService } from '../services/blogitem.service';
import { BlogItem } from '../models/blogitem.model';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-edit-blogitem',
  templateUrl: './edit-blogitem.component.html',
  styleUrls: ['./edit-blogitem.component.css']
})
export class EditBlogitemComponent implements OnInit, OnDestroy {

  id: string | null = null;

  paramSubscription?: Subscription;
  editBlogItemSubscription?: Subscription;

  blogItem?: BlogItem;

  constructor(private blogItemService: BlogitemService,
    private route: ActivatedRoute,
    private router: Router) {

  }

  ngOnInit(): void {
    this.paramSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        if (this.id) {
          this.editBlogItemSubscription = this.blogItemService
            .getBlogItemById(this.id).subscribe({
              next: (response) => {
                this.blogItem = response;
              },
            });
        }
      },
    });

  }

  ngOnDestroy(): void {
    this.paramSubscription?.unsubscribe();
    this.editBlogItemSubscription?.unsubscribe();
  }
}
