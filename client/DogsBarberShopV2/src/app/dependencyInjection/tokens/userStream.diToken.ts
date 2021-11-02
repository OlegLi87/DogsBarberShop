import { InjectionToken } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { User } from 'src/app/models/user';

export type UserStream = BehaviorSubject<User | null>;

export const USER_STREAM = new InjectionToken<UserStream>('user stream', {
  providedIn: 'root',
  factory: () => new BehaviorSubject<User | null>(null),
});
