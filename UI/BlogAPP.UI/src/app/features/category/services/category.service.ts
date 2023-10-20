import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { Category } from '../models/category.model';
@Injectable({
  providedIn: 'root',
})
export class CategoryService {

  categoriesUrl: string = 'https://localhost:7111/api/categories';

  constructor(private http: HttpClient) { }

  addCategory(model: AddCategoryRequest): Observable<void> {
    return this.http.post<void>(
      `${this.categoriesUrl}`,
      model
    );
  }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.categoriesUrl}`);
  }
}




