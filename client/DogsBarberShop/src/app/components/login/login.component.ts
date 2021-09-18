import { UserCredentials } from './../../models/UserCredentials';
import { AuthService } from './../../services/auth.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { LoginMode } from 'src/app/models/LoginMode';
import { NgForm } from '@angular/forms';
import { catchError, tap } from 'rxjs/operators';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass'],
})
export class LoginComponent implements OnInit {
  userCredentials: UserCredentials = new UserCredentials();
  loginMode: LoginMode = LoginMode.SignUp;
  isLoading = false;

  titleTexts = {
    [LoginMode.SignUp]: 'Sign Up',
    [LoginMode.SignIn]: 'Sign In',
  };

  toggleModeTexts = {
    [LoginMode.SignUp]: 'Allready have an account?',
    [LoginMode.SignIn]: 'New user?',
  };

  @ViewChild('form') form!: NgForm;

  constructor(private _authService: AuthService) {}

  ngOnInit(): void {}

  toggleMode(): void {
    if (this.loginMode === LoginMode.SignUp) this.loginMode = LoginMode.SignIn;
    else this.loginMode = LoginMode.SignUp;

    this.form.resetForm();
  }

  onSubmit(): void {
    if (this.form.invalid) return;
    this.isLoading = true;
    this._authService
      .login(this.userCredentials, this.loginMode)
      .pipe(
        tap(() => (this.isLoading = false)),
        catchError((err) => {
          this.isLoading = false;
          throw err;
        })
      )
      .subscribe();
  }

  onReset(): void {
    this.userCredentials = new UserCredentials();
    this.form.resetForm();
  }
}
