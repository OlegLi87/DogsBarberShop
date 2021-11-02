import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { UtilsService } from 'src/app/services/utils.service';

export enum LoginMode {
  SignIn,
  SignUp,
}

@Component({
  selector: 'login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginPageComponent implements OnInit {
  loginMode = LoginMode.SignUp;
  loginModeToggledStream$ = new Subject<boolean>();
  emailConfirmationSent = true;

  private _currentBreakPoint!: string;

  constructor(private _utils: UtilsService) {}

  ngOnInit(): void {
    this._currentBreakPoint = this._utils.getCurrentBreakpoint();
  }

  toggleLoginMode(mode: string): void {
    if (mode === 'signin') this.loginMode = LoginMode.SignIn;
    else this.loginMode = LoginMode.SignUp;

    this.loginModeToggledStream$.next(true);
  }

  get tuiElementSize(): 's' | 'm' | 'l' {
    return this._currentBreakPoint === 'sm' || this._currentBreakPoint === 'md'
      ? 'm'
      : 'l';
  }
}
