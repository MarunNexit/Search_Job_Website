import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-popup-hint',
  templateUrl: './popup-hint.component.html',
  styleUrls: ['./popup-hint.component.scss']
})
export class PopupHintComponent {
  @Input() Data: any;
  @Input() Type: string;

}
