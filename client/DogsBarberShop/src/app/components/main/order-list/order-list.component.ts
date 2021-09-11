import { Component, OnInit } from '@angular/core';
import { Order } from 'src/app/models/Order';
import { OrdersRepository } from 'src/app/services/repositories/OrdersRepository';

@Component({
  selector: 'order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.sass'],
})
export class OrderListComponent implements OnInit {
  orders!: Order[];

  constructor(private _ordersRepostory: OrdersRepository) {}

  ngOnInit(): void {
    this.orders = this._ordersRepostory.getOrders();
  }

  trackByFunction(index: number, order: Order): string {
    return order.orderId;
  }
}
