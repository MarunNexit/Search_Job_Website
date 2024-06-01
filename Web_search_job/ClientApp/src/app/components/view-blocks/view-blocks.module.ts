import {NgModule} from "@angular/core";
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {LoadingProgressComponent} from "./loading-progress/loading-progress.component";
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";

@NgModule({
  declarations: [
    LoadingProgressComponent
  ],
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    NgOptimizedImage,
    MatProgressSpinnerModule,

  ],
  exports: [
    LoadingProgressComponent,
  ]
})
export class ViewBlocksModule { }
