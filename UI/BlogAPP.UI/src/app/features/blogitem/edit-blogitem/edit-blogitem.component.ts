import { Component, OnDestroy, OnInit } from '@angular/core';
import { BlogitemService } from '../services/blogitem.service';
import { BlogItem } from '../models/blogitem.model';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { EditBlogItemRequest } from '../models/edit-blogitem-request.model';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';

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
  categories$?: Category[];

  constructor(private blogItemService: BlogitemService,
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private router: Router) {

  }

  ngOnInit(): void {
    this.paramSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        if (this.id) {
          this.editBlogItemSubscription = this.blogItemService
            .getBlogItemById(this.id)
            .subscribe({
              next: (response) => {
                this.blogItem = response;
                console.log(response);
                console.log(this.blogItem);
                console.log("");
              },
            });
        }
      },
    });

    this.categoryService.getCategories().subscribe({
      next: (response) => {
        this.categories$ = response;
      },
    });

  }

  onFormSubmit(): void {

    const editBlogItemRequest: EditBlogItemRequest = {

      title: this.blogItem?.title ?? '',
      shortDescription: this.blogItem?.shortDescription ?? '',
      content: this.blogItem?.content ?? '',
      featuredImageUrl: this.blogItem?.featuredImageUrl ?? '',
      urlHandle: this.blogItem?.urlHandle ?? '',
      publishedDate: this.blogItem?.publishedDate ?? new Date,
      author: this.blogItem?.author ?? '',
      isVisible: this.blogItem?.isVisible ?? false,
      categories: this.blogItem?.categories ?? []
    };

    if (this.id) {
      this.blogItemService.editBlogItem(this.id, editBlogItemRequest)
        .subscribe({
          next: (response) => {
            console.log(response);
            this.router.navigateByUrl('/admin/blogItems');
          },
        });
    }
  }

  ngOnDestroy(): void {
    this.paramSubscription?.unsubscribe();
    this.editBlogItemSubscription?.unsubscribe();
  }
}
