import { JwtService } from './jwt.service';
import { environment } from './../../environments/environment';
import { LocalStorageService } from './localStorage.service';
import { Inject, Injectable } from '@angular/core';
import { UserStream, USER_STREAM } from '../di_providers/userStream.provider';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private jwtLocalStorageKey = environment.localStorageKeys.jwt;

  constructor(
    private localStorageService: LocalStorageService,
    private jwtService: JwtService,
    @Inject(USER_STREAM) private userStream$: UserStream
  ) {}

  streamUserAtAppInit(): void {
    const jwtToken = this.localStorageService.getItem(this.jwtLocalStorageKey);
    if (jwtToken === null) return;

    const user = this.jwtService.createUserFromToken(jwtToken);
    if (!user) {
      this.localStorageService.deleteItem(this.jwtLocalStorageKey);
      return;
    }

    this.userStream$.next(user);
  }
}
