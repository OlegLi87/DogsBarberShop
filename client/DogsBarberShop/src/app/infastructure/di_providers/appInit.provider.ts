import { AuthService } from '../../services/auth.service';
import { APP_INITIALIZER, FactoryProvider } from '@angular/core';

function getInitFunc(authService: AuthService): () => void {
  return () => {
    authService.streamUserAtAppInit();
  };
}

export const appInitiProvider: FactoryProvider = {
  provide: APP_INITIALIZER,
  useFactory: getInitFunc,
  multi: true,
  deps: [AuthService],
};
