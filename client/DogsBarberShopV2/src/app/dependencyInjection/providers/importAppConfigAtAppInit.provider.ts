import { APP_CONFIG_STREAM } from './../tokens/appConfig.diToken';
import { FactoryProvider, APP_INITIALIZER } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { AppConfigStream } from '../tokens/appConfig.diToken';
import { AppConfig } from 'src/app/models/appConfig';

function importAppConfig(
  httpClient: HttpClient,
  appConfigStream$: AppConfigStream
) {
  return () => {
    return httpClient.get<AppConfig>('./assets/appConfig.json').pipe(
      tap((config) => {
        appConfigStream$.next(config);
      }),
      catchError((err) => {
        console.error('Failed to load config file.');
        throw err;
      })
    );
  };
}

export const importAppConfigAtAppInitProvider: FactoryProvider = {
  provide: APP_INITIALIZER,
  useFactory: importAppConfig,
  multi: true,
  deps: [HttpClient, APP_CONFIG_STREAM],
};
