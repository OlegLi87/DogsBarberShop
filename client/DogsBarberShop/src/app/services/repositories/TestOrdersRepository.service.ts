import { MessageStatus } from './../../models/Message';
import {
  MESSAGES_STREAM,
  MessagesStream,
} from './../../infastructure/di_providers/messagesStream.provider';
import { Inject, Injectable } from '@angular/core';
import { Order } from 'src/app/models/Order';
import { OrdersRepository } from './OrdersRepository';
import { cloneDeep } from 'lodash';
import { Message } from 'src/app/models/Message';

@Injectable()
export class TestOrdersRepositoryService extends OrdersRepository {
  private _orders!: Order[];

  constructor(
    @Inject(MESSAGES_STREAM) private _messagesStream$: MessagesStream
  ) {
    super();
    this._orders = new Array<Order>();
    this._orders.push(
      new Order('1', '1', 'Jack', new Date(2021, 10, 10), new Date(2021, 9, 9))
    );
    this._orders.push(
      new Order('2', '2', 'Sara', new Date(2021, 10, 25), new Date(2021, 9, 10))
    );
    this._orders.push(
      new Order('3', '3', 'Mike', new Date(2021, 9, 28), new Date(2021, 9, 10))
    );
    this._orders.push(
      new Order('4', '4', 'Laura', new Date(2021, 11, 3), new Date(2021, 9, 15))
    );
    this._orders.push(
      new Order(
        '4',
        'ba951056-35b2-4309-ac56-b9d1cfdc2c3f',
        'Oleg',
        new Date(2021, 11, 18),
        new Date(2021, 9, 25)
      )
    );
  }

  getOrder(orderId: string): Order | null {
    let order = this._orders.find((o) => (o.orderId = orderId));
    if (order) return cloneDeep(order);
    return null;
  }

  getOrders(): Order[] {
    const orders = new Array<Order>();
    this._orders.forEach((o) => orders.push(cloneDeep(o)));
    return orders;
  }

  addOrder(order: Order): void {
    this._orders.push(order);
  }

  removeOrder(orderId: string): void {
    const index = this._orders.findIndex((o) => o.orderId === orderId);
    if (index !== -1) this._orders = this._orders.splice(index, 1);
  }

  updateOrder(orderId: string, updatedData: { [key: string]: any }): void {
    let order = this.getOrder(orderId) as Order;
    order = { ...order, ...updatedData };

    const index = this._orders.findIndex((o) => o.orderId === orderId);
    if (index > -1) {
      this._orders.splice(index, 1, order);
      this._messagesStream$.next(
        new Message(['Order updated successfully.'], MessageStatus.Success)
      );
    }
  }
}
