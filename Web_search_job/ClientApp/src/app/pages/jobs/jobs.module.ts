import {NgModule} from "@angular/core";
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {JobDetailComponent} from "./job-detail/job-detail.component";
import {SimilarJobsListComponent} from "../../components/lists/similar-jobs-list/similar-jobs-list.component";
import {JobShortCardComponent} from "../../components/cards/job-short-card/job-short-card.component";
import {AppropriatenessJobComponent} from "../../components/cards/appropriateness-job/appropriateness-job.component";
import {MatButtonModule} from "@angular/material/button";
import {MatMenuModule} from "@angular/material/menu";
import {ChipsModule} from "../../components/chips/chips.module";
import {MatIconModule} from "@angular/material/icon";
import {MatDialogModule} from "@angular/material/dialog";
import {BrowserModule} from "@angular/platform-browser";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {PipeModule} from "../../pipes/pipes.module";

@NgModule({
  declarations: [
    SimilarJobsListComponent,
    AppropriatenessJobComponent,
    JobDetailComponent,
  ],
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    NgOptimizedImage,
    JobShortCardComponent,
    MatButtonModule,
    MatMenuModule,
    ChipsModule,
    MatIconModule,
    BrowserModule,
    BrowserAnimationsModule,
    MatDialogModule,
    PipeModule,

  ],
  exports: [
    SimilarJobsListComponent,
  ]
})
export class JobsModule { }
