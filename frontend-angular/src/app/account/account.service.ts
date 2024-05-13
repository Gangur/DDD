import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, ReplaySubject, map, of } from 'rxjs';
import { Client, LoginDto, RegisterDto, UserDto } from '../api/http-client';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private currentUserSource = new ReplaySubject<UserDto | null>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private client: Client, private router: Router)
  {
  }

  loadCurrentUser() {
    const token = localStorage.getItem('token');
    if (token === null) {
      this.currentUserSource.next(null);
      return of(null)
    }

    return this.client.v1AuthGetCurrentUser().pipe(
      map(user => {
        if (user) {
          localStorage.setItem('token', user.token!);
          this.currentUserSource.next(user);
          return user;
        }
        else
          return null;
    }));
  }

  login(login: string, password: string) {
    const loginRequest = {
      login: login,
      password: password
    } as LoginDto

    return this.client.v1AuthLogin(loginRequest).pipe(map(user => {
      localStorage.setItem('token', user.token!);
      this.currentUserSource.next(user);
      return user;
    }));
  }

  register(email: string, name: string, password: string) {
    const registerRequest = {
      email: email,
      displayName: name,
      password: password,
    } as RegisterDto

    return this.client.v1AuthRegister(registerRequest).pipe(map(user => {
      localStorage.setItem('token', user.token!);
      this.currentUserSource.next(user);
      return user;
    }));
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExists(email: string) {
    return this.client.v1AuthCheckEmail(email)
  }
}
