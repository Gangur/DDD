import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError } from 'rxjs';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = localStorage.getItem('token');
    if (token) {
      request = request.clone({ headers: request.headers.set('Authorization', `Bearer ${token}`) });
    }

    return next.handle(request);
  }
}
