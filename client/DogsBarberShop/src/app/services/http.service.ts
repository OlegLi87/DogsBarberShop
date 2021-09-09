import { UserCredentials } from './../models/UserCredentials';
import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginMode } from '../models/LoginMode';

@Injectable({ providedIn: 'root' })
export class HttpService {
  private _api_address = environment.apiAdress;

  constructor(private _httpClient: HttpClient) {}

  login(
    userCredentials: UserCredentials,
    loginMode: LoginMode
  ): Observable<string> {
    const httpOptions: Object = {
      responseType: 'text',
    };

    const apiAddress = `${this._api_address}/auth/${
      loginMode === LoginMode.SignUp ? 'signup' : 'signin'
    }`;

    return this._httpClient.post<string>(
      apiAddress,
      userCredentials,
      httpOptions
    );
  }
}
