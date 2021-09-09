import { InjectionToken, ValueProvider } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { User } from '../../models/User';

export type UserStream = BehaviorSubject<null | User>;

export const USER_STREAM = new InjectionToken<UserStream>('user stream');

const userStream$: UserStream = new BehaviorSubject<null | User>(null);

export const userStreamProvider: ValueProvider = {
  provide: USER_STREAM,
  useValue: userStream$,
};
