import { ConfirmMessageStream } from './../../infastructure/di_providers/confirmMessageStream.provider';
import { AuthService } from '../../services/auth.service';
import { Component, Inject, OnInit } from '@angular/core';
import { CONFIRM_MESSAGE_STREAM } from 'src/app/infastructure/di_providers/confirmMessageStream.provider';
import { ConfirmMessage } from 'src/app/models/ConfirmMessage';

@Component({
  selector: 'main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.sass'],
})
export class MainComponent implements OnInit {
  constructor(
    private _authService: AuthService,
    @Inject(CONFIRM_MESSAGE_STREAM)
    private _confirmMessageStream$: ConfirmMessageStream
  ) {}

  ngOnInit(): void {}

  logout(): void {
    this._confirmMessageStream$.next(
      new ConfirmMessage(
        'Are you sure to logout?',
        this._authService.logout.bind(this._authService),
        () => {}
      )
    );
  }
}
