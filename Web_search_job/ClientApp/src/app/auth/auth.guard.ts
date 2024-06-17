import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { AuthService } from '../models/backend/dtos/auth/auth-service';
import { combineLatest, map, Observable } from 'rxjs';
import {RouterHelperService} from "../services/router-helper.service";
import {UserService} from "../services/backend/user.service";
import {UserData} from "../models/backend/dtos/auth/dtos/user-data";

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {

  constructor(
    private authService: AuthService,
    private userService: UserService,
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

          if (route.routeConfig?.path === 'logout') {
            return true;
          }

          this.userService.getUserData().subscribe(userData => {
            if(userData && userData.id != null){
              if (route.data['roles'] && route.data['roles'].indexOf(role) === -1) {
                this.routerHelperService.goToUrl('/', true);
                return false;
              }

              console.log(role)

              if (route.routeConfig?.path === 'profile' && !route.params['id'] && role === 'Employer') {
                this.routerHelperService.goToUrl(`/profile-employer/${userData.userId}`, true);
                return false;
              }

              if (route.routeConfig?.path === 'profile' && !route.params['id']) {
                this.routerHelperService.goToUrl(`/profile/${userData.userId}`, true);
                return false;
              }

              return true;
            }
            else{
              console.log("OK")
              console.log(route.routeConfig?.path)

              if(userData && userData.userId){
                if (route.routeConfig?.path === 'profile' && !route.params['id']) {
                  this.routerHelperService.goToUrl(`/profile/${userData.userId}`, true);
                  return false;
                }

                if(route.routeConfig?.path?.startsWith('profile') ){
                  console.log('IS PROFILE')
                  return true;
                }
                this.routerHelperService.goToUrl(`/profile/${userData.userId}`, true);
                return true;
              }
              else{
                return false;
              }
            }
          });
        }
        return true;
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
