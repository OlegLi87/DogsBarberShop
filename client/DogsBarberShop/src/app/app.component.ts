import { Component } from '@angular/core';
import { NavigationService } from './services/navigation.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass'],
  providers: [NavigationService],
  host: {
    class: 'app-main',
  },
})
export class AppComponent {
  title = 'DogsBarberShop';

  constructor(private navigationService: NavigationService) {}
}
