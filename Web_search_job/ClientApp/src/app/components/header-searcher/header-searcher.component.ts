import { Component } from '@angular/core';
import {HeaderStateService} from "../../services/header-searcher-state.service";
import {filter, Subject, takeUntil} from "rxjs";
import { Router, Event as RouterEvent, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-header-searcher',
  templateUrl: './header-searcher.component.html',
  styleUrls: ['./header-searcher.component.scss']
})
export class HeaderSearcherComponent {
  isExpanded = false;
  activeMenuItem: string = "item0";
  private unsubscribe$: Subject<void> = new Subject<void>();

  constructor(
    private router: Router,
    private headerStateService: HeaderStateService
  ) {
    const storedValue = localStorage.getItem('activeMenuItem');
    if (storedValue !== null) {
      this.activeMenuItem = JSON.parse(storedValue);
    }

    this.router.events.pipe(
      filter((event: RouterEvent): event is NavigationEnd => event instanceof NavigationEnd),
      takeUntil(this.unsubscribe$)
    ).subscribe((event: NavigationEnd) => {
      if (event.url === '/login-register') {
        this.headerStateService.toggleHeaderSectionVisibilityMakeFalse();
      }
      else{
        this.headerStateService.toggleHeaderSectionVisibilityMakeTrue();
      }
    });
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }


  setActiveMenuItem(id: string): void {
    this.activeMenuItem = id;
    localStorage.setItem('activeMenuItem', JSON.stringify(id));
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

 /* ngOnInit(): void {
    this.toggleHeaderSectionVisibility();
  }

  toggleHeaderSectionVisibility() {
    this.headerStateService.toggleHeaderSectionVisibilityMakeFalse();
  }*/

  isHeaderSectionVisible(): boolean {
    return this.headerStateService.getIsHeaderSectionVisible();
  }
}
