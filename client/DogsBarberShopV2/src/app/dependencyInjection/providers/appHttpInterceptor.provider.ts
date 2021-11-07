import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { ClassProvider, Inject, Injectable } from '@angular/core';
import { Observable, timer } from 'rxjs';
import { delay, delayWhen, retryWhen, tap } from 'rxjs/operators';
import { AppError } from 'src/app/models/appError';
import { UserStream, USER_STREAM } from '../tokens/userStream.diToken';

@Injectable()
export class AppHttpInterceptor implements HttpInterceptor {
  constructor(@Inject(USER_STREAM) private _userStream$: UserStream) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const user = this._userStream$.value;
    if (user) {
      req = req.clone({
        headers: req.headers.append('Authorization', `Bearer ${user.token}`),
      });
    }

    // only fore development,to simulate hetwork latency
    const delayDuration = req.url.includes('http') ? 5000 : 0;

    return next.handle(req).pipe(
      delay(delayDuration),
      retryWhen((errors) => {
        let retryCount = 0;
        let delay = 500;
        return errors.pipe(
          tap((err) => {
            if (retryCount++ > 4) throw err;
            if (err instanceof HttpErrorResponse)
              this.proccessHttpErrorResponse(err);
          }),
          delayWhen(() => {
            delay += 800;
            return timer(delay);
          })
        );
      })
    );
  }

  private proccessHttpErrorResponse(err: HttpErrorResponse): void {
    if (err.status === 0)
      throw new AppError('Failed to establish a network connection.');
    if (err.status < 500) throw new AppError(err.error.errors);
  }
}

export const appHttpInterceptorProvider: ClassProvider = {
  provide: HTTP_INTERCEPTORS,
  multi: true,
  useClass: AppHttpInterceptor,
};
