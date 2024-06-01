import { NgModule } from '@angular/core';
import {HomeComponent} from "./home.component";
import {PopularEmployersComponent} from "../../components/view-blocks/popular-employers/popular-employers.component";
import {
  PopularEmployersCardComponent
} from "../../components/cards/popular-employers-card/popular-employers-card.component";
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {HighestPayingJobsComponent} from "../../components/view-blocks/highest-paying-jobs/highest-paying-jobs.component";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {
  HighestPayingJobCardComponent
} from "../../components/cards/highest-paying-job-card/highest-paying-job-card.component";
import {ChipsModule} from "../../components/chips/chips.module";
import {AuthModule} from "../../components/auth/auth.module";
import {PaginationModule} from "../../components/pagination/pagination.module";
import {ViewBlocksModule} from "../../components/view-blocks/view-blocks.module";

@NgModule({
  declarations: [
    HomeComponent,
    PopularEmployersComponent,
    PopularEmployersCardComponent,
    HighestPayingJobsComponent,
    HighestPayingJobCardComponent,

  ],
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    NgOptimizedImage,
    ChipsModule,
    AuthModule,
    PaginationModule,
    ViewBlocksModule,

  ],
  exports: [
    HomeComponent,
    PopularEmployersComponent,
    PopularEmployersCardComponent,
    HighestPayingJobsComponent,
    HighestPayingJobCardComponent,

  ]
})
export class HomeModule { }
