import { Injectable } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginResponse } from '../models/login-response.model';
import { HttpClient } from '@angular/common/http';
import { User } from '../../user/models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrlLogin: string = "https://localhost:7111/api/auth/login"

  constructor(private http: HttpClient) { }

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.baseUrlLogin}`, {
      email: request.email,
      password: request.password
    });
  }

  $user = new BehaviorSubject<User | undefined>(undefined); //it'll be of type User or undefined
  setUser(user: User): void {

    this.$user.next(user);

    localStorage.setItem('user-email', user.email);
    localStorage.setItem('user-roles', user.roles.join(','));

  }

  user(): Observable<User | undefined> {
    return this.$user.asObservable();
  }
}
