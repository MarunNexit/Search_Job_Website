import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AuthGuard} from './auth/auth.guard';
import {Role} from './models/backend/dtos/users/roles';
import {HomeComponent} from "./pages/home/home.component";
import {SearchJobPageComponent} from "./pages/search-job-page/search-job-page.component";
import {PopularEmployersPageComponent} from "./pages/popular-employers-page/popular-employers-page.component";
import {RegistrationPageComponent} from "./pages/auth/registration-page/registration-page.component";
import {JobDetailComponent} from "./pages/jobs/job-detail/job-detail.component";
import {EmployerPageComponent} from "./pages/employers/employer-page/employer-page.component";
import {
  RecommendationSearcherPageComponent
} from "./components/recommendation-searcher-page/recommendation-searcher-page.component";
import {LoginPageComponent} from "./pages/auth/login-page/login-page.component";
import {ProfilePageComponent} from "./pages/profile/profile-page/profile-page.component";
import {LogoutPageComponent} from "./pages/auth/logout-page/logout-page.component";
import {LastPageService} from "./services/last-page.service";
import {AboutPageComponent} from "./pages/about-page/about-page.component";
import {ChatRoomsPageComponent} from "./pages/chats/chat-rooms-page/chat-rooms-page.component";
import {ChatPageComponent} from "./pages/chats/chat-page/chat-page.component";
import {ProfileEmployerComponent} from "./pages/profile/profile-employer/profile-employer.component";
import {SavedJobsPageComponent} from "./pages/saved-jobs-page/saved-jobs-page.component";
import {RequestHistoryPageComponent} from "./pages/request-history-page/request-history-page.component";
import {RecommendationPageComponent} from "./pages/recommendation-page/recommendation-page.component";
import {JobCreatePageComponent} from "./pages/employers/jobs/job-create-page/job-create-page.component";
import {MyJobsPageComponent} from "./pages/employers/jobs/my-jobs-page/my-jobs-page.component";


const routes: Routes = [
  { path: '', pathMatch: 'full', component: HomeComponent,
  },
  { path: 'search-job', component: SearchJobPageComponent,
    canActivate: [AuthGuard],
    data: {
      roles: [Role.User, Role.Admin],
      allowAnonymous: true,
    }
  },
  { path: 'about-us', component: AboutPageComponent,
    canActivate: [AuthGuard],
    data: {
      roles: [Role.User, Role.Admin, Role.Employer],
      allowAnonymous: true,
    }
  },
  { path: 'popular-employers', component: PopularEmployersPageComponent,
    canActivate: [AuthGuard],
    data: {
      roles: [Role.User, Role.Admin],
      allowAnonymous: true,
    }
  },
  { path: 'auth-register', component: RegistrationPageComponent,
    canActivate: [AuthGuard],
    data: {
      roles: [Role.Admin],
      allowAnonymous: true,
    }
  },
  { path: 'auth-register/:role', component: RegistrationPageComponent,
    canActivate: [AuthGuard],
    data: {
      roles: [Role.Admin],
      allowAnonymous: true,
    }
  },
  { path: 'auth-login', component: LoginPageComponent,
    canActivate: [AuthGuard],
    data: {
      roles: [Role.Admin],
      allowAnonymous: true,
    }
  },
  { path: 'job/:id', component: JobDetailComponent,
    canActivate: [AuthGuard],
    data: {
      roles: [Role.User, Role.Admin, Role.Employer],
      allowAnonymous: true,
    }
  },
  { path: 'employer/:id', component: EmployerPageComponent,
    canActivate: [AuthGuard],
    data: {
      roles: [Role.User, Role.Admin, Role.Employer],
      allowAnonymous: true,
    }
  },
  { path: 'profile/:id', component: ProfilePageComponent,
    canActivate: [AuthGuard],
    data: {
      roles: [Role.User, Role.Admin, Role.Employer],
      allowAnonymous: true,
    }
  },
  { path: 'profile-employer/:id', component: ProfileEmployerComponent,
    canActivate: [AuthGuard],
    data: {
      roles: [Role.Admin, Role.Employer],
      allowAnonymous: false,
    }
  },
  { path: 'profile', component: ProfilePageComponent,
    canActivate: [AuthGuard],
    data: {
      roles: [Role.User, Role.Admin, Role.Employer],
      allowAnonymous: false,
    }
  },
  {
    path: 'recommendations', component: RecommendationPageComponent,
    canActivate: [AuthGuard],
    data: {
      roles: [Role.User, Role.Admin],
      allowAnonymous: false,
    }
  },
  {
    path: 'saved-jobs', component: SavedJobsPageComponent,
    canActivate: [AuthGuard],
    data: {
      roles: [Role.User, Role.Admin],
      allowAnonymous: false,
    }
  },
  {
    path: 'logout', component: LogoutPageComponent,
    canActivate: [AuthGuard],
    data: {
      roles: [Role.User, Role.Admin, Role.Employer],
      allowAnonymous: false,
    }
  },
  {
    path: 'chat-rooms', component: ChatRoomsPageComponent,
    canActivate: [AuthGuard],
    data: {
      roles: [Role.User, Role.Admin, Role.Employer],
      allowAnonymous: false,
    }
  },
  { path: 'chat/:id', component: ChatPageComponent,
    canActivate: [AuthGuard],
    data: {
      roles: [Role.User, Role.Admin, Role.Employer],
      allowAnonymous: true,
    }
  },
  { path: 'request-history', component: RequestHistoryPageComponent,
    canActivate: [AuthGuard],
    data: {
      roles: [Role.User, Role.Admin],
      allowAnonymous: false,
    }
  },
  { path: 'create-job', component: JobCreatePageComponent,
    canActivate: [AuthGuard],
    data: {
      roles: [Role.Employer, Role.Admin],
      allowAnonymous: false,
    }
  },
  { path: 'my-jobs', component: MyJobsPageComponent,
    canActivate: [AuthGuard],
    data: {
      roles: [Role.Employer, Role.Admin],
      allowAnonymous: false,
    }
  },
/*  {
    path: 'protected',
    component: ProtectedPageComponent,
    canActivate: [AuthGuard],
    data: { roles: [Role.User, Role.Admin] }
  },*/
/*  { path: 'login-page', component: LoginPageComponent },
  { path: 'register', component: RegisterPageComponent }*/
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
