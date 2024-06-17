import {NgModule} from "@angular/core";
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {JobShortCardComponent} from "../../components/cards/job-short-card/job-short-card.component";
import {PaginationModule} from "../../components/pagination/pagination.module";
import {RequestHistoryPageComponent} from "./request-history-page.component";
import {ViewBlocksModule} from "../../components/view-blocks/view-blocks.module";

@NgModule({
  declarations: [
    RequestHistoryPageComponent
  ],
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    NgOptimizedImage,
    JobShortCardComponent,
    PaginationModule,
    ViewBlocksModule,
  ],
  exports: [
    RequestHistoryPageComponent
  ]
})
export class RequestHistoryModule { }
