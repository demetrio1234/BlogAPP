import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { BlogitemService } from '../services/blogitem.service';
import { BlogItem } from '../models/blogitem.model';
import { Category } from '../../category/models/category.model';
import { ImageService } from 'src/app/shared/imageuploader/services/image.service';

@Component({
  selector: 'app-blog-item',
  templateUrl: './blog-item.component.html',
  styleUrls: ['./blog-item.component.css']
})
export class BlogItemComponent implements OnInit, OnDestroy {

  //#region Variables
  urlHandle: string | null = null;
  //#endregion Variables

  //#region Models
  //#endregion Models

  //#region Subscriptions
  getBlogItemSubscription?: Subscription;
  //#endregion Subscriptions

  //#region Observables
  blogItem$?: Observable<BlogItem>;
  //#endregion Observables

  constructor(private route: ActivatedRoute,
    private blogItemService: BlogitemService,
  ) {

  }
  ngOnDestroy(): void {
    this.getBlogItemSubscription?.unsubscribe();
  }

  ngOnInit(): void {

    this.route.paramMap.subscribe({
      next: (params) => {
        this.urlHandle = params.get('urlHandle');

        if (this.urlHandle) {
          this.blogItem$ = this.blogItemService.getBlogItemByUrlHandle(this.urlHandle);
        }
      },
    });
  }
}
