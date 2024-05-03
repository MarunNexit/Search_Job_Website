import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-chip-job',
  templateUrl: './chip-job.component.html',
  styleUrls: ['./chip-job.component.scss']
})
export class ChipJobComponent {
  @Input() IsBig: boolean;
}
