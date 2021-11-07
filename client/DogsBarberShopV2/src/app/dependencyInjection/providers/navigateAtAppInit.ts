import { APP_INITIALIZER, FactoryProvider } from '@angular/core';
import { AuthService } from '../../services/auth.service';

function streamUserAtAppInit(authService: AuthService): () => void {
  return () => {
    authService.authUserAtAppInit();
  };
}

export const navigateAtAppInitProvider: FactoryProvider = {
  provide: APP_INITIALIZER,
  multi: true,
  useFactory: streamUserAtAppInit,
  deps: [AuthService],
};
