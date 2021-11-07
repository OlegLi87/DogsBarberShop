import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { appErrorHandlerProvider } from './../../dependencyInjection/providers/appErrorHandler.provider';
import { NotificationMessageComponent } from './../../components/shared/notification-message/notification-message.component';
import { importAppConfigAtAppInitProvider } from './../../dependencyInjection/providers/importAppConfigAtAppInit.provider';
import { navigateAtAppInitProvider } from '../../dependencyInjection/providers/navigateAtAppInit';
import { RoutingModule } from './routing.module';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { TaigaUiModule } from '../taigaui/taigaui.module';
import { CommonModule } from '@angular/common';
import { appHttpInterceptorProvider } from 'src/app/dependencyInjection/providers/appHttpInterceptor.provider';

const inOutModules = [
  RoutingModule,
  TaigaUiModule,
  HttpClientModule,
  BrowserAnimationsModule,
];
const inOutDeclarables = [NotificationMessageComponent];

@NgModule({
  declarations: [...inOutDeclarables],
  imports: [...inOutModules, CommonModule],
  exports: [...inOutModules, ...inOutDeclarables],
  providers: [
    navigateAtAppInitProvider,
    importAppConfigAtAppInitProvider,
    appHttpInterceptorProvider,
    appErrorHandlerProvider,
  ],
})
export class CoreModule {}
