import { Component, OnInit } from '@angular/core';
import {HeaderStateService} from "../../services/header-searcher-state.service";
import {debounceTime, filter, Subject, Subscription, takeUntil} from "rxjs";
import {Router, Event as RouterEvent, NavigationEnd, NavigationStart} from '@angular/router';
import {ScreenSizeService} from "../../services/screen-size.sevice";
import {UserHeader} from "../../models/user-header.model";
import {RouterHelperService} from "../../services/router-helper.service";
import {AuthService} from "../../services/backend/auth/auth-service";
import {handleImageError} from "../../functions/handleImageError";
import {AuthenticationClient} from "../../services/backend/auth/auth-client";
import {UserId} from "../../services/backend/auth/dtos/user-id";
import {UserData} from "../../services/backend/auth/dtos/user-data";
import {UserService} from "../../services/backend/user.service";
import {MainSearchService} from "../../services/main-search.service";


@Component({
  selector: 'app-header-searcher',
  templateUrl: './header-searcher.component.html',
  styleUrls: ['./header-searcher.component.scss'],
})
export class HeaderSearcherComponent implements OnInit {
  isExpanded = false;
  isScreenSmall = false;
  isLogin = false;
  isUserImage = false;
  isSideBarRight = false;
  isProfileUser = false;
  isAboutPage = false;
  isSearchBarFilled = false;

  userData = {
    id: 1,
    firstName:'Софія',
    lastName: 'Мацькевич',
    email:'ф',
    image:'',
  }

  user: UserHeader = this.userData;

  userHeadData: UserData;
  searchQuery: string = '';
  searchQuerySmall: string = '';

  private paramSubscription: Subscription;

  constructor(
    private router: Router,
    private headerStateService: HeaderStateService,
    private screenSizeService: ScreenSizeService,
    private routerHelper: RouterHelperService,
    private authService: AuthService,
    private userClient: AuthenticationClient,
    private userService: UserService,
    private mainSearchService: MainSearchService

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
      if (event.url.startsWith('/search-job') || event.url === '/' || event.url === '/popular-employers') {
        this.isProfileUser = false;
        this.headerStateService.toggleHeaderSectionVisibilityMakeTrue();
        this.isAboutPage = false;
      }
      else if (event.url === '/about-us') {
        this.isProfileUser = false;
        this.headerStateService.toggleHeaderSectionVisibilityMakeTrue();
        this.isAboutPage = true;
      }
      else if(event.url.startsWith('/profile/')){
        this.isProfileUser = true;
        this.headerStateService.toggleHeaderSectionVisibilityMakeFalse();
        this.isAboutPage = false;
      }
      else{
        this.isProfileUser = false;
        this.headerStateService.toggleHeaderSectionVisibilityMakeFalse();
        this.isAboutPage = false;
      }
    });
  }

  getUserData(): void {
    if(this.isLogin){
      if (this.userService.isUserDataEmpty()) {
        this.userClient.getUserData().subscribe(userData => {
          this.userHeadData = userData;
          this.userService.setUserData(userData);
        });
      }
      else{
      }
    }
  }

  ngOnInit(): void {
    this.IsAuthUser();
    this.getUserData()

    this.paramSubscription = this.mainSearchService.getParamObservable().pipe(debounceTime(300)).subscribe(params => {
      const { sort, filter, request } = params;
      this.searchQuery = params.request || '';
      console.log(params, this.searchQuery, params.request);

      if (sort && filter && request) {
        this.searchQuery = request;
        this.searchQuerySmall = request;
      }

      if( this.searchQuery != ''){
        this.isSearchBarFilled = true
      }
      else{
        this.isSearchBarFilled = false
      }
    });

    if(this.user && this.user.image && this.user.image != ""){
      this.isUserImage = true;
    }
  }

  activeMenuItem: string = "item0";
  private unsubscribe$: Subject<void> = new Subject<void>();

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }


  setActiveMenuItem(id: string): void {
    this.activeMenuItem = id;
    localStorage.setItem('activeMenuItem', JSON.stringify(id));
  }


  IsAuthUser(): void {
    this.authService.isLoggedIn().subscribe(isLoggedIn => {
      this.isLogin = isLoggedIn;
      if (isLoggedIn) {
        this.getUserData();
      } else {
        // Користувач не увійшов у систему, виконуйте необхідні дії
      }
    });
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

  goToURL(s: string, b: boolean) {
    this.routerHelper.goToUrl(s, b);
  }

  newBadge = true;

  toggleBadgeVisibility() {
    this.newBadge = !this.newBadge;
  }

  protected readonly handleImageError = handleImageError;

  emptySearchBar(){
    this.isSearchBarFilled = false;

    if(this.searchQuery !=''){
      this.mainSearchService.setParamsRequest("");
    }
  }

  onSearchBarSubmit(){
    if(!this.activeMenuItem.startsWith("/search-job")) {
      console.log('/search-job');
      this.goToURL('/search-job', true);
    }

    if(this.searchQuerySmall != ""){
      this.isSearchBarFilled = true;
      this.searchQuery = this.searchQuerySmall;
    }

    this.mainSearchService.setParamsRequest(this.searchQuery);
  }
}
