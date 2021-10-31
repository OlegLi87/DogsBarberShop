import LocalStorageService from './localStorage.service';
import { Inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import {
  USER_STREAM,
  UserStream,
} from '../dependencyInjection/tokens/userStream.diToken';

@Injectable({
  providedIn: 'root',
})
export default class AuthService {
  private _tokenKey: string = environment.localStorageUserTokenKey;

  constructor(
    private _localStorage: LocalStorageService,
    @Inject(USER_STREAM) private _userStream$: UserStream
  ) {}

  authUserAtAppInit(): void {
    const token = this._localStorage.getItem(this._tokenKey);
    if (!token) this._userStream$.next(null);
  }
}
