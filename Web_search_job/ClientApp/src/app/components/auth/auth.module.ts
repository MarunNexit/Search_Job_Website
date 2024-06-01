import {NgModule} from "@angular/core";
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {SocialLoginComponent} from "./social-login/social-login.component";
import {TestUsersComponent} from "./test-users/test-users.component";
import {GoogleSigninButtonDirective} from "../../auth/google-signin-button.directive";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";


@NgModule({
  declarations: [
    SocialLoginComponent,
    TestUsersComponent,
    GoogleSigninButtonDirective,
  ],
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    NgOptimizedImage,
    MatButtonModule,
    MatIconModule,

  ],
  exports: [
    SocialLoginComponent,
    TestUsersComponent,
  ]
})
export class AuthModule { }
