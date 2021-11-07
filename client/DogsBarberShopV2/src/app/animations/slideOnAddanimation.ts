import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';

export const SlideOnAdd = trigger('slideOnAdd', [
  state(
    'void',
    style({
      opacity: 0,
      transform: 'translateX(-200%)',
    })
  ),
  transition('void => *', animate('500ms ease-out')),
  transition(
    '* => void',
    animate(
      '500ms ease-in',
      style({
        transform: 'translateX(200%)',
        opacity: 0,
      })
    )
  ),
]);
