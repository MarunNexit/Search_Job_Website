import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-popup-analytics',
  templateUrl: './popup-analytics.component.html',
  styleUrls: ['./popup-analytics.component.css']
})
export class PopupAnalyticsComponent {
  @Input() Data: any;
  @Input() Type: any;

}
