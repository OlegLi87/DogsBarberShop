import { Order } from './../../../../models/Order';
import { Component, HostListener, Input, OnInit } from '@angular/core';
import { listItemMatCardSlide } from 'src/app/infastructure/animations/listItemMatCardSlide.animation';

@Component({
  selector: 'order-list-item',
  templateUrl: './order-list-item.component.html',
  styleUrls: ['./order-list-item.component.sass'],
  animations: [listItemMatCardSlide],
})
export class OrderListItemComponent implements OnInit {
  @Input() order!: Order;
  @HostListener('mouseenter')
  onMouseEnter(): void {
    this.animationState = 'slide';
  }
  @HostListener('mouseleave')
  onMouseLeave(): void {
    this.animationState = 'normal';
  }

  showDetails = false;
  animationState = 'normal';

  constructor() {}

  ngOnInit(): void {}

  closeOrderDetailsModal(): void {
    this.showDetails = false;
    this.animationState = 'normal';
  }
}
