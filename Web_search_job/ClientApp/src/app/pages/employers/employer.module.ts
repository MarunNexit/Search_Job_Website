import {NgModule} from "@angular/core";
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {EmployerPageComponent} from "./employer-page/employer-page.component";
import {MatMenuModule} from "@angular/material/menu";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {ChipsModule} from "../../components/chips/chips.module";
import {EmployerJobsListComponent} from "../../components/lists/employer-jobs-list/employer-jobs-list.component";
import {JobShortCardComponent} from "../../components/cards/job-short-card/job-short-card.component";
import {HomeModule} from "../home/home.module";
import {CommentsRatingComponent} from "../../components/comments/comments-rating/comments-rating.component";
import {CommentsListComponent} from "../../components/lists/comments-list/comments-list.component";
import {CommentCardComponent} from "../../components/cards/comment-card/comment-card.component";
import {PipeModule} from "../../pipes/pipes.module";
import {
  SubscribeToEmployerComponent
} from "../../components/view-blocks/subscribe-to-employer/subscribe-to-employer.component";
import {AboutEmployerComponent} from "../../components/lists/about-employer/about-employer.component";
import {FilterSearchCardComponent} from "../../components/cards/filter-search-card/filter-search-card.component";
import {ViewBlocksModule} from "../../components/view-blocks/view-blocks.module";
import {JobCreatePageComponent} from "./jobs/job-create-page/job-create-page.component";
import {FormsModule} from "@angular/forms";
import {JobCreateComponent} from "./jobs/job-create/job-create.component";


@NgModule({
  declarations: [
    EmployerPageComponent,
    EmployerJobsListComponent,
    CommentsRatingComponent,
    CommentsListComponent,
    CommentCardComponent,
    SubscribeToEmployerComponent,
    AboutEmployerComponent,
    JobCreatePageComponent,
    JobCreateComponent,
  ],
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    NgOptimizedImage,
    MatMenuModule,
    MatButtonModule,
    MatIconModule,
    ChipsModule,
    JobShortCardComponent,
    HomeModule,
    PipeModule,
    FilterSearchCardComponent,
    ViewBlocksModule,
    PipeModule,
    FormsModule,

  ],
    exports: [
        EmployerPageComponent,
        AboutEmployerComponent,
        JobCreatePageComponent,
        JobCreateComponent,
    ]
})
export class EmployerModule { }
