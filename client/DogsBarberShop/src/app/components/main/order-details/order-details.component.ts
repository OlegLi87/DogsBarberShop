import { ConfirmMessage } from 'src/app/models/ConfirmMessage';
import { ConfirmMessageStream } from './../../../infastructure/di_providers/confirmMessageStream.provider';
import {
  USER_STREAM,
  UserStream,
} from './../../../infastructure/di_providers/userStream.provider';
import { Order } from './../../../models/Order';
import {
  AfterViewInit,
  Component,
  ElementRef,
  EventEmitter,
  Inject,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { OrdersRepository } from 'src/app/services/repositories/OrdersRepository';
import { CONFIRM_MESSAGE_STREAM } from 'src/app/infastructure/di_providers/confirmMessageStream.provider';

@Component({
  selector: 'order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.sass'],
})
export class OrderDetailsComponent implements OnInit, AfterViewInit {
  @Input() order!: Order;
  @Output() closed = new EventEmitter<void>();

  @ViewChild('timePickerToggler')
  timePickerToggler!: ElementRef<HTMLInputElement>;

  constructor(
    @Inject(USER_STREAM) private _userStream$: UserStream,
    @Inject(CONFIRM_MESSAGE_STREAM)
    private _confirmMessageStream: ConfirmMessageStream,
    private _ordersRepository: OrdersRepository
  ) {}

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    console.log('in view init');
    console.log(this.timePickerToggler);
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

  get toShowButtons(): boolean {
    return this.order.userId === this._userStream$.value?.id;
  }

  onEditClicked(): void {
    this.timePickerToggler.nativeElement.click();
  }
}
