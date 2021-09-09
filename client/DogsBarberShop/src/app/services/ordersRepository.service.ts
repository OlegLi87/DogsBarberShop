import { Injectable } from '@angular/core';
import { Order } from '../models/Order';
import { OrdersRepository } from './../models/OrdersRepository';

@Injectable({ providedIn: 'root' })
export class OrdersRepositoryService implements OrdersRepository {
  getOrders(): Order[] {
    return [];
  }

  addOrder(order: Order): void {}

  removeOrder(orderId: string): void {}
}
