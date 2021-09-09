import { MainComponent } from './components/main/main.component';
import { MainGuard } from './services/route_guards/mainGuard.service';
import { NotFoundComponent } from './components/shared/not-found/not-found.component';
import { LoginComponent } from './components/login/login.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrderListComponent } from './components/main/order-list/order-list.component';

const routes: Routes = [
  {
    path: 'orders',
    component: MainComponent,
    canActivate: [MainGuard],
    children: [{ path: '', component: OrderListComponent }],
  },
  { path: 'login', component: LoginComponent, canActivate: [MainGuard] },
  { path: '', redirectTo: '/orders', pathMatch: 'full' },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class RoutingModule {}
