import {
  MessagesStream,
  MESSAGES_STREAM,
} from '../../../infastructure/di_providers/messagesStream.provider';
import { Component, Inject, OnInit } from '@angular/core';
import { Message, MessageStatus } from 'src/app/models/Message';
import { AppConfig } from 'src/app/infastructure/di_providers/appConfig.provider';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'message-toaster',
  templateUrl: './message-toaster.component.html',
  styleUrls: ['./message-toaster.component.sass'],
})
export class MessageToasterComponent implements OnInit {
  message!: Message | null;
  private readonly _displayTime = 5000;

  constructor(
    private router: Router,
    @Inject(MESSAGES_STREAM) private _messagesStream$: MessagesStream,
    @Inject('appConfig') private _appConfig: AppConfig
  ) {}

  ngOnInit(): void {
    this._messagesStream$.subscribe(this.saveMessage.bind(this));
    this.router.events
      .pipe(filter((e) => e instanceof NavigationEnd))
      .subscribe(() => (this.message = null));
  }

  saveMessage(message: Message): void {
    this.message = message;
    setTimeout(() => {
      this.message = null;
    }, this._displayTime);
  }

  get backGround(): string {
    switch (this.message?.status) {
      case MessageStatus.Success:
        return this._appConfig.colors.success;
      case MessageStatus.Warning:
        return this._appConfig.colors.warning;
      case MessageStatus.Error:
        return this._appConfig.colors.error;
      default:
        return this._appConfig.colors.success;
    }
  }
}
