import { Component, OnDestroy, OnInit } from '@angular/core';
import { AddBlogItemRequest } from '../models/add-blogitem-request.model';
import { BlogitemService } from '../services/blogitem.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscriber, Subscription } from 'rxjs';

@Component({
  selector: 'app-add-blogitem',
  templateUrl: './add-blogitem.component.html',
  styleUrls: ['./add-blogitem.component.css']
})
export class AddBlogitemComponent implements OnInit, OnDestroy{

  model: AddBlogItemRequest;
  
  private subscription: Subscription = new Subscription;

  constructor(private blogItemService: BlogitemService, private router: Router) {
    this.model = {
      title: '',
      shortDescription: '',
      content: '',
      featuredImageUrl: '',
      urlHandle: '',
      publishedDate: new Date(),
      author: '',
      isVisible: true,

    }

  }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  onFormSubmit(): void {
    this.subscription  = this.blogItemService.addBlogItem(this.model).subscribe({
      next: (response) => {
        this.router.navigateByUrl('/admin/blogItems');
        console.log(response);
      },
    });
  }

  
  ngOnDestroy(): void {
   this.subscription.unsubscribe();
  }
}
