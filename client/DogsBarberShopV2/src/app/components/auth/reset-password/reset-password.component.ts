import { catchError, tap } from 'rxjs/operators';
import { AuthService } from './../../../services/auth.service';
import { Component, Inject, InjectionToken, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { confirmPasswordValidator } from 'src/app/dependencyInjection/providers/loginFormGroupFactory.provider';
import { throwError } from 'rxjs';

enum ResetPasswordMode {
  Forgot,
  Reset,
}

type FgCreator = (rm: ResetPasswordMode) => FormGroup;

function fgCreator(resetMode: ResetPasswordMode): FormGroup {
  let fg: FormGroup;

  if (resetMode === ResetPasswordMode.Forgot)
    fg = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
    });
  else
    fg = new FormGroup({
      password: new FormControl(null, [
        Validators.required,
        Validators.minLength(5),
      ]),
      confirmPassword: new FormControl(null, [
        Validators.required,
        Validators.minLength(5),
        confirmPasswordValidator,
      ]),
    });

  return fg;
}

const RESET_PASSWORD_FG_CREATOR = new InjectionToken<FgCreator>(
  'password reset form group creator'
);

@Component({
  selector: 'reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss'],
  providers: [{ provide: RESET_PASSWORD_FG_CREATOR, useValue: fgCreator }],
})
export class ResetPasswordComponent implements OnInit {
  private _token!: string;
  private _email!: string;

  fg!: FormGroup;
  resetMode = ResetPasswordMode.Forgot;
  headerText = {
    [ResetPasswordMode.Forgot]: 'Password assistance',
    [ResetPasswordMode.Reset]: 'Choose new password',
  };
  isLoading = false;

  constructor(
    private _route: ActivatedRoute,
    private _authService: AuthService,
    @Inject(RESET_PASSWORD_FG_CREATOR) private _fgCreator: FgCreator
  ) {}

  ngOnInit(): void {
    const token = this._route.snapshot.queryParamMap.get('token');
    const email = this._route.snapshot.queryParamMap.get('email');
    if (token && email) {
      this.resetMode = ResetPasswordMode.Reset;
      this._token = token;
      this._email = email;
    }
    this.fg = this._fgCreator(this.resetMode);
  }

  getControl(name: string): FormControl {
    return this.fg.get(name) as FormControl;
  }

  onSubmit(): void {
    if (this.fg.valid) {
      this.isLoading = true;
      if (this.resetMode === ResetPasswordMode.Forgot)
        this._authService
          .forgotPassword(this.fg.value['email'])
          .pipe(
            tap(() => (this.isLoading = false)),
            catchError((err) => {
              this.isLoading = false;
              return throwError(err);
            })
          )
          .subscribe();
      else
        this._authService
          .resetPassword(this.fg.value, this._token, this._email)
          .pipe(
            tap(() => (this.isLoading = false)),
            catchError((err) => {
              this.isLoading = false;
              return throwError(err);
            })
          )
          .subscribe();
    }
  }
}
