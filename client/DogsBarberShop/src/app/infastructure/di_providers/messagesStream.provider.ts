import { InjectionToken, ValueProvider } from '@angular/core';
import { Subject } from 'rxjs';
import { Message } from 'src/app/models/Message';

export type MessagesStream = Subject<Message>;

export const MESSAGES_STREAM = new InjectionToken<MessagesStream>(
  'messsages stream'
);

const messagesStream$: MessagesStream = new Subject<Message>();

export const messagesStreamProvider: ValueProvider = {
  provide: MESSAGES_STREAM,
  useValue: messagesStream$,
};
