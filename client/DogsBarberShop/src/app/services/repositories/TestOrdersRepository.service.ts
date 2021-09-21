import {
  USER_STREAM,
  UserStream,
} from './../../infastructure/di_providers/userStream.provider';
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
import { Subject, Observable } from 'rxjs';

import { nanoid } from 'nanoid';
import { User } from 'src/app/models/User';

@Injectable()
export class TestOrdersRepositoryService extends OrdersRepository {
  private _orders!: Order[];
  private _repositoryStream$ = new Subject<Array<Order>>();
  private _callingCount = 0;

  constructor(
    @Inject(MESSAGES_STREAM) private _messagesStream$: MessagesStream,
    @Inject(USER_STREAM) private _userStream$: UserStream
  ) {
    super();
    this._orders = new Array<Order>();
    this._orders.push(
      new Order(
        nanoid(),
        nanoid(),
        'Jack',
        new Date(2021, 10, 10),
        new Date(2021, 9, 9)
      )
    );
    this._orders.push(
      new Order(
        nanoid(),
        nanoid(),
        'Sara',
        new Date(2021, 10, 25),
        new Date(2021, 9, 10)
      )
    );
    this._orders.push(
      new Order(
        nanoid(),
        nanoid(),
        'Mike',
        new Date(2021, 9, 28),
        new Date(2021, 9, 10)
      )
    );
    this._orders.push(
      new Order(
        nanoid(),
        nanoid(),
        'Laura',
        new Date(2021, 11, 3),
        new Date(2021, 9, 15)
      )
    );
    this._orders.push(
      new Order(
        nanoid(),
        'ba951056-35b2-4309-ac56-b9d1cfdc2c3f',
        'Oleg',
        new Date(2021, 11, 18),
        new Date(2021, 9, 25)
      )
    );
  }

  getRepositoryStream(): Observable<Array<Order>> {
    return this._repositoryStream$;
  }

  // with network latency imitation
  streamOrders(): void {
    const orders = new Array<Order>();
    this._orders.forEach((o) => orders.push(cloneDeep(o)));
    if (!this._callingCount++)
      setTimeout(() => this._repositoryStream$.next(orders), 3000);
    else this._repositoryStream$.next(orders);
  }

  addOrder(arrivalDate: Date): void {
    const activeUser = this._userStream$.value as User;
    const newOrder = new Order(
      nanoid(),
      activeUser.id,
      activeUser.firstName,
      arrivalDate,
      new Date(Date.now())
    );
    this._orders.push(newOrder);
    this.streamOrders();
    this._messagesStream$.next(
      new Message('Order successfully created.', MessageStatus.Success)
    );
  }

  removeOrder(orderId: string): void {
    const index = this._orders.findIndex((o) => o.orderId === orderId);
    if (index > -1) {
      this._orders.splice(index, 1);
      this.streamOrders();
      this._messagesStream$.next(
        new Message(
          'Your order successfully removed from the list.',
          MessageStatus.Success
        )
      );
    }
  }

  updateOrder(orderId: string, updatedData: { [key: string]: any }): void {
    let order = this._orders.find((o) => o.orderId === orderId);
    let updated = false;

    if (order) {
      for (var key in updatedData)
        if ((order as Object).hasOwnProperty(key)) {
          (order as any)[key] = updatedData[key];
          updated = true;
        }
    }

    if (updated) {
      this.streamOrders();
      this._messagesStream$.next(
        new Message('Your order successfuly updated.', MessageStatus.Success)
      );
    }
  }
}
