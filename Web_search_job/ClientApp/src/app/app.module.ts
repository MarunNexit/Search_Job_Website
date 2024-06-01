import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import { RouterModule } from '@angular/router';
import {HeaderSearcherComponent} from "./components/header-searcher/header-searcher.component";
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './pages/home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import {NgOptimizedImage} from "@angular/common";
import {ChipJobListComponent} from "./components/lists/chip-job-list/chip-job-list.component";
import {FooterSearcherComponent} from "./components/footer-searcher/footer-searcher.component";
import {HomeModule} from "./pages/home/home.module";
import {PopularEmployersPageComponent} from "./pages/popular-employers-page/popular-employers-page.component";
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {SearchJobPageComponent} from "./pages/search-job-page/search-job-page.component";
import {ChipSearchJobComponent} from "./components/chips/chip-search-job/chip-search-job.component";
import {SearchJobPageModule} from "./pages/search-job-page/search-job-page.module";
import {JobShortListComponent} from "./components/lists/job-short-list/job-short-list.component";
import {RegistrationLoginModule} from "./pages/auth/registration-login.module";
import {RegistrationPageComponent} from "./pages/auth/registration-page/registration-page.component";
import {JobDetailComponent} from "./pages/jobs/job-detail/job-detail.component";
import {JobsModule} from "./pages/jobs/jobs.module";
import {MatBadgeModule} from "@angular/material/badge";
import {MatIconModule} from "@angular/material/icon";
import {MatButtonModule} from "@angular/material/button";
import {MatMenuModule} from "@angular/material/menu";
import {EmployerModule} from "./pages/employers/employer.module";
import {EmployerPageComponent} from "./pages/employers/employer-page/employer-page.component";
import {
  RecommendationSearcherPageComponent
} from "./pages/recommendation-searcher-page/recommendation-searcher-page.component";
import {JobShortCardComponent} from "./components/cards/job-short-card/job-short-card.component";
import {PaginationModule} from "./components/pagination/pagination.module";
import {ChipsModule} from "./components/chips/chips.module";
import {PipeModule} from "./pipes/pipes.module";
import {LoadingProgressComponent} from "./components/view-blocks/loading-progress/loading-progress.component";
import {AppRoutingModule} from "./app-routing.module";
import {AuthModule} from "./components/auth/auth.module";
import {HttpErrorInterceptor} from "./auth/http-error.interceptor";
import {TokenInterceptor} from "./auth/token.interceptor";
import {FacebookLoginProvider, GoogleLoginProvider, SocialAuthServiceConfig} from "@abacritt/angularx-social-login";
import {environment} from "../environments/environment";
import {StoreModule} from "@ngrx/store";
import {reducers} from "./store/reducers";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import {ProfileHeaderComponent} from "./pages/profile/profile-header/profile-header.component";
import {ProfileModule} from "./pages/profile/profile.module";
import {AuthGuard} from "./auth/auth.guard";
import {PopupModule} from "./components/popup/popup.module";
import {DirectivesModule} from "./directive/directives.module";
import {ChatRoomsModule} from "./pages/chats/chat-rooms.module";
import {SavedJobsPageComponent} from "./pages/saved-jobs-page/saved-jobs-page.component";

let EffectsModule;

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HeaderSearcherComponent,
        FooterSearcherComponent,
        PopularEmployersPageComponent,
        RecommendationSearcherPageComponent,
        SavedJobsPageComponent,
    ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    AppRoutingModule,

    BrowserAnimationsModule,
    //NoopAnimationsModule,

    HomeModule,
    SearchJobPageModule,
    RegistrationLoginModule,
    JobsModule,
    EmployerModule,
    FormsModule,
    StoreModule.forRoot(reducers, {}),
    //EffectsModule.forRoot([]),
    MatSnackBarModule,
    DirectivesModule,


    NgOptimizedImage,
    BrowserAnimationsModule,
    ChipSearchJobComponent,
    MatBadgeModule,
    MatIconModule,
    MatButtonModule,
    MatMenuModule,
    JobShortCardComponent,
    PaginationModule,
    ChipsModule,
    PipeModule,
    AuthModule,
    ProfileModule,
    PopupModule,
    ChatRoomsModule,

  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          /*{
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              environment.socialLogin.google.clientId
            ),
          },
          {
            id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider(
              environment.socialLogin.facebook.clientId
            ),
          },*/
        ],
        onError: err => {
          console.error(err);
        },
      } as SocialAuthServiceConfig,
    },
  ],
  exports: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
