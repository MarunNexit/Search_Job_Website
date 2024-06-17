import {NgModule} from "@angular/core";
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {RiveModule} from "ng-rive";
import {MatInputModule} from "@angular/material/input";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {PopupPurpleComponent} from "./popup-purple/popup-purple.component";
import {PopupJobRequestComponent} from "./popup-job-request/popup-job-request.component";
import {PopupCreateResumeComponent} from "./popup-create-resume/popup-create-resume.component";
import {PopupAnalyticsComponent} from "./popup-analytics/popup-analytics.component";
import {PopupPrivacyComponent} from "./popup-privacy/popup-privacy.component";
import {PopupProfileSettingsComponent} from "./popup-profile-settings/popup-profile-settings.component";
import {PopupInfoComponent} from "./popup-info/popup-info.component";
import {MatIconModule} from "@angular/material/icon";
import {PopupWhiteComponent} from "./popup-white/popup-white.component";
import {MatButtonModule} from "@angular/material/button";
import {MatMenuModule} from "@angular/material/menu";
import {PopupWorkExperienceComponent} from "./popup-work-experience/popup-work-experience.component";
import {PopupUserInfoComponent} from "./popup-user-info/popup-user-info.component";
import {PopupHintComponent} from "./popup-hint/popup-hint.component";
import {PopupAddSectionComponent} from "./popup-add-section/popup-add-section.component";
import {PopupNewResumeComponent} from "./popup-new-resume/popup-new-resume.component";
import {PopupAddSectionCardComponent} from "../cards/popup-add-section-card/popup-add-section-card.component";
import {PopupAddUserInfoComponent} from "./popup-add-user-info/popup-add-user-info.component";
import {ViewBlocksModule} from "../view-blocks/view-blocks.module";

@NgModule({
  declarations: [
    PopupPurpleComponent,
    PopupJobRequestComponent,
    PopupCreateResumeComponent,
    PopupAnalyticsComponent,
    PopupPrivacyComponent,
    PopupProfileSettingsComponent,
    PopupInfoComponent,
    PopupWhiteComponent,
    PopupWorkExperienceComponent,
    PopupUserInfoComponent,
    PopupHintComponent,
    PopupAddSectionComponent,
    PopupNewResumeComponent,
    PopupAddSectionCardComponent,
    PopupAddUserInfoComponent,

  ],
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    NgOptimizedImage,
    RiveModule,
    MatInputModule,
    ReactiveFormsModule,
    MatIconModule,
    MatButtonModule,
    MatMenuModule,
    FormsModule,
    ViewBlocksModule,

  ],
  exports: [
    PopupPurpleComponent,
    PopupJobRequestComponent,
    PopupCreateResumeComponent,
    PopupAnalyticsComponent,
    PopupPrivacyComponent,
    PopupProfileSettingsComponent,
    PopupInfoComponent,
    PopupWhiteComponent,
  ],
  schemas: [

  ]
})
export class PopupModule { }
