import { HttpService } from './http.service';
import { UserCredentials } from './../models/UserCredentials';
import { JwtService } from './jwt.service';
import { environment } from './../../environments/environment';
import { LocalStorageService } from './localStorage.service';
import { Inject, Injectable } from '@angular/core';
import {
  UserStream,
  USER_STREAM,
} from '../infastructure/di_providers/userStream.provider';
import { LoginMode } from '../models/LoginMode';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _jwtLocalStorageKey = environment.localStorageKeys.jwt;

  constructor(
    private _localStorageService: LocalStorageService,
    private _jwtService: JwtService,
    private _httpService: HttpService,
    @Inject(USER_STREAM) private _userStream$: UserStream
  ) {}

  streamUserAtAppInit(): void {
    const jwtToken = this._localStorageService.getItem(
      this._jwtLocalStorageKey
    );
    if (jwtToken === null) return;

    const user = this._jwtService.createUserFromToken(jwtToken);
    if (!user) {
      this.logout();
      return;
    }
    this._userStream$.next(user);
  }

  login(
    userCredentials: UserCredentials,
    loginMode: LoginMode
  ): Observable<string> {
    return this._httpService
      .login(userCredentials, loginMode)
      .pipe(tap(this.saveTokenAndStreamUser.bind(this)));
  }

  logout(): void {
    this._localStorageService.deleteItem(this._jwtLocalStorageKey);
    this._userStream$.next(null);
  }

  private saveTokenAndStreamUser(jwtToken: string): void {
    this._localStorageService.saveItem(this._jwtLocalStorageKey, jwtToken);

    const user = this._jwtService.createUserFromToken(jwtToken);
    this._userStream$.next(user);
  }
}
