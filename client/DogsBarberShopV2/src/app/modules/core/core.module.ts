import { importAppConfigAtAppInitProvider } from './../../dependencyInjection/providers/importAppConfigAtAppInit.provider';
import { navigateAtAppInitProvider } from '../../dependencyInjection/providers/navigateAtAppInit';
import { RoutingModule } from './routing.module';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { TaigaUiModule } from '../taigaui/taigaui.module';
import { appConfigStreamProvider } from 'src/app/dependencyInjection/providers/appConfig.provider';

const inOutModules = [RoutingModule, TaigaUiModule, HttpClientModule];

@NgModule({
  declarations: [],
  imports: [...inOutModules],
  exports: [...inOutModules],
  providers: [
    navigateAtAppInitProvider,
    importAppConfigAtAppInitProvider,
    appConfigStreamProvider,
  ],
})
export class CoreModule {}
