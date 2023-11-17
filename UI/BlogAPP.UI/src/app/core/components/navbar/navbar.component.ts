import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscriber, Subscription } from 'rxjs';
import { AuthService } from 'src/app/features/auth/services/auth.service';
import { User } from 'src/app/features/user/models/user.model';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit, OnDestroy {


  constructor(private authService: AuthService) {

  }

  user?: User;
  ngOnInit(): void {
   this.userSubscription =  this.authService.user().subscribe({
      next: (response) => {

        this.user = response;
        console.log(this.user);

      },
    });
  }

  userSubscription?:Subscription;
  ngOnDestroy(): void {
    this.userSubscription?.unsubscribe();
  }
}
