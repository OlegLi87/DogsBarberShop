import { BehaviorSubject } from 'rxjs';
import { InjectionToken } from '@angular/core';
import { AppConfig } from 'src/app/models/appConfig';

export type AppConfigStream = BehaviorSubject<AppConfig | null>;
export const APP_CONFIG_STREAM = new InjectionToken<AppConfigStream>(
  'app config stream',
  {
    providedIn: 'root',
    factory: () => new BehaviorSubject<AppConfig | null>(null),
  }
);
