import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-popup-new-resume',
  templateUrl: './popup-new-resume.component.html',
  styleUrls: ['./popup-new-resume.component.css']
})
export class PopupNewResumeComponent {
  @Input() Data: any;

}
