import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { Category } from '../models/category.model';
import { EditCategoryRequest } from '../models/edit-category-request.model';
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

  getCategoryById(id: string): Observable<Category> {
    return this.http.get<Category>(`${this.categoriesUrl}/${id}`);
  }

  editCategory(id: string, editCategoryRequest: EditCategoryRequest): Observable<Category> {

    return this.http.put<Category>(`${this.categoriesUrl}/${id}`, editCategoryRequest);

  }

  deleteCategory(id:string):Observable<Category>{
    return this.http.delete<Category>(`${this.categoriesUrl}/${id}`);
  }


}