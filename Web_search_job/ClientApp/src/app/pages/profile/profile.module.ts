import {NgModule} from "@angular/core";
import {SearchJobPageComponent} from "../search-job-page/search-job-page.component";
import {FilterSearchListComponent} from "../../components/lists/filter-search-list/filter-search-list.component";
import {JobShortListComponent} from "../../components/lists/job-short-list/job-short-list.component";
import {
  BlankBlockNoResultComponent
} from "../../components/view-blocks/blank-block-no-result/blank-block-no-result.component";
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {MatCheckboxModule} from "@angular/material/checkbox";
import {FilterSearchCardComponent} from "../../components/cards/filter-search-card/filter-search-card.component";
import {HomeModule} from "../home/home.module";
import {JobShortCardComponent} from "../../components/cards/job-short-card/job-short-card.component";
import {ChipsModule} from "../../components/chips/chips.module";
import {ViewBlocksModule} from "../../components/view-blocks/view-blocks.module";

@NgModule({
  declarations: [

  ],
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    NgOptimizedImage,
    
  ],
  exports: [

  ]
})
export class ProfileMogule { }
