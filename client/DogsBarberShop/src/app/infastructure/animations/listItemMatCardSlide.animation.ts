import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';

export const listItemMatCardSlide = trigger('cardSlide', [
  state('normal', style({})),
  state(
    'slide',
    style({
      transform: 'translate(clamp(40px,7vw,60px))',
    })
  ),
  transition('normal <=> slide', animate(150)),
]);
