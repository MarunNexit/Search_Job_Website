import {Component, OnInit} from '@angular/core';
import {HeaderStateService} from "../../services/header-searcher-state.service";
import {debounceTime, filter, Subject, Subscription, takeUntil} from "rxjs";
import {ActivatedRoute, Event as RouterEvent, NavigationStart, Router} from '@angular/router';
import {ScreenSizeService} from "../../services/screen-size.sevice";
import {UserHeader} from "../../models/user-header.model";
import {RouterHelperService} from "../../services/router-helper.service";
import {AuthService} from "../../models/backend/dtos/auth/auth-service";
import {handleImageError} from "../../functions/handleImageError";
import {AuthenticationClient} from "../../models/backend/dtos/auth/auth-client";
import {UserData} from "../../models/backend/dtos/auth/dtos/user-data";
import {UserService} from "../../services/backend/user.service";
import {MainSearchService} from "../../services/main-search.service";
import {ProfileDataService} from "../../services/backend/profile-data.service";
import {ResumeDTO} from "../../models/backend/dtos/profiles/resume.dto";
import {PopupPurpleComponent} from "../popup/popup-purple/popup-purple.component";
import {MatDialog} from "@angular/material/dialog";


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
  isCreateJobPage = false;
  isEditJobPage = false;
  isSearchBarFilled = false;
  FirstName = 'Я';
  LastName = '';
  userHeadData: UserData;
  userHeaderPanel: UserData;
  searchQuery: string = '';
  searchQuerySmall: string = '';
  currentUrl: string = '';
  id: string | null = '';
  profileData: ResumeDTO | null;
  isMyProfile: boolean = true;
  userRole: string = 'ANONYM';

  private paramSubscription: Subscription;

  constructor(
    private router: Router,
    private headerStateService: HeaderStateService,
    private screenSizeService: ScreenSizeService,
    private routerHelper: RouterHelperService,
    private authService: AuthService,
    private userClient: AuthenticationClient,
    private userService: UserService,
    private mainSearchService: MainSearchService,
    private route: ActivatedRoute,
    private routerHelperService: RouterHelperService,
    public dialog: MatDialog,
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
      this.currentUrl = event.url;
      if (event.url.startsWith('/search-job') || event.url === '/' || event.url === '/popular-employers') {
        this.isProfileUser = false;
        this.headerStateService.toggleHeaderSectionVisibilityMakeTrue();
        this.isAboutPage = false;
        this.isEditJobPage = false;
        this.isCreateJobPage = false;
      }
      else if (event.url === '/create-job') {
        this.isProfileUser = false;
        this.isCreateJobPage = true;
        this.isEditJobPage = false;
      }
      else if (event.url.startsWith('/create-job')) {
        this.isProfileUser = false;
        this.isCreateJobPage = false;
        this.isEditJobPage = true;
      }
      else if (event.url === '/about-us') {
        this.isProfileUser = false;
        this.headerStateService.toggleHeaderSectionVisibilityMakeTrue();
        this.isAboutPage = true;
        this.isEditJobPage = false;
        this.isCreateJobPage = false;
      }
      else if(event.url.startsWith('/profile')){
        this.isProfileUser = true;
        this.headerStateService.toggleHeaderSectionVisibilityMakeFalse();
        this.isAboutPage = false;
        console.log( event.url)
        let url_param =  event.url.split('/')
        console.log(url_param)
        const id = url_param[2];
        console.log(id);
        this.id = id;
        this.IsAuthUser();
        this.isEditJobPage = false;
        this.isCreateJobPage = false;
      }
      else{
        this.isProfileUser = false;
        this.headerStateService.toggleHeaderSectionVisibilityMakeFalse();
        this.isAboutPage = false;
        this.isEditJobPage = false;
        this.isCreateJobPage = false;
      }
    });
  }

  getUserData(): void {
    if(this.isLogin){
      this.authService.getRole().subscribe(role => {
        this.userRole = role;
      })

      if (this.userService.isUserDataEmpty()) {
        this.userClient.getUserData().subscribe(userData => {
          this.userHeadData = userData;
          this.userService.setUserData(userData);
          this.FirstName = this.userHeadData.firstName ? this.userHeadData.firstName : 'Я';
          this.LastName = this.userHeadData.lastName ? this.userHeadData.lastName : '';

          console.log(this.userHeadData)
          if(this.userHeadData && this.userHeadData.userImg && this.userHeadData.userImg != ""){
            this.isUserImage = true;
          }

          if(this.isProfileUser){
            console.log("THIS WORK");
            this.isMyProfile = true;
            if(this.id && this.id != userData.userId){
              console.log(this.id, userData.id)
              this.isMyProfile = false;
              this.userService.setAnotherUserData(this.id);
            }
          }
          else{
            this.userService.clearAnotherUserData();
          }
          this.userHeaderPanel = this.userHeadData;
          console.log(this.userHeadData);
        });
      }
      else{
      }
    }
  }


  getAnotherUserData(): void {
    if(!this.isLogin){
      console.log("I AM NOT LOGIN");
      if(this.userService.isAnotherUserDataEmpty()){
        console.log("Is Empty");
        if(this.isProfileUser){
          this.isMyProfile = false;
          if(this.id){
            console.log("I AM HERE")
            this.userService.setAnotherUserData(this.id);
            this.userService.getAnotherUserData().subscribe(userData => {
              if(userData){
                this.userHeaderPanel = userData;
                console.log("this.userHeaderPanel", this.userHeaderPanel)
              }
            });
          }
        }
        else {
          this.userService.clearAnotherUserData();
        }
      }
    }
  }


  ngOnInit(): void {
    this.IsAuthUser();
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
        this.getAnotherUserData();
      }
    });
  }

  isHeaderSectionVisible(): boolean {
    return this.headerStateService.getIsHeaderSectionVisible();
  }

  goToURL(s: string, b: boolean) {
    this.routerHelper.goToUrl(s, b);
  }

  newBadge = true;

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



  openDialogAnalytics(): void {
    if(this.userHeadData && this.userRole == "Searcher"){
      const dialogRef = this.dialog.open(PopupPurpleComponent, {
        width: '1000px',
        data: {type: "searcherAnalytics", title: "Аналітика діяльності", user: this.userHeadData}
      });

      dialogRef.afterClosed().subscribe(result => {
        console.log(result);
      });
    }
  }


}
