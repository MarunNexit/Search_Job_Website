import {NgModule} from "@angular/core";
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {ScrollToDirective} from "./scroll-to.directive";

@NgModule({
  declarations: [
    ScrollToDirective
  ],
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    NgOptimizedImage,

  ],
  exports: [
    ScrollToDirective,
  ]
})
export class DirectivesModule { }
