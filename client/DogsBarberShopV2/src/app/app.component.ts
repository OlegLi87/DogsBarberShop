import { NavigationManagerService } from './services/navigationManager.service';
import { Component } from '@angular/core';

@Component({
  selector: 'root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  constructor(private _navigationManager: NavigationManagerService) {}
}
