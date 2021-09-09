import { Order } from './Order';

export interface OrdersRepository {
  getOrders: () => Order[];
  addOrder: (order: Order) => void;
  removeOrder: (orderId: string) => void;
}
