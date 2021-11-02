import {
  NOTIFICATION_MESSAGE_STREAM,
  NotificationMessageStream,
} from './../../../dependencyInjection/tokens/notificationMessageStream.diToken';
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  Inject,
  OnInit,
} from '@angular/core';
import { NotificationMessage } from 'src/app/models/notificationMessage';
import { watch } from '@taiga-ui/cdk';
import { timer } from 'rxjs';

@Component({
  selector: 'notification-message',
  templateUrl: './notification-message.component.html',
  styleUrls: ['./notification-message.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NotificationMessageComponent implements OnInit {
  private _displayDuration = 10000;
  notificationMessage!: NotificationMessage | null;

  constructor(
    @Inject(NOTIFICATION_MESSAGE_STREAM)
    private _notificationMessageStream$: NotificationMessageStream,
    private _changeDetectorRef: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this._notificationMessageStream$
      .pipe(watch(this._changeDetectorRef))
      .subscribe((msg) => {
        this.notificationMessage = msg;
        timer(this._displayDuration)
          .pipe(watch(this._changeDetectorRef))
          .subscribe(() => (this.notificationMessage = null));
      });
  }
}
