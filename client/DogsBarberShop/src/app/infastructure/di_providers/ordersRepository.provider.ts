import { TestOrdersRepositoryService } from '../../services/repositories/TestOrdersRepository.service';
import { ClassProvider } from '@angular/core';
import { OrdersRepository } from '../../services/repositories/OrdersRepository';

export const ordersRepositoryProvider: ClassProvider = {
  provide: OrdersRepository,
  useClass: TestOrdersRepositoryService,
};
