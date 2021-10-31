import { BehaviorSubject } from 'rxjs';
import { InjectionToken, ValueProvider } from '@angular/core';

export const APP_CONFIG_STREAM = new InjectionToken('app config');
const appConfigStream$ = new BehaviorSubject<any>(null);

export const appConfigStreamProvider: ValueProvider = {
  provide: APP_CONFIG_STREAM,
  useValue: appConfigStream$,
};
