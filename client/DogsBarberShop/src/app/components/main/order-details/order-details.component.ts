import { OrderDetailsEditComponent } from './order-details-edit/order-details-edit.component';
import { ConfirmMessage } from 'src/app/models/ConfirmMessage';
import { ConfirmMessageStream } from './../../../infastructure/di_providers/confirmMessageStream.provider';
import {
  USER_STREAM,
  UserStream,
} from './../../../infastructure/di_providers/userStream.provider';
import { Order } from './../../../models/Order';
import {
  Component,
  EventEmitter,
  Inject,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { OrdersRepository } from 'src/app/services/repositories/OrdersRepository';
import { CONFIRM_MESSAGE_STREAM } from 'src/app/infastructure/di_providers/confirmMessageStream.provider';
import {
  orderDetailsRotation_edit,
  orderDetailsRotation_info,
} from 'src/app/infastructure/animations/orderDetailsRotation.animation';

@Component({
  selector: 'order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.sass'],
  animations: [orderDetailsRotation_info, orderDetailsRotation_edit],
})
export class OrderDetailsComponent implements OnInit {
  @Input() order!: Order;
  @Output() closed = new EventEmitter<void>();
  animationState = 'normal';
  @ViewChild(OrderDetailsEditComponent)
  editComponent!: OrderDetailsEditComponent;

  constructor(
    @Inject(USER_STREAM) private _userStream$: UserStream,
    @Inject(CONFIRM_MESSAGE_STREAM)
    private _confirmMessageStream: ConfirmMessageStream,
    private _ordersRepository: OrdersRepository
  ) {}

  ngOnInit(): void {}

  ngDoCheck(): void {
    console.log('%c Checking in order-details', 'color: green');
  }

  removeOrder(): void {
    this._confirmMessageStream.next(
      new ConfirmMessage(
        'Are you sure to delete your order?',
        this._ordersRepository.removeOrder.bind(
          this._ordersRepository,
          this.order.orderId
        ),
        () => {}
      )
    );
  }

  onClosedClicked(): void {
    if (
      this.editComponent.clonedDate.getTime() !==
      this.order.arrivalTime.getTime()
    )
      this._confirmMessageStream.next(
        new ConfirmMessage(
          'All changes will be lost.Are you sure to exit?',
          () => {
            this.closed.emit();
          },
          () => {}
        )
      );
    else this.closed.emit();
  }

  get toShowButtons(): boolean {
    return this.order.userId === this._userStream$.value?.id;
  }
}
