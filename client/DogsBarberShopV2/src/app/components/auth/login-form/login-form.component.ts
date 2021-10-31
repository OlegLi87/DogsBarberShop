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
    private _utils: UtilsService,
    @Inject(LOGIN_FG_FACTORY) private _fgFactory: LoginFgFactory
  ) {}

  ngOnInit(): void {
    this.loginFg = this._fgFactory(this.loginMode);
    this.loginModeToggledStream$.subscribe(this.clearState.bind(this));
    this._currentBreakPoint = this._utils.getCurrentBreakpoint();
  }

  onSubmit(): void {
    this.wasSubmitted = true;
    if (this.loginFg.valid) {
      console.log(this.loginFg.value);
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
