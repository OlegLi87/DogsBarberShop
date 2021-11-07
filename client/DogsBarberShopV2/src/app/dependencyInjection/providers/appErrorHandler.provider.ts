import { NotificationMessage } from './../../models/notificationMessage';
import {
  ClassProvider,
  ErrorHandler,
  Inject,
  Injectable,
  NgZone,
} from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { AppError } from 'src/app/models/appError';
import { NotificationMessageStatus } from 'src/app/models/notificationMessage';
import {
  NotificationMessagesStream,
  NOTIFICATION_MESSAGE_STREAM,
} from '../tokens/notificationMessageStream.diToken';

@Injectable()
export class AppErrorHandler implements ErrorHandler {
  constructor(
    @Inject(NOTIFICATION_MESSAGE_STREAM)
    private _notificationMessageStream$: NotificationMessagesStream,
    private _ngZone: NgZone
  ) {}

  handleError(error: any): void {
    this._ngZone.run(() => {
      if (error instanceof AppError) {
        of(error.messages)
          .pipe(
            map((msg) => ([] as string[]).concat(msg)),
            map((msgs) =>
              msgs.map<NotificationMessage>((m) => {
                return { message: m, status: NotificationMessageStatus.Error };
              })
            )
          )
          .subscribe((notifMsgs) =>
            this._notificationMessageStream$.next(notifMsgs)
          );
      } else
        this._notificationMessageStream$.next({
          message: error,
          status: NotificationMessageStatus.Error,
        });
    });
  }
}

export const appErrorHandlerProvider: ClassProvider = {
  provide: ErrorHandler,
  useClass: AppErrorHandler,
};
