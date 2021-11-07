import { catchError, map } from 'rxjs/operators';
import { AuthService } from './../../services/auth.service';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/internal/operators/tap';

@Injectable({
  providedIn: 'root',
})
export class EmailConfirmedGuardService implements CanActivate {
  constructor(private _authService: AuthService, private _router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    const token = String(route.queryParams['token']);
    const email = String(route.queryParams['email']);
    if (!token || !email) return this.getObervableWithRedirect();

    return this._authService.confirmEmail<boolean>(token, email).pipe(
      map(() => true),
      catchError((err) => this.getObervableWithRedirect())
    );
  }

  private getObervableWithRedirect(): Observable<boolean> {
    return of(false).pipe(tap(() => this._router.navigateByUrl('login')));
  }
}
