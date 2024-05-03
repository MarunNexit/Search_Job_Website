import { Component, OnInit } from '@angular/core';
import {HeaderStateService} from "../../services/header-searcher-state.service";
import {filter, Subject, takeUntil} from "rxjs";
import {Router, Event as RouterEvent, NavigationEnd, NavigationStart} from '@angular/router';
import {ScreenSizeService} from "../../services/screen-size.sevice";
import {UserHeader} from "../../models/user-header.model";


@Component({
  selector: 'app-header-searcher',
  templateUrl: './header-searcher.component.html',
  styleUrls: ['./header-searcher.component.scss'],
})
export class HeaderSearcherComponent implements OnInit {
  isExpanded = false;
  isScreenSmall = false;
  isLogin = true;
  isUserImage = false;
  isSideBarRight = false;


  userData = {
    id: 1,
    firstName:'Софія',
    lastName: 'Мацькевич',
    email:'ф',
    image:'',
  }
  /*userFetching*/
  /*constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getUserData().subscribe(
      (userData: User) => {
        this.user = userData;
      },
      (error) => {
        console.error('Error fetching user data:', error);
      }
    );
  } */

  user: UserHeader;

  ngOnInit(): void {
    this.user = this.userData;

    if(this.user.image && this.user.image != ""){
      this.isUserImage = true;
    }
  }




  activeMenuItem: string = "item0";
  private unsubscribe$: Subject<void> = new Subject<void>();

  constructor(
    private router: Router,
    private headerStateService: HeaderStateService,
    private screenSizeService: ScreenSizeService,
  ) {
    const storedValue = localStorage.getItem('activeMenuItem');
    if (storedValue !== null) {
      this.activeMenuItem = JSON.parse(storedValue);
    }


    this.screenSizeService.setIsSmallScreen('(max-width: 1200px)');


    this.screenSizeService.isScreenSmall$.subscribe(isSmall => {
      this.isScreenSmall = isSmall;
    });

    this.router.events.pipe(
      filter((event: RouterEvent): event is NavigationStart => event instanceof NavigationStart),
      takeUntil(this.unsubscribe$)
    ).subscribe((event: NavigationStart) => {
      this.activeMenuItem = event.url;
      if (event.url === '/search-job' || event.url === '/' || event.url === '/popular-employers') {
        this.headerStateService.toggleHeaderSectionVisibilityMakeTrue();
      }
      else{
        this.headerStateService.toggleHeaderSectionVisibilityMakeFalse();
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




  newBadge = true;

  toggleBadgeVisibility() {
    this.newBadge = !this.newBadge;
  }

}
