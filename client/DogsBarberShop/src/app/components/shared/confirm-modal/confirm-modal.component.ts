import { ConfirmMessage } from '../../../models/ConfirmMessage';
import { ConfirmMessageStream } from '../../../infastructure/di_providers/confirmMessageStream.provider';
import {
  ChangeDetectionStrategy,
  Component,
  HostBinding,
  Inject,
  OnInit,
} from '@angular/core';
import { CONFIRM_MESSAGE_STREAM } from 'src/app/infastructure/di_providers/confirmMessageStream.provider';

@Component({
  selector: 'confirm-modal',
  templateUrl: './confirm-modal.component.html',
  styleUrls: ['./confirm-modal.component.sass'],
  changeDetection: ChangeDetectionStrategy.Default,
})
export class ConfirmModalComponent implements OnInit {
  confirmMessage!: ConfirmMessage;

  @HostBinding('class.visible') isVisible = false;

  constructor(
    @Inject(CONFIRM_MESSAGE_STREAM)
    private _confirmMessageStream$: ConfirmMessageStream
  ) {}

  ngOnInit(): void {
    this._confirmMessageStream$.subscribe((cm) => {
      this.isVisible = true;
      this.confirmMessage = cm;
    });
  }

  onAnswerClicked(response: boolean): void {
    this.isVisible = false;
    if (response) this.confirmMessage.confirmAction();
    else this.confirmMessage.rejectAction();
  }
}
