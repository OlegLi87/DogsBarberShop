import { Observable } from 'rxjs';
import { Order } from '../../models/Order';

export abstract class OrdersRepository {
  abstract getRepositoryStream(): Observable<Array<Order> | Order>;
  abstract streamOrder(orderId: string): void;
  abstract streamOrders(): void;
  abstract addOrder(order: Order): void;
  abstract removeOrder(orderId: string): void;
  abstract updateOrder(
    orderId: string,
    updatedData: { [key: string]: any }
  ): void;
}
