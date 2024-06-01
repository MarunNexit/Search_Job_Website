import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-popup-create-resume',
  templateUrl: './popup-create-resume.component.html',
  styleUrls: ['./popup-create-resume.component.scss']
})
export class PopupCreateResumeComponent {
  @Input() Data: any;
  @Input() Type: any;

}
