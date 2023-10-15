import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { Category } from '../models/category.model';
@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(private http: HttpClient) {}

  addCategory(model: AddCategoryRequest): Observable<void> {
    return this.http.post<void>(
      'https://localhost:7111/api/categories',
      model
    );
  }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(
      'https://localhost:7111/api/categories'
    );
  }
}




