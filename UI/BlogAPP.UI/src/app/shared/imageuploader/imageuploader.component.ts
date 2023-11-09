import { Component, OnDestroy, OnInit } from '@angular/core';
import { ImageService } from './services/image.service';
import { Router } from '@angular/router';
import { Observable, Subscriber } from 'rxjs';
import { BlogImage } from './models/blog-image.model';

@Component({
  selector: 'app-imageuploader',
  templateUrl: './imageuploader.component.html',
  styleUrls: ['./imageuploader.component.css']
})
export class ImageuploaderComponent implements OnInit, OnDestroy {

  private file?: File;

  fileName?: string;
  title?: string;
  url?: string;

  imageSubscriber?: Subscriber<BlogImage>;

  imageList$?: Observable<BlogImage[]>;

  constructor(private imageService: ImageService, private router: Router) { }

  ngOnInit(): void {
    this.getImages();
  }

  ngOnDestroy(): void {
    throw new Error('');
  }

  onFileChange(event: Event): void {
    const element = event.currentTarget as HTMLInputElement;
    this.file = element.files?.[0];
  }

  uploadImage(): void {

    if (this.file &&
      this.fileName && this.fileName != '' &&
      this.title && this.title != '') {

      this.imageService.uploadImage(this.file, this.fileName, this.title).subscribe({
        next: (response) => {
          //this.imageSubscriber = response;
          console.log(response);
          this.getImages();
          //this.router.navigateByUrl('/admin/images');
        },
      });

    }

  }

  private getImages(){
    this.imageList$ = this.imageService.getAllImages();
  }
}
