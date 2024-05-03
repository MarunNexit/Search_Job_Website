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

@NgModule({
  declarations: [
    EmployerPageComponent,
    EmployerJobsListComponent,
    CommentsRatingComponent,
    CommentsListComponent,
    CommentCardComponent
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

  ],
  exports: [
    EmployerPageComponent,
  ]
})
export class EmployerModule { }
