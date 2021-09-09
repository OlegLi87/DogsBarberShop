import { UtilsService } from './../../services/utils.service';
import { MessageStatus } from './../../models/Message';
import { MessagesStream, MESSAGES_STREAM } from './messagesStream.provider';
import { ClassProvider, ErrorHandler, Injectable, NgZone } from '@angular/core';
import { Inject } from '@angular/core';
import { Message } from 'src/app/models/Message';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable()
class AppErrorHandler implements ErrorHandler {
  constructor(
    @Inject(MESSAGES_STREAM) private _messagesStream$: MessagesStream,
    private _utils: UtilsService,
    private _ngZone: NgZone
  ) {}

  handleError(error: any): void {
    let message: Message;

    if (error instanceof HttpErrorResponse)
      message = this._utils.createMessageFromHttpErrorResponse(error);
    else if (error instanceof Error)
      message = new Message([error.message], MessageStatus.Error);
    else if (typeof error === 'string')
      message = new Message([error], MessageStatus.Error);

    this._ngZone.run(() => this._messagesStream$.next(message));
  }
}

export const errorHandlerProvider: ClassProvider = {
  provide: ErrorHandler,
  useClass: AppErrorHandler,
};
