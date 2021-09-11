import { Order } from '../../models/Order';

export abstract class OrdersRepository {
  abstract getOrders(): Order[];
  abstract addOrder(order: Order): void;
  abstract removeOrder(orderId: string): void;
}