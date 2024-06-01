import { Component } from '@angular/core';
import {RouterHelperService} from "../../../services/router-helper.service";

@Component({
  selector: 'app-chat-room-card',
  templateUrl: './chat-room-card.component.html',
  styleUrls: ['./chat-room-card.component.scss']
})
export class ChatRoomCardComponent {

  constructor(private routerHelper: RouterHelperService) {

  }

  goToURL(s: string, b: boolean) {
    this.routerHelper.goToUrl(s, b);
  }

}
