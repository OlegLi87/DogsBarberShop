import { Message, MessageStatus } from './../models/Message';
import { Injectable } from '@angular/core';

import stc from 'string-to-color';
import Color from 'color';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class UtilsService {
  // not using it currently,keeping for optinal use
  convertColorToRgba(color: string, alpha: number): string {
    const hex = stc(color);
    return Color(hex).alpha(alpha).toString();
  }

  createMessageFromHttpErrorResponse(error: HttpErrorResponse): Message {
    let status: MessageStatus;
    let messages: string[] = [];

    if (!error.status) {
      status = MessageStatus.Error;
      messages.push('Connection failed.Please try again later.');
    } else if (error.status >= 500) {
      status = MessageStatus.Error;
      messages.push('Internal server error.Please try again later.');
    } else {
      status = MessageStatus.Warning;
      messages = messages.concat(JSON.parse(error.error)?.errors);
    }

    return new Message(messages, status);
  }
}
