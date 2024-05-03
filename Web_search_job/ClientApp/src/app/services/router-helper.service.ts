import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

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
}
