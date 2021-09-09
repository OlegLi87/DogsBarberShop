import { UserStream } from '../infastructure/di_providers/userStream.provider';
import { Inject, Injectable } from '@angular/core';
import { USER_STREAM } from '../infastructure/di_providers/userStream.provider';
import { Router } from '@angular/router';
import { User } from '../models/User';

@Injectable({
  providedIn: 'root',
})
export class NavigationService {
  constructor(
    private router: Router,
    @Inject(USER_STREAM) private userStream$: UserStream
  ) {
    this.userStream$.subscribe(this.navigate.bind(this));
  }

  private navigate(user: User | null): void {
    if (user) this.router.navigateByUrl('');
    else this.router.navigateByUrl('/login');
  }
}
