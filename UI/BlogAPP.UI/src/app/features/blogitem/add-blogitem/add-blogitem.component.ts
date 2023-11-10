import { Component, OnDestroy, OnInit } from '@angular/core';
import { AddBlogItemRequest } from '../models/add-blogitem-request.model';
import { BlogitemService } from '../services/blogitem.service';
import { Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';
import { ImageuploaderComponent } from 'src/app/shared/imageuploader/imageuploader.component';
import { ImageService } from 'src/app/shared/imageuploader/services/image.service';
import { BlogItem } from '../models/blogitem.model';

@Component({
  selector: 'app-add-blogitem',
  templateUrl: './add-blogitem.component.html',
  styleUrls: ['./add-blogitem.component.css']
})
export class AddBlogitemComponent implements OnInit, OnDestroy {

  id: string | null = null;
  selectedCategories?: string[];
  model: AddBlogItemRequest;
  blogItem?: BlogItem;
  isImageSelectorVisible: boolean = false;

  editBlogItemSubscription?: Subscription;
  subscription: Subscription = new Subscription;
  imageSubscription?: Subscription;

  categories$?: Observable<Category[]>;

  constructor(private blogItemService: BlogitemService,
    private router: Router,
    private categoryService: CategoryService,
    private imageService: ImageService) {
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

  ngOnInit(): void {

    this.categories$ = this.categoryService.getCategories();
    this.isImageSelectorVisible = false;

    this.imageSubscription = this.imageService.onSelectedImage().subscribe({
      next: (response) => {
        if (this.model) {
          this.model.featuredImageUrl = response.url;
          this.isImageSelectorVisible = false;
        }

      },
    });

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
    this.imageSubscription?.unsubscribe();
  }

  openImageSelector(): void {
    this.isImageSelectorVisible = true;
  }

  closeImageSelector() {
    this.isImageSelectorVisible = false;
  }

}
