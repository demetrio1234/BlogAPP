import { Component, OnInit } from '@angular/core';

import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css'],
})
export class CategoryListComponent implements OnInit {
  categories: Category[] = [];

  constructor(private categoryService: CategoryService) {}

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe({
      next: (response: Category[]) => {
        this.categories = response;
        console.log(response[0].id);
        console.log(this.categories[0].id);
        console.log(this.categories[0].name);
        console.log(this.categories[0].urlHandle);
      },
    });
  }
}
