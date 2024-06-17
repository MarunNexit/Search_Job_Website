import {NgModule} from "@angular/core";
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {ChipsJobPriorityComponent} from "./chips-job-priority/chips-job-priority.component";
import {ChipSearchJobComponent} from "./chip-search-job/chip-search-job.component";
import {ChipJobListComponent} from "../lists/chip-job-list/chip-job-list.component";
import {ChipAboutEmployerComponent} from "./chip-about-employer/chip-about-employer.component";
import {JobRequestChipComponent} from "./job-request-chip/job-request-chip.component";


@NgModule({
  declarations: [
    ChipsJobPriorityComponent,
    ChipJobListComponent,
    ChipAboutEmployerComponent,
    JobRequestChipComponent,
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
    ChipAboutEmployerComponent,
    JobRequestChipComponent
  ]
})
export class ChipsModule { }
