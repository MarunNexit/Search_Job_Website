import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-popup-work-experience',
  templateUrl: './popup-work-experience.component.html',
  styleUrls: ['./popup-work-experience.component.css']
})
export class PopupWorkExperienceComponent {
  @Input() Data: any;
  @Input() Type: any;
}
