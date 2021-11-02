import { NotificationMessage } from './../../models/notificationMessage';
import { InjectionToken } from '@angular/core';
import { Subject } from 'rxjs';

export type NotificationMessageStream = Subject<NotificationMessage>;

export const NOTIFICATION_MESSAGE_STREAM =
  new InjectionToken<NotificationMessageStream>('notification message', {
    providedIn: 'root',
    factory: () => new Subject<NotificationMessage>(),
  });
