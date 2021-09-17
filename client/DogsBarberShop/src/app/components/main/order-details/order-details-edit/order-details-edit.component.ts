import { DatePipe } from '@angular/common';
import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { OrdersRepository } from 'src/app/services/repositories/OrdersRepository';

@Component({
  selector: 'order-details-edit',
  templateUrl: './order-details-edit.component.html',
  styleUrls: ['./order-details-edit.component.sass'],
})
export class OrderDetailsEditComponent implements OnInit {
  @Input() arrivalDate!: Date;
  @Input() orderId!: string;
  @Output() saved = new EventEmitter<void>();

  private _datePipe = new DatePipe('en');
  clonedDate!: Date;
  minDate = new Date(Date.now());

  constructor(private _ordersRepostory: OrdersRepository) {}

  ngOnInit(): void {
    this.clonedDate = new Date(this.arrivalDate.getTime());
  }

  ngDoCheck(): void {
    console.log('%c Checking in order-details-edit', 'color: blue');
  }

  onDateChanged(event: MatDatepickerInputEvent<any, any>): void {
    const newDate = event.value as Date;
    this.clonedDate.setFullYear(newDate.getFullYear());
    this.clonedDate.setMonth(newDate.getMonth());
    this.clonedDate.setDate(newDate.getDate());
  }

  onSaveClicked(): void {
    this._ordersRepostory.updateOrder(this.orderId, {
      arrivalTime: this.clonedDate,
    });
    this.clonedDate = this.arrivalDate;
    this.saved.emit();
  }

  get date(): string {
    return this._datePipe.transform(this.clonedDate, 'dd/MM/YY') as string;
  }

  get time(): string {
    return this._datePipe.transform(this.clonedDate, 'HH:mm') as string;
  }

  set time(time: string) {
    const [hours, minutes] = time.split(':');
    this.clonedDate.setHours(+hours);
    this.clonedDate.setMinutes(+minutes);
  }

  get isChangesMade(): boolean {
    return this.arrivalDate.getTime() !== this.clonedDate.getTime();
  }
}
