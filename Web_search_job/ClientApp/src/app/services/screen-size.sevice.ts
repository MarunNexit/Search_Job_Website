import {Injectable, Input, OnInit} from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ScreenSizeService {
  isScreenSmall$: Observable<boolean>;
  screenMax: string = '';

  setIsSmallScreen(size: string){
    this.screenMax = size;

    if(this.screenMax && this.screenMax != "")
    {
      this.isScreenSmall$ = this.breakpointObserver.observe(this.screenMax)
        .pipe(
          map(result => result.matches)
        );
    }
    else{
      this.isScreenSmall$ = this.breakpointObserver.observe([Breakpoints.Small, Breakpoints.XSmall])
        .pipe(
          map(result => result.matches)
        );
    }
  }


  constructor(private breakpointObserver: BreakpointObserver) {
    console.log(this.screenMax)
    this.isScreenSmall$ = this.breakpointObserver.observe([Breakpoints.Small, Breakpoints.XSmall])
      .pipe(
        map(result => result.matches)
      );

  }
}
