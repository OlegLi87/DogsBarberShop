import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';

const trans = transition('normal <=> rotate', animate(500));

export const orderDetailsRotation_info = trigger('infoRotation', [
  state(
    'normal',
    style({
      transform: 'rotateY(0deg)',
    })
  ),
  state(
    'rotate',
    style({
      transform: 'rotateY(180deg)',
    })
  ),
  trans,
]);

export const orderDetailsRotation_edit = trigger('editRotation', [
  state(
    'normal',
    style({
      transform: 'rotateY(180deg)',
    })
  ),
  state(
    'rotate',
    style({
      transform: 'rotateY(0deg)',
    })
  ),
  trans,
]);
