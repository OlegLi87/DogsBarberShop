import { Subscription } from 'rxjs';
import { Component, OnInit, OnDestroy } from '@angular/core';
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
  isLoading = false;
  orders!: Order[];

  constructor(private _ordersRepository: OrdersRepository) {}

  ngOnInit(): void {
    this._subscription = this._ordersRepository
      .getRepositoryStream()
      .subscribe((o) => {
        if (Array.isArray(o)) {
          this.orders = o;
          this.isLoading = false;
        }
      });

    this._ordersRepository.streamOrders();
    this.isLoading = true;
  }

  trackByFunction(index: number, order: Order): string {
    return order.orderId;
  }

  ngOnDestroy(): void {
    this._subscription.unsubscribe();
  }
}
