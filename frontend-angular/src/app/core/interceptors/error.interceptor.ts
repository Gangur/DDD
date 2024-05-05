import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error) {
          const text = new Response(error.error)
            .text()
            .then(errorText => {
              switch (error.status) {
                case 400:
                  try {
                    const modelStateErrors: string[] = [];
                    const data = JSON.parse(errorText);
                    for (const key in data.errors) {
                      modelStateErrors.push(data.errors[key]);
                    }
                    this.toastr.error(modelStateErrors.join('; ') + ';', 'Validation problems!')
                  } catch (e) {
                    this.toastr.error(errorText, error.status.toString());
                  }
                  break;
                case 401:
                  this.toastr.error(errorText, error.status.toString());
                  break;
                case 404:
                  this.router.navigateByUrl('/not-found');
                  break;
                case 500:
                  const navigationExras: NavigationExtras = {
                    state: { error: { error: errorText, trace: error.message } }
                  };
                  this.router.navigateByUrl('/server-error', navigationExras);
                  break;
              }
            });
        }

        return throwError(() => new Error(error.message))
      })
    );
  }
}
