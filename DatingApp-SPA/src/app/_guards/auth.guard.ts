import { AlertifyService } from './../_services/alertify.service';
import { AuthService } from './../_services/auth.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authservice: AuthService, private router: Router, private alertify: AlertifyService) {}

  canActivate(next: ActivatedRouteSnapshot): boolean {
    const roles = next.firstChild.data.roles as Array<string>;
    if (roles) {
      const match = this.authservice.roleMatch(roles);
      if (match) {
        return true;
      } else {
        this.router.navigate(['members']);
        this.alertify.error('You\'re not authorized to access this area.');
      }
    }
    if (this.authservice.loggedIn()) {
      return true;
    }

    this.alertify.error('You shall not pass!!!');
    this.router.navigate(['/home']);
    return false;
  }
}
