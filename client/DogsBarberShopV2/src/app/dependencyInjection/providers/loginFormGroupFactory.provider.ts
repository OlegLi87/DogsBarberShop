import {
  AbstractControl,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { InjectionToken, ValueProvider } from '@angular/core';
import { LoginMode } from 'src/app/components/auth/login-page/login-page.component';

type Controls = { [key: string]: AbstractControl };

export const confirmPasswordValidator: ValidatorFn = (
  control: AbstractControl
) => {
  const password = control.parent?.get('password')?.value;
  const confirmPassword = control.value as string;

  if (password === confirmPassword || confirmPassword?.length < 5) return null;
  return { mismatch: true };
};

const loginFgFactory = (loginMode: LoginMode) => {
  const controls: Controls = {};

  controls.userName = new FormControl(null, [
    Validators.required,
    Validators.minLength(2),
  ]);

  controls.password = new FormControl(null, [
    Validators.required,
    Validators.minLength(5),
  ]);

  if (loginMode === LoginMode.SignUp) {
    controls.confirmPassword = new FormControl(null, [
      Validators.required,
      Validators.minLength(5),
      confirmPasswordValidator,
    ]);

    controls.email = new FormControl(null, [
      Validators.required,
      Validators.email,
    ]);

    controls.firstName = new FormControl(null, [
      Validators.required,
      Validators.minLength(2),
    ]);
  }

  return new FormGroup(controls);
};

export type LoginFgFactory = (loginMode: LoginMode) => FormGroup;
export const LOGIN_FG_FACTORY = new InjectionToken('login formGroup factory');

export const loginFormGroupFactory: ValueProvider = {
  provide: LOGIN_FG_FACTORY,
  useValue: loginFgFactory,
};
