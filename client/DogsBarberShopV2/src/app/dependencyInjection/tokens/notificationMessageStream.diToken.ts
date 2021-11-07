import { NotificationMessage } from './../../models/notificationMessage';
import { InjectionToken } from '@angular/core';
import { Subject } from 'rxjs';

export type NotificationMessagesStream = Subject<
  NotificationMessage | NotificationMessage[]
>;

export const NOTIFICATION_MESSAGE_STREAM =
  new InjectionToken<NotificationMessagesStream>('notification message', {
    providedIn: 'root',
    factory: () => new Subject<NotificationMessage | NotificationMessage[]>(),
  });
