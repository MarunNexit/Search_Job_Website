import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from '@angular/core';
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {RegistrationLoginPageComponent} from "./registration-login-page/registration-login-page.component";
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
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA // Додайте CUSTOM_ELEMENTS_SCHEMA для використання веб-компонентів
  ]
})
export class RegistrationLoginModule { }
