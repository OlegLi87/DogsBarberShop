import { Order } from './../../../../models/Order';
import {
  Component,
  ElementRef,
  HostListener,
  Input,
  OnInit,
} from '@angular/core';
import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';

@Component({
  selector: 'order-list-item',
  templateUrl: './order-list-item.component.html',
  styleUrls: ['./order-list-item.component.sass'],
  animations: [
    trigger('cardSlide', [
      state('normal', style({})),
      state(
        'slide',
        style({
          transform: 'translate(clamp(40px,7vw,60px))',
        })
      ),
      transition('normal => slide', animate(150)),
      transition('slide => normal', animate(150)),
    ]),
  ],
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
