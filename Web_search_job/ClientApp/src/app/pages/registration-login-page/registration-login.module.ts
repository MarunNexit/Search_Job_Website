import { NgModule } from '@angular/core';
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {RegistrationLoginPageComponent} from "./registration-login-page.component";
import {RiveModule} from "ng-rive";

@NgModule({
  declarations: [
    RegistrationLoginPageComponent,
  ],
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    NgOptimizedImage,
    RiveModule,

  ],
  exports: [
    RegistrationLoginPageComponent,
  ]
})
export class RegistrationLoginModule { }
