import { NotificationMessageComponent } from './../../components/shared/notification-message/notification-message.component';
import { importAppConfigAtAppInitProvider } from './../../dependencyInjection/providers/importAppConfigAtAppInit.provider';
import { navigateAtAppInitProvider } from '../../dependencyInjection/providers/navigateAtAppInit';
import { RoutingModule } from './routing.module';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { TaigaUiModule } from '../taigaui/taigaui.module';
import { CommonModule } from '@angular/common';

const inOutModules = [RoutingModule, TaigaUiModule, HttpClientModule];
const inOutDeclarables = [NotificationMessageComponent];

@NgModule({
  declarations: [...inOutDeclarables],
  imports: [...inOutModules, CommonModule],
  exports: [...inOutModules, ...inOutDeclarables],
  providers: [navigateAtAppInitProvider, importAppConfigAtAppInitProvider],
})
export class CoreModule {}
