import {NgModule} from "@angular/core";
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {PaginationComponent} from "./pagination-list/pagination.component";
import {MatIconModule} from "@angular/material/icon";

@NgModule({
  declarations: [
    PaginationComponent,

  ],
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    NgOptimizedImage,
    MatIconModule,

  ],
  exports: [
    PaginationComponent,

  ]
})
export class PaginationModule { }
