import { InjectionToken, ValueProvider } from '@angular/core';
import { Subject } from 'rxjs';
import { ConfirmMessage } from 'src/app/models/ConfirmMessage';

export type ConfirmMessageStream = Subject<ConfirmMessage>;

export const CONFIRM_MESSAGE_STREAM = new InjectionToken<ConfirmMessageStream>(
  'Confirm message stream'
);

const confirmMessageStream$ = new Subject<ConfirmMessage>();

export const confirmMessageStreamProvide: ValueProvider = {
  provide: CONFIRM_MESSAGE_STREAM,
  useValue: confirmMessageStream$,
};
