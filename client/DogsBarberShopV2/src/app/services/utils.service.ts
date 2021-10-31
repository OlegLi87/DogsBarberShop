import { BehaviorSubject } from 'rxjs';
import { APP_CONFIG_STREAM } from './../dependencyInjection/providers/appConfig.provider';
import { DOCUMENT } from '@angular/common';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class UtilsService {
  _appConfig: any;

  constructor(
    @Inject(APP_CONFIG_STREAM) private appConfigStream$: BehaviorSubject<any>,
    @Inject(DOCUMENT) private _document: Document
  ) {
    this._appConfig = this.appConfigStream$.value;
  }

  getCurrentBreakpoint(): string {
    const bodyWidth = this._document.body.clientWidth;
    const breakPoints = this._appConfig.style.breakPoints;

    if (bodyWidth < breakPoints.md) return 'sm';
    if (bodyWidth < breakPoints.lg) return 'md';
    if (bodyWidth < breakPoints.xlg) return 'lg';
    if (bodyWidth < breakPoints.xxlg) return 'xlg';
    return 'xxlg';
  }
}
