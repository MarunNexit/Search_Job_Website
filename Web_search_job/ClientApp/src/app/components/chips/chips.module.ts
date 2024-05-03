import {NgModule} from "@angular/core";
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {ChipsJobPriorityComponent} from "./chips-job-priority/chips-job-priority.component";
import {ChipSearchJobComponent} from "./chip-search-job/chip-search-job.component";
import {ChipJobComponent} from "./chip-job/chip-job.component";


@NgModule({
  declarations: [
    ChipsJobPriorityComponent,
    ChipJobComponent
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
    ChipJobComponent
  ]
})
export class ChipsModule { }
