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

@NgModule({
  declarations: [
    SearchJobPageComponent,
    FilterSearchListComponent,
    JobShortListComponent,

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
    ChipsModule

  ],
  exports: [
    SearchJobPageComponent,
  ]
})
export class SearchJobPageModule { }
