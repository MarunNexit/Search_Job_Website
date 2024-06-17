import {NgModule} from "@angular/core";
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {JobShortCardComponent} from "../../components/cards/job-short-card/job-short-card.component";
import {PaginationModule} from "../../components/pagination/pagination.module";
import {ViewBlocksModule} from "../../components/view-blocks/view-blocks.module";
import { RecommendationSearcherPageComponent } from "../../components/recommendation-searcher-page/recommendation-searcher-page.component";

@NgModule({
  declarations: [
    RecommendationSearcherPageComponent
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
    RecommendationSearcherPageComponent
  ]
})
export class RecommendationModule { }
