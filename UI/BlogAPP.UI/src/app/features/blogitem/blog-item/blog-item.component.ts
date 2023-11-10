import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-blog-item',
  templateUrl: './blog-item.component.html',
  styleUrls: ['./blog-item.component.css']
})
export class BlogItemComponent implements OnInit {

  //#region Variables
  url: string | null = null;

  //#endregion Variables

  //#region Subscriptios
  urlSubscription? : Subscription;
  //#endregion Subscriptions

  constructor(private route: ActivatedRoute) {

  }
  ngOnInit(): void {

   this.urlSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        params.get('urlHandle')
      },
    });




  }

}
