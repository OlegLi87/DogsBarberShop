import { HttpAuthService } from './httpServices/httpAuth.service';
import { LoginCredentials } from './../models/loginCredentials';
import { LocalStorageService } from './localStorage.service';
import { Inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import {
  USER_STREAM,
  UserStream,
} from '../dependencyInjection/tokens/userStream.diToken';
import { LoginMode } from '../components/auth/login-page/login-page.component';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _tokenKey: string = environment.localStorageUserTokenKey;

  constructor(
    private _localStorage: LocalStorageService,
    private _httpAuth: HttpAuthService,
    @Inject(USER_STREAM) private _userStream$: UserStream
  ) {}

  authUserAtAppInit(): void {
    const token = this._localStorage.getItem(this._tokenKey);
    if (!token) this._userStream$.next(null);
  }

  login(loginCredentials: LoginCredentials, loginMode: LoginMode): void {
    // if (loginMode === LoginMode.SignUp) this._httpAuth.signUp(loginCredentials)
    // else this._httpAuth.signIn(loginCredentials);
    if (loginMode === LoginMode.SignUp) {
      loginCredentials.emailConfirmationUrl = environment.emailConfirmationUrl;
      this._httpAuth.signUp(loginCredentials).subscribe((data) => {
        console.log(data);
      });
    }
  }
}
