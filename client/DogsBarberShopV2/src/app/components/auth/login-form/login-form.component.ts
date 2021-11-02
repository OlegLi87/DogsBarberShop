import { AuthService } from './../../../services/auth.service';
import { FormGroup, FormControl } from '@angular/forms';
import {
  loginFormGroupFactory,
  LoginFgFactory,
  LOGIN_FG_FACTORY,
} from './../../../dependencyInjection/providers/loginFormGroupFactory.provider';
import { Component, Inject, Input, OnInit } from '@angular/core';
import { LoginMode } from '../login-page/login-page.component';
import { Observable } from 'rxjs';
import { UtilsService } from 'src/app/services/utils.service';
import {
  NOTIFICATION_MESSAGE_STREAM,
  NotificationMessageStream,
} from 'src/app/dependencyInjection/tokens/notificationMessageStream.diToken';
import { NotificationMessageStatus } from 'src/app/models/notificationMessage';

@Component({
  selector: 'login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss'],
  providers: [loginFormGroupFactory],
})
export class LoginFormComponent implements OnInit {
  @Input() loginMode!: LoginMode;
  @Input() loginModeToggledStream$!: Observable<boolean>;

  loginFg!: FormGroup;
  wasSubmitted = false;
  formTitles = {
    [LoginMode.SignIn]: 'Sign In',
    [LoginMode.SignUp]: 'Sign Up',
  };
  _currentBreakPoint!: string;

  constructor(
    private _authService: AuthService,
    private _utils: UtilsService,
    @Inject(LOGIN_FG_FACTORY) private _fgFactory: LoginFgFactory,
    @Inject(NOTIFICATION_MESSAGE_STREAM)
    private _notificationMessageStream$: NotificationMessageStream
  ) {}

  ngOnInit(): void {
    this.loginFg = this._fgFactory(this.loginMode);
    this.loginModeToggledStream$.subscribe(this.clearState.bind(this));
    this._currentBreakPoint = this._utils.getCurrentBreakpoint();
  }

  onSubmit(): void {
    this.wasSubmitted = true;
    if (this.loginFg.valid) {
      //this._authService.login(this.loginFg.value, this.loginMode);
      this._notificationMessageStream$.next({
        message:
          'To complete registration proccess,please follow a link that was sent to your email address.',
        status: NotificationMessageStatus.Information,
      });
    }
  }

  getControl(name: string): FormControl {
    return this.loginFg.get(name) as FormControl;
  }

  clearState(): void {
    this.loginFg.reset();
    this.wasSubmitted = false;
  }

  get tuiElementSize(): 's' | 'm' | 'l' {
    return this._currentBreakPoint === 'sm' || this._currentBreakPoint === 'md'
      ? 's'
      : 'm';
  }
}
