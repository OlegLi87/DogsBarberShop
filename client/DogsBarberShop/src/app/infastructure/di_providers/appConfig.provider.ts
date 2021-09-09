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
    success: '#D1F8D1',
    warning: '#FFD65C',
    error: '#ff5454',
  },
};

export const appConfigProvider: ValueProvider = {
  provide: 'appConfig',
  useValue: appConfig,
};
