import {
  AppConfigStream,
  APP_CONFIG_STREAM,
} from './../dependencyInjection/tokens/appConfig.diToken';
import { DOCUMENT } from '@angular/common';
import { Inject, Injectable } from '@angular/core';
import { AppConfig } from '../models/appConfig';

@Injectable({
  providedIn: 'root',
})
export class UtilsService {
  _appConfig: AppConfig;

  constructor(
    @Inject(APP_CONFIG_STREAM) private appConfigStream$: AppConfigStream,
    @Inject(DOCUMENT) private _document: Document
  ) {
    this._appConfig = this.appConfigStream$.value as AppConfig;
  }

  getCurrentBreakpoint(): string {
    const bodyWidth = this._document.body.clientWidth;
    const breakPoints = this._appConfig.style.breakpoints;

    if (bodyWidth < breakPoints.md) return 'sm';
    if (bodyWidth < breakPoints.lg) return 'md';
    if (bodyWidth < breakPoints.xlg) return 'lg';
    if (bodyWidth < breakPoints.xxlg) return 'xlg';
    return 'xxlg';
  }
}
