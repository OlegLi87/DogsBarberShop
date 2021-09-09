import { confirmMessageStreamProvide } from './infastructure/di_providers/confirmMessageStream.provider';
import { ConfirmModalComponent } from './components/shared/confirm-modal/confirm-modal.component';
import { MainComponent } from './components/main/main.component';
import { OrderListComponent } from './components/main/order-list/order-list.component';
import { errorHandlerProvider } from './infastructure/di_providers/errorHandler.provider';
import { appConfigProvider } from './infastructure/di_providers/appConfig.provider';
import { MessageToasterComponent } from './components/shared/message-toaster/message-toaster.component';
import { LoadingSpinnerComponent } from './components/shared/loading-spinner/loading-spinner.component';
import { MaterialModule } from './material.module';
import { NotFoundComponent } from './components/shared/not-found/not-found.component';
import { userStreamProvider } from './infastructure/di_providers/userStream.provider';
import { LoginComponent } from './components/login/login.component';
import { RoutingModule } from './routing.module';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { appInitiProvider } from './infastructure/di_providers/appInit.provider';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { httpInterceptorProvider } from './infastructure/di_providers/httpInterceptor.provider';
import { messagesStreamProvider } from './infastructure/di_providers/messagesStream.provider';
import { OrderComponent } from './components/main/order-list/order/order.component';

const modules = [
  CommonModule,
  RoutingModule,
  FormsModule,
  HttpClientModule,
  MaterialModule,
];
const components = [
  LoginComponent,
  OrderListComponent,
  OrderComponent,
  NotFoundComponent,
  LoadingSpinnerComponent,
  MessageToasterComponent,
  MainComponent,
  OrderListComponent,
  OrderComponent,
  ConfirmModalComponent,
];
const diProviders = [
  userStreamProvider,
  appInitiProvider,
  httpInterceptorProvider,
  messagesStreamProvider,
  appConfigProvider,
  errorHandlerProvider,
  confirmMessageStreamProvide,
];

@NgModule({
  declarations: components,
  imports: modules,
  exports: [...components, ...modules],
  providers: diProviders,
})
export class CoreModule {}
