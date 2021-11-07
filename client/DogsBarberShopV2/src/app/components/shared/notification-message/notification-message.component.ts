import { SlideOnAdd } from './../../../animations/slideOnAddanimation';
import {
  NOTIFICATION_MESSAGE_STREAM,
  NotificationMessagesStream,
} from './../../../dependencyInjection/tokens/notificationMessageStream.diToken';
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  ElementRef,
  Inject,
  OnInit,
  ViewChild,
} from '@angular/core';
import { NotificationMessage } from 'src/app/models/notificationMessage';
import { watch } from '@taiga-ui/cdk';
import { timer } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'notification-message',
  templateUrl: './notification-message.component.html',
  styleUrls: ['./notification-message.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  animations: [SlideOnAdd],
})
export class NotificationMessageComponent implements OnInit {
  private _displayDuration = 5000;
  notificationMessages!: NotificationMessage[] | null;

  constructor(
    @Inject(NOTIFICATION_MESSAGE_STREAM)
    private _notificationMessagesStream$: NotificationMessagesStream,
    private _changeDetectorRef: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this._notificationMessagesStream$
      .pipe(
        watch(this._changeDetectorRef),
        map((msg) => ([] as NotificationMessage[]).concat(msg))
      )
      .subscribe((msgs) => {
        this.notificationMessages = msgs;
        timer(this._displayDuration)
          .pipe(watch(this._changeDetectorRef))
          .subscribe(() => (this.notificationMessages = null));
      });
  }
}
