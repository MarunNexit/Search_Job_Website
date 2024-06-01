import {NgModule} from "@angular/core";
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {MatMenuModule} from "@angular/material/menu";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {ChatRoomsPageComponent} from "./chat-rooms-page/chat-rooms-page.component";
import {FormsModule} from "@angular/forms";
import {MatCheckboxModule} from "@angular/material/checkbox";
import {ChatRoomCardComponent} from "../../components/cards/chat-room-card/chat-room-card.component";
import {MatBadgeModule} from "@angular/material/badge";
import {ChatPageComponent} from "./chat-page/chat-page.component";

@NgModule({
  declarations: [
    ChatRoomsPageComponent,
    ChatRoomCardComponent,
    ChatPageComponent,
  ],
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    NgOptimizedImage,
    MatMenuModule,
    MatButtonModule,
    MatIconModule,
    FormsModule,
    MatCheckboxModule,
    MatBadgeModule,

  ],
  exports: [
    ChatRoomsPageComponent,
    ChatRoomCardComponent,
    ChatPageComponent,
  ]
})
export class ChatRoomsModule { }
