import { Observable } from 'rxjs';
import { Order } from '../../models/Order';

export abstract class OrdersRepository {
  abstract getRepositoryStream(): Observable<Array<Order>>;
  abstract streamOrders(): void;
  abstract addOrder(arrivalDate: Date): void;
  abstract removeOrder(orderId: string): void;
  abstract updateOrder(
    orderId: string,
    updatedData: { [key: string]: any }
  ): void;
}
