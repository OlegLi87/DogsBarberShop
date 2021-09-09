import { Inject, Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  RouterStateSnapshot,
} from '@angular/router';
import {
  UserStream,
  USER_STREAM,
} from '../../infastructure/di_providers/userStream.provider';

@Injectable({
  providedIn: 'root',
})
export class MainGuard implements CanActivate {
  constructor(@Inject(USER_STREAM) private userStream$: UserStream) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    const urlSegments = route.url;
    const user = this.userStream$.value;

    const index = urlSegments.findIndex((url) => url.path === 'login');
    if (index !== -1 && user) return false;
    if (state.url == '' && !user) return false;

    return true;
  }
}
