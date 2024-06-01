import { Injectable } from '@angular/core';
import {NavigationExtras, Router} from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class RouterHelperService {

  constructor(private router: Router) {}

  goToUrl(url: string, scrolToTop: boolean) {
    this.router.navigateByUrl(url).then(() => {
      if(scrolToTop){
        window.scrollTo(0, 0);
      }
    });
  }

  goToUrlWithParam(url: string, scrolToTop: boolean, param: string) {
    const navigationExtras: NavigationExtras = {
      queryParams: { role: param }
    };
    this.router.navigate([url], navigationExtras).then(() => {
      if (scrolToTop) {
        window.scrollTo(0, 0);
      }
    });
  }
}
