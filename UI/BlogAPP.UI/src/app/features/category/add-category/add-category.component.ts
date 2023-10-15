import { Component, OnDestroy } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { CategoryService } from '../services/category.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent implements OnDestroy {

  model: AddCategoryRequest;

  private subscription: Subscription = new Subscription;

  constructor(private categoryService: CategoryService) {
    this.model = {
      Name: '',
      UrlHandle: ''
    }
  }


  onFormSubmit() {
    this.subscription = this.categoryService.addCategory(this.model)
      .subscribe({
      next: (response) => {

      }, error(err) {

      },
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
