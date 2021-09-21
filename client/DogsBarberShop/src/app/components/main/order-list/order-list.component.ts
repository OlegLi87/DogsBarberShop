import {
  USER_STREAM,
  UserStream,
} from './../../../infastructure/di_providers/userStream.provider';
import { Subscription } from 'rxjs';
import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { Order } from 'src/app/models/Order';
import { OrdersRepository } from 'src/app/services/repositories/OrdersRepository';
import { ordersRepositoryProvider } from 'src/app/infastructure/di_providers/ordersRepository.provider';

@Component({
  selector: 'order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.sass'],
  providers: [ordersRepositoryProvider],
})
export class OrderListComponent implements OnInit, OnDestroy {
  private _subscription!: Subscription;
  orders!: Order[];
  isLoading = false;
  toShowOrderCreate = false;

  constructor(
    private _ordersRepository: OrdersRepository,
    @Inject(USER_STREAM) private _userStream$: UserStream
  ) {}

  ngOnInit(): void {
    this._subscription = this._ordersRepository
      .getRepositoryStream()
      .subscribe((orders) => {
        this.orders = orders;
        this.isLoading = false;
      });

    this.isLoading = true;
    this._ordersRepository.streamOrders();
  }

  trackByFunction(index: number, order: Order): string {
    return order.orderId;
  }

  ngOnDestroy(): void {
    this._subscription.unsubscribe();
  }

  get isActiveUserHasOrder(): boolean {
    const user = this._userStream$.value;
    return !!this.orders.find((o) => o.userId === user?.id);
  }
}
