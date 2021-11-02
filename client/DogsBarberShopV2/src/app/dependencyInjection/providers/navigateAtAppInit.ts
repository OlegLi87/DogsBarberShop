import { distinctUntilChanged } from 'rxjs/operators';
import { APP_INITIALIZER, FactoryProvider } from '@angular/core';
import { Router } from '@angular/router';
import { UserStream, USER_STREAM } from '../tokens/userStream.diToken';
import { AuthService } from '../../services/auth.service';

function streamUserAtAppInit(
  authService: AuthService,
  userStream$: UserStream,
  router: Router
): () => void {
  return () => {
    authService.authUserAtAppInit();
    // userStream$.pipe(distinctUntilChanged()).subscribe((user) => {
    //   if (!user) router.navigate(['login']);
    //   else router.navigate(['']);
    // });
  };
}

export const navigateAtAppInitProvider: FactoryProvider = {
  provide: APP_INITIALIZER,
  multi: true,
  useFactory: streamUserAtAppInit,
  deps: [AuthService, USER_STREAM, Router],
};
