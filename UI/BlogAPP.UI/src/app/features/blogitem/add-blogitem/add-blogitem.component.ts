import { Component, OnDestroy, OnInit } from '@angular/core';
import { AddBlogItemRequest } from '../models/add-blogitem-request.model';
import { BlogitemService } from '../services/blogitem.service';
import { Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';

@Component({
  selector: 'app-add-blogitem',
  templateUrl: './add-blogitem.component.html',
  styleUrls: ['./add-blogitem.component.css']
})
export class AddBlogitemComponent implements OnInit, OnDestroy {

  model: AddBlogItemRequest;

  private subscription: Subscription = new Subscription;

  constructor(private blogItemService: BlogitemService,
    private router: Router,
    private categoryService: CategoryService) {
    this.model = {
      title: '',
      shortDescription: '',
      content: '',
      featuredImageUrl: '',
      urlHandle: '',
      publishedDate: new Date(),
      author: '',
      isVisible: true,
      categories: [],
    }
  }

  categories$?: Observable<Category[]>;
  isImageSelectorVisible:boolean=false;
  
  ngOnInit(): void {

    this.categories$ = this.categoryService.getCategories();
    this.isImageSelectorVisible = false;

  }

  onFormSubmit(): void {

    console.log(this.model);

    this.blogItemService.addBlogItem(this.model)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/blogItems');
        },
      });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  openImageSelector(): void {
    this.isImageSelectorVisible = true;
  }

  closeImageSelector(){
    this.isImageSelectorVisible = false;
  }

}
