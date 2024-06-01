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
} from "./pages/recommendation-searcher-page/recommendation-searcher-page.component";
import {CounterComponent} from "./counter/counter.component";
import {FetchDataComponent} from "./fetch-data/fetch-data.component";
import {LoginPageComponent} from "./pages/auth/login-page/login-page.component";
import {ProfilePageComponent} from "./pages/profile/profile-page/profile-page.component";
import {LogoutPageComponent} from "./pages/auth/logout-page/logout-page.component";
import {LastPageService} from "./services/last-page.service";
import {AboutPageComponent} from "./pages/about-page/about-page.component";
import {ChatRoomsPageComponent} from "./pages/chats/chat-rooms-page/chat-rooms-page.component";
import {ChatPageComponent} from "./pages/chats/chat-page/chat-page.component";


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
      allowAnonymous: false,
    }
  },
  {
    path: 'recommendations', component: RecommendationSearcherPageComponent,
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
