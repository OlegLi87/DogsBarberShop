import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { ClassProvider, Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { delay, retryWhen, tap } from 'rxjs/operators';
import { UserStream, USER_STREAM } from './userStream.provider';

@Injectable()
class AppHttpInterceptor implements HttpInterceptor {
  constructor(@Inject(USER_STREAM) private _userStream$: UserStream) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (this._userStream$.value)
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${this._userStream$.value.jwtToken}`,
        },
      });

    return next.handle(req).pipe(
      delay(3000), // imitating network latency
      retryWhen((errors) => {
        let retryCount = 0;
        return errors.pipe(
          delay(500),
          tap((error: HttpErrorResponse) => {
            if (++retryCount > 3 || (error.status < 500 && error.status))
              throw error;
          })
        );
      })
    );
  }
}

export const httpInterceptorProvider: ClassProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: AppHttpInterceptor,
  multi: true,
};
