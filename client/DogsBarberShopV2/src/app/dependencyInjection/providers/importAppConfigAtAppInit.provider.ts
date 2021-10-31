import { FactoryProvider, APP_INITIALIZER } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { APP_CONFIG_STREAM } from './appConfig.provider';

function importAppConfig(
  httpClient: HttpClient,
  appConfigStream$: BehaviorSubject<any>
) {
  return () => {
    return new Promise<void>((res, rej) => {
      httpClient
        .get('./assets/appConfig.json')
        .pipe(
          catchError((err) => {
            rej('Error occurred while loading config file.');
            throw err;
          })
        )
        .subscribe((config) => {
          appConfigStream$.next(config);
          res();
        });
    });
  };
}

export const importAppConfigAtAppInitProvider: FactoryProvider = {
  provide: APP_INITIALIZER,
  useFactory: importAppConfig,
  multi: true,
  deps: [HttpClient, APP_CONFIG_STREAM],
};
