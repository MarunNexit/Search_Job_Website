import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from '@angular/core';
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {RegistrationPageComponent} from "./registration-page/registration-page.component";
import {RiveModule} from "ng-rive";
import {MatInputModule} from "@angular/material/input";
import {ReactiveFormsModule} from "@angular/forms";
import {LoginPageComponent} from "./login-page/login-page.component";
import {LogoutPageComponent} from "./logout-page/logout-page.component";

@NgModule({
  declarations: [
    RegistrationPageComponent,
    LoginPageComponent,
    LogoutPageComponent,
  ],
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    NgOptimizedImage,
    RiveModule,
    MatInputModule,
    ReactiveFormsModule,

  ],
  exports: [
    RegistrationPageComponent,
    LoginPageComponent,
    LogoutPageComponent,
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA // Додайте CUSTOM_ELEMENTS_SCHEMA для використання веб-компонентів
  ]
})
export class RegistrationLoginModule { }
