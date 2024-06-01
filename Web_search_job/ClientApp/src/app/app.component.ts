import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs/operators';
import { LastPageService } from './services/last-page.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'app';
  private authGuard: any;
  constructor(private router: Router, private lastPageService: LastPageService) {}
  ngOnInit(): void {
    /*this.router.navigate([this.lastPageService.getLastPage()]);
    this.router.events
      .pipe(
        filter((event): event is NavigationEnd => event instanceof NavigationEnd)
      )
      .subscribe(event => {
        this.lastPageService.setLastPage(event.urlAfterRedirects);
        console.log(this.lastPageService.getLastPage())
      });*/
  }


  /*ngOnInit(): void {
    this.authGuard.checkAuthOnAppLoad().subscribe(
      (isAuthenticated: boolean) => {
        if (!isAuthenticated) {
          console.log("NON")
        }
      }
    );
  }*/

}
