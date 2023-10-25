import { Component, OnDestroy, OnInit } from '@angular/core';
import { BlogitemService } from '../services/blogitem.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { BlogItem } from '../models/blogitem.model';
import { EditBlogItemRequest } from '../models/edit-blogitem-request.model';


@Component({
  selector: 'app-edit-blogitem',
  templateUrl: './edit-blogitem.component.html',
  styleUrls: ['./edit-blogitem.component.css']
})
export class EditBlogitemComponent implements OnInit, OnDestroy {

  blogItem?: BlogItem;

  id: string | null = null;

  paramSubscription?: Subscription;
  editBlogItemSubscription?: Subscription;


  constructor(private blogitemService: BlogitemService, private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.paramSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        if (this.id) {
          this.editBlogItemSubscription = this.blogitemService.getBlogItem(this.id).subscribe({
            next: (response) => {
              this.blogItem = response;
            },
          });
        }
      },
    });
  }

  onFormSubmit(): void {
    const EditBlogItemRequest: EditBlogItemRequest = {
      title: this.blogItem?.title ?? '',
      shortdescription: this.blogItem?.shortdescription ?? '',
      content: this.blogItem?.content ?? '',
      featuredimageurl: this.blogItem?.featuredimageurl ?? '',
      urlhandle: this.blogItem?.urlhandle ?? '',
      publisheddate: this.blogItem?.publisheddate ?? new Date,
      author: this.blogItem?.author ?? '',
      isvisible: this.blogItem?.isvisible ?? '',
    }

    if (this.id) {
      this.blogitemService.editBlogItem(this.id, EditBlogItemRequest)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/blogitems');
          },
        });
    }
  }


  onDelete(): void {
    if (this.id) {
      this.blogitemService.deleteBlogItem(this.id).subscribe({
        next: (response) => {
          console.log(response);
          this.router.navigateByUrl('/admin/blogitems');
        },
      });
    }
  }

  ngOnDestroy(): void {
    this.paramSubscription?.unsubscribe();
    this.editBlogItemSubscription?.unsubscribe();
  }

}
