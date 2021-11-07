import { ResetPasswordData } from './../models/resetPasswordData';
import { environment } from './../../environments/environment';
import { ForgotPasswordData } from './../models/forgotPasswordData';
import { User } from './../models/user';
import { HttpAuthService } from './httpServices/httpAuth.service';
import { LoginCredentials } from './../models/loginCredentials';
import { LocalStorageService } from './localStorage.service';
import {
  USER_STREAM,
  UserStream,
} from '../dependencyInjection/tokens/userStream.diToken';
import { LoginMode } from '../components/auth/login-page/login-page.component';
import {
  NotificationMessagesStream,
  NOTIFICATION_MESSAGE_STREAM,
} from '../dependencyInjection/tokens/notificationMessageStream.diToken';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { NotificationMessageStatus } from '../models/notificationMessage';
import jwtDecode from 'jwt-decode';
import { Inject, Injectable } from '@angular/core';
import { NavigationManagerService } from './navigationManager.service';

export type MessageResponse = { message: string };
export type TokenResponse = { token: string };

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _tokenKey: string = environment.localStorageUserTokenKey;

  constructor(
    private _localStorage: LocalStorageService,
    private _httpAuth: HttpAuthService,
    private _navigationManager: NavigationManagerService,
    @Inject(USER_STREAM) private _userStream$: UserStream,
    @Inject(NOTIFICATION_MESSAGE_STREAM)
    private _notificationMessagesStream$: NotificationMessagesStream
  ) {}

  authUserAtAppInit(): void {
    const token = this._localStorage.getItem(this._tokenKey);
    if (!token) return this._userStream$.next(null);

    try {
      const user = this.createUserFromToken(token);
      this._userStream$.next(user);
    } catch {
      this._userStream$.next(null);
    }
  }

  login(
    loginCredentials: LoginCredentials,
    loginMode: LoginMode
  ): Observable<any> {
    if (loginMode === LoginMode.SignUp) {
      loginCredentials.emailConfirmationUrl = environment.emailConfirmationUrl;
      return this._httpAuth.signUp(loginCredentials).pipe(
        tap((res) => {
          this._notificationMessagesStream$.next({
            message: res.message,
            status: NotificationMessageStatus.Information,
          });
        })
      );
    }

    return this._httpAuth.signIn(loginCredentials).pipe(
      map(({ token }) => this.createUserFromToken(token)),
      tap((user) => {
        this.saveToken(user.token);
        this._userStream$.next(user);
      })
    );
  }

  confirmEmail<T>(token: string, email: string): Observable<T> {
    return this._httpAuth.confirmEmail<T>(encodeURIComponent(token), email);
  }

  forgotPassword(email: string): Observable<MessageResponse> {
    const data: ForgotPasswordData = {
      email: email,
      resetPasswordUrl: environment.resetPasswordUrl,
    };
    return this._httpAuth.forgotPassword(data).pipe(
      tap((res) => {
        this._notificationMessagesStream$.next({
          message: res.message,
          status: NotificationMessageStatus.Information,
        });
      })
    );
  }

  resetPassword(
    data: ResetPasswordData,
    token: string,
    email: string
  ): Observable<MessageResponse> {
    return this._httpAuth
      .resetPassword(data, encodeURIComponent(token), email)
      .pipe(
        tap((res) => {
          this._navigationManager.navigate('/login', () =>
            setTimeout(
              () =>
                this._notificationMessagesStream$.next({
                  message: res.message,
                  status: NotificationMessageStatus.Success,
                }),
              800
            )
          );
        })
      );
  }

  private createUserFromToken(token: string): User {
    const data = jwtDecode(token) as any;
    const user: User = {
      userName: data['userName'],
      email: data['email'],
      firstName: data['firstName'],
      token: token,
    };
    return user;
  }

  private saveToken(token: string): void {
    this._localStorage.addItem(this._tokenKey, token);
  }
}
