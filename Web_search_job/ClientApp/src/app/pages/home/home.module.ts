import { NgModule } from '@angular/core';
import {HomeComponent} from "./home.component";
import {PopularEmployersComponent} from "../../components/view-blocks/popular-employers/popular-employers.component";
import {
  PopularEmployersCardComponent
} from "../../components/cards/popular-employers-card/popular-employers-card.component";
import {ChipJobComponent} from "../../components/chips/chip-job/chip-job.component";
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {HighestPayingJobsComponent} from "../../components/view-blocks/highest-paying-jobs/highest-paying-jobs.component";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {
  HighestPayingJobCardComponent
} from "../../components/cards/highest-paying-job-card/highest-paying-job-card.component";

@NgModule({
  declarations: [
    HomeComponent,
    PopularEmployersComponent,
    PopularEmployersCardComponent,
    ChipJobComponent,
    HighestPayingJobsComponent,
    HighestPayingJobCardComponent
  ],
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    NgOptimizedImage
  ],
  exports: [
    HomeComponent,
    PopularEmployersComponent,
    PopularEmployersCardComponent,
    ChipJobComponent,
    HighestPayingJobsComponent,
    HighestPayingJobCardComponent
  ]
})
export class HomeModule { }
