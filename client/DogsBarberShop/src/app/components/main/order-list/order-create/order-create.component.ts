import { ConfirmMessage } from './../../../../models/ConfirmMessage';
import { ConfirmMessageStream } from './../../../../infastructure/di_providers/confirmMessageStream.provider';
import { OrderDetailsEditComponent } from './../../order-details/order-details-edit/order-details-edit.component';
import {
  Component,
  OnInit,
  Output,
  EventEmitter,
  ViewChild,
  Inject,
} from '@angular/core';
import { CONFIRM_MESSAGE_STREAM } from 'src/app/infastructure/di_providers/confirmMessageStream.provider';

@Component({
  selector: 'order-create',
  templateUrl: './order-create.component.html',
  styleUrls: ['./order-create.component.sass'],
})
export class OrderCreateComponent implements OnInit {
  @Output() closed = new EventEmitter<void>();
  @ViewChild(OrderDetailsEditComponent) orderEdit!: OrderDetailsEditComponent;

  currentDate!: Date;

  constructor(
    @Inject(CONFIRM_MESSAGE_STREAM)
    private _confirmMessageStream$: ConfirmMessageStream
  ) {}

  ngOnInit(): void {
    this.currentDate = new Date();
    this.currentDate.setHours(10);
    this.currentDate.setMinutes(0);
  }

  onCloseClicked(): void {
    if (this.orderEdit.clonedDate.getTime() !== this.currentDate.getTime())
      this._confirmMessageStream$.next(
        new ConfirmMessage(
          'All changes will be lost.Are you sure to exit?',
          () => this.closed.emit(),
          () => {}
        )
      );
    else this.closed.emit();
  }
}
