import { Inject, Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanLoad,
  Route,
  RouterStateSnapshot,
  UrlSegment,
} from '@angular/router';
import {
  UserStream,
  USER_STREAM,
} from 'src/app/dependencyInjection/tokens/userStream.diToken';

@Injectable({ providedIn: 'root' })
export class LoginAndMainPageGuardService implements CanActivate, CanLoad {
  constructor(@Inject(USER_STREAM) private _userStream$: UserStream) {}

  canLoad(route: Route, segments: UrlSegment[]): boolean {
    const user = this._userStream$.value;
    if (user) return false;
    return true;
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    const user = this._userStream$.value;

    if (!user) return false;
    return true;
  }
}
