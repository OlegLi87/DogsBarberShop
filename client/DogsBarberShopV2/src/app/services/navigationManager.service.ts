import {
  APP_CONFIG_STREAM,
  AppConfigStream,
} from './../dependencyInjection/tokens/appConfig.diToken';
import { Inject, Injectable } from '@angular/core';
import { NavigationEnd, NavigationStart, Router } from '@angular/router';
import { filter, map, distinctUntilChanged } from 'rxjs/operators';
import {
  UserStream,
  USER_STREAM,
} from '../dependencyInjection/tokens/userStream.diToken';

@Injectable({
  providedIn: 'root',
})
export class NavigationManagerService {
  constructor(
    private _router: Router,
    @Inject(USER_STREAM) private _userStream: UserStream,
    @Inject(APP_CONFIG_STREAM) private _appConfigStream$: AppConfigStream
  ) {
    this.configureNavigation();
  }

  navigate(path: string, callback?: () => void) {
    const subs = this._router.events
      .pipe(filter((e) => e instanceof NavigationEnd))
      .subscribe(() => {
        subs.unsubscribe();
        if (callback) callback();
      });
    this._router.navigateByUrl(path);
  }

  // in case user stream emitted value on first navigaion (at app init),
  // if there is no active user the only paths that are allowed is login components paths otherwise the user will be redirected ti login page.
  // if there is active user,all paths are allowed except login paths in which case user will be redirected to main page.
  private configureNavigation(): void {
    this._userStream.pipe(distinctUntilChanged()).subscribe((user) => {
      const isFirstNavigation = !this._router.navigated;

      if (isFirstNavigation) {
        const subs = this._router.events
          .pipe(
            filter((e) => e instanceof NavigationStart),
            map((e) => e as NavigationStart)
          )
          .subscribe(({ url }) => {
            subs.unsubscribe();
            const loginComponentsPaths =
              this._appConfigStream$.value?.componentPaths.login;

            if (!user && !loginComponentsPaths?.find((u) => url.includes(u)))
              this._router.navigateByUrl('login');
            else if (user && loginComponentsPaths?.find((u) => url.includes(u)))
              this._router.navigateByUrl('');
          });
      } else {
        if (user) this._router.navigateByUrl('');
        else this._router.navigateByUrl('login');
      }
    });
  }
}
