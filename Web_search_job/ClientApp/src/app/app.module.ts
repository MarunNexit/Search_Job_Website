import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import {HeaderSearcherComponent} from "./components/header-searcher/header-searcher.component";
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './pages/home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import {NgOptimizedImage} from "@angular/common";
import {ChipJobComponent} from "./components/chips/chip-job/chip-job.component";
import {FooterSearcherComponent} from "./components/footer-searcher/footer-searcher.component";
import {HomeModule} from "./pages/home/home.module";
import {PopularEmployersPageComponent} from "./pages/popular-employers-page/popular-employers-page.component";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {SearchJobPageComponent} from "./pages/search-job-page/search-job-page.component";
import {ChipSearchJobComponent} from "./components/chips/chip-search-job/chip-search-job.component";
import {SearchJobPageModule} from "./pages/search-job-page/search-job-page.module";
import {JobShortListComponent} from "./components/lists/job-short-list/job-short-list.component";
import {RegistrationLoginModule} from "./pages/registration-login-page/registration-login.module";
import {RegistrationLoginPageComponent} from "./pages/registration-login-page/registration-login-page.component";
import {JobDetailComponent} from "./pages/jobs/job-detail/job-detail.component";
import {JobsModule} from "./pages/jobs/jobs.module";
import {MatBadgeModule} from "@angular/material/badge";
import {MatIconModule} from "@angular/material/icon";
import {MatButtonModule} from "@angular/material/button";
import {MatMenuModule} from "@angular/material/menu";
import {EmployerModule} from "./pages/employers/employer.module";
import {EmployerPageComponent} from "./pages/employers/employer-page/employer-page.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    CounterComponent,
    FetchDataComponent,
    HeaderSearcherComponent,
    FooterSearcherComponent,
    PopularEmployersPageComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    HomeModule,
    SearchJobPageModule,
    RegistrationLoginModule,
    JobsModule,
    EmployerModule,


    FormsModule,
    RouterModule.forRoot([
      {path: '', component: HomeComponent, pathMatch: 'full'},
      {path: 'search-job', component: SearchJobPageComponent},
      {path: 'popular-employers', component: PopularEmployersPageComponent},
      {path: 'login-register', component: RegistrationLoginPageComponent},
      {path: 'job/:id', component: JobDetailComponent},
      {path: 'employer/:id', component: EmployerPageComponent},
      {path: 'counter', component: CounterComponent},
      {path: 'fetch-data', component: FetchDataComponent},
    ]),
    NgOptimizedImage,
    BrowserAnimationsModule,
    ChipSearchJobComponent,
    MatBadgeModule,
    MatIconModule,
    MatButtonModule,
    MatMenuModule
  ],
  providers: [],
  exports: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
