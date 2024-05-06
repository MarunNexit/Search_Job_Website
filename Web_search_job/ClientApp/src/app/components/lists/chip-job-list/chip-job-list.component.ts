import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-chip-job-list',
  templateUrl: './chip-job-list.component.html',
  styleUrls: ['./chip-job-list.component.scss']
})
export class ChipJobListComponent {
  @Input() IsBig: boolean;
}
