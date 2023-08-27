import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private router: Router) { }

  canActivate() {
    if (localStorage.getItem('currentUser')) {
        // Logged in so return true
        return true;
    }

    // Not logged in so redirect to login page
    this.router.navigate(['/login']);
    return false;
  }
}
