import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { AddUserRequest } from '../models/add-user-request.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  usersUrl: string = 'https://localhost:7111/api/users';

  addUser(model: AddUserRequest): Observable<void> {
    return this.http.post<void>(`${this.usersUrl}`, model)
  }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.usersUrl}`);
  }

}
