import { userStreamProvider } from './di_providers/userStream.provider';
import { LoginComponent } from './components/login/login.component';
import { RoutingModule } from './routing.module';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MainComponent } from './components/main/main.component';
import { appInitiProvider } from './di_providers/appInit.provider';

const modules = [RoutingModule, FormsModule];
const components = [LoginComponent, MainComponent];
const diProviders = [userStreamProvider, appInitiProvider];

@NgModule({
  declarations: components,
  imports: modules,
  exports: [...components, ...modules],
  providers: diProviders,
})
export class CoreModule {}
