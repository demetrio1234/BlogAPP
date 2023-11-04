import { Component, OnDestroy, OnInit } from '@angular/core';
import { BlogitemService } from '../services/blogitem.service';
import { BlogItem } from '../models/blogitem.model';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
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

  routeSubscription?: Subscription;
  getBlogItemSubscription?: Subscription;
  editBlogItemSubscription?: Subscription;

  blogItem?: BlogItem;
  categories$?: Observable<Category[]>;
  selectedCategories?: string[];

  constructor(
    private blogItemService: BlogitemService,
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private router: Router) {
  }

  ngOnInit(): void {
    this.categories$ = this.categoryService.getCategories();

    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        if (this.id) {
          this.getBlogItemSubscription = this.blogItemService
            .getBlogItemById(this.id)
            .subscribe({
              next: (response) => {
                this.blogItem = response;
                this.selectedCategories = this.blogItem.categories.map(x => x.id);
              },
            });

            
        }
      },
    });


  }

  onFormSubmit(): void {

    if (this.blogItem && this.id) {

      var editBlogItemRequest: EditBlogItemRequest = {
        title: this.blogItem?.title,
        shortDescription: this.blogItem?.shortDescription,
        content: this.blogItem?.content ,
        featuredImageUrl: this.blogItem?.featuredImageUrl,
        urlHandle: this.blogItem?.urlHandle,
        publishedDate: this.blogItem?.publishedDate,
        author: this.blogItem?.author,
        isVisible: this.blogItem?.isVisible,
        categories: []
      };

      this.blogItem.categories.forEach(element => {
        editBlogItemRequest.categories.push(element.id)
      });

      this.editBlogItemSubscription = this.blogItemService.editBlogItem(this.id, editBlogItemRequest)
        .subscribe({
          next: (response) => {
            console.log(response);
            this.router.navigateByUrl('/admin/blogItems');
          },
        });

    }

  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.getBlogItemSubscription?.unsubscribe();
    this.editBlogItemSubscription?.unsubscribe();
  }

  deleteBlogItemById(id: string): void {
    if (id) {
      this.blogItemService.deleteBlogItem(id).subscribe({
        next: (response) => {
          console.log(response);
          this.router.navigateByUrl('/admin/blogItems');
        },
      });
    }
  }
}
