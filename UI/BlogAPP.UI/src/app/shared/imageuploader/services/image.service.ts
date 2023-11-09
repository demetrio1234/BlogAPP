import { Injectable } from '@angular/core';
import { Observable, Subscriber } from 'rxjs';
import { BlogImage } from '../models/blog-image.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  blogItemsUrl: string = 'https://localhost:7111/api/images';

  //imageSubscriber? : Subscriber;

  constructor(private http: HttpClient) { }

  uploadImage(file: File, fileName: string, title: string): Observable<BlogImage> {
    const formData = new FormData();
    formData.append('file', file);
    formData.append('fileName', fileName);
    formData.append('title', title)

    return this.http.post<BlogImage>(this.blogItemsUrl, formData);

  }

  getAllImages(){
    return this.http.get<BlogImage[]>(this.blogItemsUrl);
  }
}