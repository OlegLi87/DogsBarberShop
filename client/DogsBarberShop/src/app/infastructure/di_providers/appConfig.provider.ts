import { ValueProvider } from '@angular/core';

export interface AppConfig {
  colors: {
    success: string;
    warning: string;
    error: string;
    [key: string]: string;
  };
}

const appConfig: AppConfig = {
  colors: {
    success: 'rgba(121, 224, 121, 0.95)',
    warning: 'rgba(255, 214, 92, 0.95)',
    error: 'rgba(255, 84, 84,0.95)',
  },
};

export const appConfigProvider: ValueProvider = {
  provide: 'appConfig',
  useValue: appConfig,
};
