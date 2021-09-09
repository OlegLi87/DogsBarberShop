import { ConfirmMessage } from '../../../models/ConfirmMessage';
import { ConfirmMessageStream } from '../../../infastructure/di_providers/confirmMessageStream.provider';
import { Component, HostBinding, Inject, OnInit } from '@angular/core';
import { CONFIRM_MESSAGE_STREAM } from 'src/app/infastructure/di_providers/confirmMessageStream.provider';

@Component({
  selector: 'confirm-modal',
  templateUrl: './confirm-modal.component.html',
  styleUrls: ['./confirm-modal.component.sass'],
})
export class ConfirmModalComponent implements OnInit {
  confirmMessage!: ConfirmMessage;

  @HostBinding('style.visibility') display = 'hidden';

  constructor(
    @Inject(CONFIRM_MESSAGE_STREAM)
    private _confirmMessageStream$: ConfirmMessageStream
  ) {}

  ngOnInit(): void {
    this._confirmMessageStream$.subscribe((cm) => {
      this.display = 'visible';
      this.confirmMessage = cm;
    });
  }

  onAnswerClicked(response: boolean): void {
    this.display = 'hidden';
    if (response) this.confirmMessage.confirmAction();
    else this.confirmMessage.rejectAction();
  }
}
