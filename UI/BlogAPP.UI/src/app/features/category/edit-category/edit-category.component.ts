import { Component, OnDestroy, OnInit } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Category } from '../models/category.model';
import { EditCategoryRequest } from '../models/edit-category-request.model';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit, OnDestroy {

  id: string | null = null;

  paramSubscription?: Subscription;
  editCategorySubscription?: Subscription;

  category?: Category;

  constructor(private route: ActivatedRoute,
    private categoryService: CategoryService,
    private router: Router) {

  }

  ngOnInit(): void {
    this.paramSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        if (this.id) {
          this.editCategorySubscription = this.categoryService
            .getCategoryById(this.id)
            .subscribe({
              next: (response) => {
                this.category = response;
              },
            });
        }
      },
    });
  }

  onFormSubmit(): void {

    const editCategoryRequest: EditCategoryRequest = {
      name: this.category?.name ?? '',
      urlHandle: this.category?.urlHandle ?? ''
    };

    if (this.id) {
      this.categoryService.editCategory(this.id, editCategoryRequest)
        .subscribe({
          next: (response) => {
            console.log(response);
            this.router.navigateByUrl('/admin/categories');
          },
        });
    }
  }
  onDelete(): void {
    if (this.id) {
      this.categoryService.deleteCategory(this.id).subscribe({
        next: (response) => {
          console.log(response);
          this.router.navigateByUrl('/admin/categories');
        },
      });
    }
  }

  ngOnDestroy(): void {
    this.paramSubscription?.unsubscribe();
    this.editCategorySubscription?.unsubscribe();
  }
}
