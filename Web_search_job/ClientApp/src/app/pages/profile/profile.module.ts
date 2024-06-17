import {NgModule} from "@angular/core";
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {ProfilePageComponent} from "./profile-page/profile-page.component";
import {ProfileHeaderComponent} from "./profile-header/profile-header.component";
import {MatIconModule} from "@angular/material/icon";
import {ProfileResumeComponent} from "../../components/resume/profile-resume/profile-resume.component";
import {ChipsModule} from "../../components/chips/chips.module";
import {MatButtonModule} from "@angular/material/button";
import {MatMenuModule} from "@angular/material/menu";
import {ResumeSectionComponent} from "../../components/resume/resume-section/resume-section.component";
import {EmployerModule} from "../employers/employer.module";
import {PipeModule} from "../../pipes/pipes.module";
import {BrowserAnimationsModule, NoopAnimationsModule} from "@angular/platform-browser/animations";
import {BrowserModule} from "@angular/platform-browser";
import {ViewBlocksModule} from "../../components/view-blocks/view-blocks.module";
import {FormsModule} from "@angular/forms";

@NgModule({
  declarations: [
    ProfilePageComponent,
    ProfileHeaderComponent,
    ProfileResumeComponent,
    ResumeSectionComponent,
  ],
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    NgOptimizedImage,
    MatIconModule,
    ChipsModule,
    MatButtonModule,
    MatMenuModule,
    EmployerModule,
    PipeModule,
    BrowserAnimationsModule,
    //NoopAnimationsModule,
    BrowserModule,
    ViewBlocksModule,
    FormsModule,
  ],
  exports: [
    ProfilePageComponent,
    ProfileHeaderComponent,
    ResumeSectionComponent,
  ]
})
export class ProfileModule { }
