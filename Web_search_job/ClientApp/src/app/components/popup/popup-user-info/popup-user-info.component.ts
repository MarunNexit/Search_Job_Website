import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-popup-user-info',
  templateUrl: './popup-user-info.component.html',
  styleUrls: ['./popup-user-info.component.css']
})
export class PopupUserInfoComponent {
  @Input() Data: any;
  @Input() Type: any;
}
