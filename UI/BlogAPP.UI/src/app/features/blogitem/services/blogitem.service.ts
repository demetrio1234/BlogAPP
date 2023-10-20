import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BlogItem } from '../models/blogitem.model';
import { AddBlogItemRequest } from '../models/add-blogitem-request.model';

@Injectable({
  providedIn: 'root'
})
export class BlogitemService {

  blogItemsUrl: string = 'https://localhost:7111/api/BlogPosts';

  constructor(private http: HttpClient) { }

  addBlogItem(model: AddBlogItemRequest): Observable<void> {
    return this.http.post<void>(`${this.blogItemsUrl}`,
      model
    );
  }

  getAllBlogItems(): Observable<BlogItem[]> {
    return this.http.get<BlogItem[]>(`${this.blogItemsUrl}`);
  }
}
