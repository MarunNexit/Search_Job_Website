import { NgModule } from '@angular/core';
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {SearchJobPageComponent} from "./search-job-page.component";
import {FilterSearchCardComponent} from "../../components/cards/filter-search-card/filter-search-card.component";
import {JobShortCardComponent} from "../../components/cards/job-short-card/job-short-card.component";
import {MatCheckboxModule} from "@angular/material/checkbox";
import {FilterSearchListComponent} from "../../components/lists/filter-search-list/filter-search-list.component";
import {JobShortListComponent} from "../../components/lists/job-short-list/job-short-list.component";
import {HomeModule} from "../home/home.module";
import {ChipsModule} from "../../components/chips/chips.module";
import {
  BlankBlockNoResultComponent
} from "../../components/view-blocks/blank-block-no-result/blank-block-no-result.component";
import {ViewBlocksModule} from "../../components/view-blocks/view-blocks.module";
import {FormsModule} from "@angular/forms";
import {PaginationModule} from "../../components/pagination/pagination.module";

@NgModule({
  declarations: [
    SearchJobPageComponent,
    FilterSearchListComponent,
    JobShortListComponent,
    BlankBlockNoResultComponent,
  ],
    imports: [
        CommonModule,
        RouterLink,
        RouterLinkActive,
        NgOptimizedImage,
        MatCheckboxModule,
        FilterSearchCardComponent,
        HomeModule,
        JobShortCardComponent,
        ChipsModule,
        ViewBlocksModule,
        FormsModule,
        PaginationModule,
    ],
  exports: [
    SearchJobPageComponent,
  ]
})
export class SearchJobPageModule { }
