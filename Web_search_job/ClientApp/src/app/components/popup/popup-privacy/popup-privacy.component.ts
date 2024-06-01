import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-popup-privacy',
  templateUrl: './popup-privacy.component.html',
  styleUrls: ['./popup-privacy.component.css']
})
export class PopupPrivacyComponent {
  @Input() Data: any;
  @Input() Type: any;

}
