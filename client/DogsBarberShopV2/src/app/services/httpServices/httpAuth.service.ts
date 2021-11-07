import { MessageResponse, TokenResponse } from './../auth.service';
import { ResetPasswordData } from './../../models/resetPasswordData';
import { LoginCredentials } from './../../models/loginCredentials';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { ForgotPasswordData } from 'src/app/models/forgotPasswordData';

@Injectable({
  providedIn: 'root',
})
export class HttpAuthService {
  private readonly _apiAddress = environment.api_address + '/auth';

  constructor(private _httpClient: HttpClient) {}

  signUp(loginCredentials: LoginCredentials): Observable<MessageResponse> {
    return this._httpClient.post<any>(
      this._apiAddress + '/signUp',
      loginCredentials
    );
  }

  signIn(loginCredentials: LoginCredentials): Observable<TokenResponse> {
    return this._httpClient.post<any>(
      this._apiAddress + '/signIn',
      loginCredentials
    );
  }

  confirmEmail<T>(token: string, email: string): Observable<T> {
    return this._httpClient.post<T>(
      this._apiAddress + `/confirmEmail?token=${token}&email=${email}`,
      {}
    );
  }

  forgotPassword(data: ForgotPasswordData): Observable<MessageResponse> {
    return this._httpClient.post<MessageResponse>(
      this._apiAddress + '/forgotPassword',
      data
    );
  }

  resetPassword(
    data: ResetPasswordData,
    token: string,
    email: string
  ): Observable<MessageResponse> {
    return this._httpClient.post<MessageResponse>(
      `${this._apiAddress + '/resetPassword'}?token=${token}&email=${email}`,
      data
    );
  }
}
