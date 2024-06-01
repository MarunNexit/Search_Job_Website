import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-popup-profile-settings',
  templateUrl: './popup-profile-settings.component.html',
  styleUrls: ['./popup-profile-settings.component.css']
})
export class PopupProfileSettingsComponent {
  @Input() Data: any;
  @Input() Type: any;

}
