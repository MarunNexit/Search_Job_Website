import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-blank-block-no-result',
  templateUrl: './blank-block-no-result.component.html',
  styleUrls: ['./blank-block-no-result.component.scss']
})
export class BlankBlockNoResultComponent {
  @Input() text: string;

}
