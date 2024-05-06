import {NgModule} from "@angular/core";
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {ChipsJobPriorityComponent} from "./chips-job-priority/chips-job-priority.component";
import {ChipSearchJobComponent} from "./chip-search-job/chip-search-job.component";
import {ChipJobListComponent} from "../lists/chip-job-list/chip-job-list.component";
import {ChipAboutEmployerComponent} from "./chip-about-employer/chip-about-employer.component";


@NgModule({
  declarations: [
    ChipsJobPriorityComponent,
    ChipJobListComponent,
    ChipAboutEmployerComponent
  ],
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    NgOptimizedImage,
    ChipSearchJobComponent,

  ],
  exports: [
    ChipsJobPriorityComponent,
    ChipSearchJobComponent,
    ChipJobListComponent,
    ChipAboutEmployerComponent
  ]
})
export class ChipsModule { }
