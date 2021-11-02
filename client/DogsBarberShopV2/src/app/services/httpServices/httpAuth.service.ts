import { LoginCredentials } from './../../models/loginCredentials';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

export type SignUpResponse = { message: string };
export type SignInResponse = string;

@Injectable({
  providedIn: 'root',
})
export class HttpAuthService {
  private readonly _apiAddress = environment.api_address + '/auth';

  constructor(private _httpClient: HttpClient) {}

  signUp(loginCredentials: LoginCredentials): Observable<SignUpResponse> {
    return this._httpClient.post<any>(
      this._apiAddress + '/signUp',
      loginCredentials
    );
  }

  signIn(loginCredentials: LoginCredentials): Observable<SignInResponse> {
    return this._httpClient.post<any>(
      this._apiAddress + '/signIn',
      loginCredentials
    );
  }
}
