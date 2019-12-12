import { Component } from '@angular/core';
import { UserService } from './services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'Festive Phonebook';

  constructor(public userService: UserService,
    private router: Router) {}

  Logout() {
    localStorage.setItem('token', '');
    this.router.navigateByUrl('/home');
  }
}
