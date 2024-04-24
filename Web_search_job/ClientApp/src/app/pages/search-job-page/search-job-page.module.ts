import { NgModule } from '@angular/core';
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {SearchJobPageComponent} from "./search-job-page.component";
import {ChipSearchJobComponent} from "../../components/chips/chip-search-job/chip-search-job.component";
import {FilterSearchCardComponent} from "../../components/cards/filter-search-card/filter-search-card.component";
import {JobShortCardComponent} from "../../components/cards/job-short-card/job-short-card.component";
import {MatCheckboxModule} from "@angular/material/checkbox";
import {FilterSearchListComponent} from "../../components/lists/filter-search-list/filter-search-list.component";
import {JobShortListComponent} from "../../components/lists/job-short-list/job-short-list.component";
import {HomeModule} from "../home/home.module";

@NgModule({
  declarations: [
    SearchJobPageComponent,
    JobShortCardComponent,
    FilterSearchListComponent,
    JobShortListComponent,
  ],
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    NgOptimizedImage,
    ChipSearchJobComponent,
    MatCheckboxModule,
    FilterSearchCardComponent,
    HomeModule,

  ],
  exports: [
    SearchJobPageComponent,

  ]
})
export class SearchJobPageModule { }
