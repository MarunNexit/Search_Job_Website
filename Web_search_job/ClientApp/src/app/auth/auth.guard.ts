import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/backend/auth/auth-service';
import { combineLatest, map, Observable } from 'rxjs';
import {RouterHelperService} from "../services/router-helper.service";

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private router: Router,
    private routerHelperService:RouterHelperService,
  ) {}

  canActivate(route: ActivatedRouteSnapshot): Observable<boolean> {
    return combineLatest([
      this.authService.isLoggedIn(),
      this.authService.getRole(),
    ]).pipe(
      map(([isLogged, role]) => {
        console.log(isLogged, route.data['roles'], route.data['roles'].indexOf(role));

        if(!isLogged){
          if (route.data['allowAnonymous']) {
            return true;
          }
          this.routerHelperService.goToUrl('/auth-login', true);
          return false;
        }
        else if (isLogged) {
          console.log(isLogged, route.data['roles'], route.data['roles'].indexOf(role));
          if (route.data['roles'] && route.data['roles'].indexOf(role) === -1) {
            this.routerHelperService.goToUrl('/', true);
            return false;
          }
          return true;
        }

        this.routerHelperService.goToUrl('/auth-login', true);
        return false;
      })
    );
  }

  checkAuthOnAppLoad(): Observable<boolean> {
    return combineLatest([
      this.authService.isLoggedIn(),
      this.authService.getRole(),
    ]).pipe(
      map(([isLogged, role]) => {
        if (!isLogged) {
          console.log("asasasas")
          this.routerHelperService.goToUrl('/auth-login', true);
          return false;
        }
        console.log("111111")
        return true;
      })
    );
  }

}
